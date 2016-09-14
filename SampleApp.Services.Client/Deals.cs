//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Reflection;
//using System.ServiceModel;
//using System.Text;
////using SampleApp.Contracts.Data;
//using System.Configuration;
//using System.Threading;
//using SampleApp.Services.Client.Factory;
//using SampleApp.Services.Client.Logger;
//using SampleApp.Services.Client.Reference;

//namespace SampleApp.Services.Client
//{
//    public sealed class Deals
//    {
//        #region Member Variables

//        private static volatile Deals _instance;
//        private static readonly object SyncRoot = new Object();
//        private readonly IProxyFactory factory;

//        private readonly ServiceClientLogger logger = new ServiceClientLogger();

//        #endregion

//        [ThreadStatic]
//        private static SampleApp.Contracts.Data.Header _header;

//        public static SampleApp.Contracts.Data.Header Header
//        {
//            get { return _header; }
//            set { _header = value; }
//        }

//        #region Instance and Static Constructor

//        private Deals()
//        {
//            factory = new WCFServiceClient();
//        }

//        public static Deals Instance
//        {
//            get
//            {
//                if (_instance == null)
//                {
//                    lock (SyncRoot)
//                    {
//                        if (_instance == null)
//                            _instance = new Deals();
//                    }
//                }
//                return _instance;
//            }
//        }

//        #endregion

//        #region Public Methods - exposed

//        public IAsyncResult DeleteEntireOffer(int masterCommonDealId, string rsTablePrefix, AsyncCallback cb, object state)
//        {
//            //DMServiceAsyncResult results = new DMServiceAsyncResult(cb, state, commonDealId);
//            logger.LogMethoInfo(new object[] { masterCommonDealId });
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.DeleteEntireOffer(masterCommonDealId, rsTablePrefix);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                //results.Error = ex.Message;
//                throw ex;
//            }
//            finally
//            {
//                //results.CompleteCall();
//            }
//            return null;
//        }
//        public void SaveCommonDeal(EntityCommonDeal deal)
//        {
//            logger.LogMethoInfo();
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.SaveCommonDeal(deal);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public List<CommonDealPackage> SavePackages(List<CommonDealPackage> commonDealPackages)
//        {

//            logger.LogMethoInfo(commonDealPackages);

//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    List<CommonDealPackage> packages = proxy.SavePackages(commonDealPackages);
//                    logger.LogReturnValueInfo(packages);
//                    return packages;
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//            return null;
//        }

//        public void CopyPackagesToLocations(int masterdealId, List<int> locationDealIds)
//        {

//            logger.LogMethoInfo(new object[] { masterdealId, locationDealIds });

//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.CopyPackagesToLocations(masterdealId, locationDealIds);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public List<CommonDealPackage> GetPackages(int commonDealId)
//        {

//            logger.LogMethoInfo(commonDealId);

//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    var commonDealPackages = proxy.GetPackages(commonDealId);

//                    logger.LogReturnValueInfo(commonDealPackages);
//                    return commonDealPackages;
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }

//            return null;

//        }

//        public void DeletePackage(int commonDealPackageId)
//        {

//            logger.LogMethoInfo(commonDealPackageId);

//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.DeletePackage(commonDealPackageId);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;

//            }

//        }

//        public void CanItemsBeDeletedFromDeal(int commonDealId, List<int> retailItemIds)
//        {
//            logger.LogMethoInfo(new object[] { commonDealId, retailItemIds });
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.CanItemsBeDeletedFromDeal(commonDealId, retailItemIds);
//                }
//            }
//            catch (Exception ex)
//            {
//                //logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public void DeleteAllPackagesForMasterDeaId(int masterCommonDealId)
//        {
//            logger.LogMethoInfo(masterCommonDealId);
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.DeleteAllPackagesForMasterDeaId(masterCommonDealId);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public ProductSet CreateOrUpdateProductSet(int commonDealId, ProductSet productSet)
//        {

//            logger.LogMethoInfo(new object[] { commonDealId, productSet });

//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    var productset = proxy.CreateOrUpdateProductSet(commonDealId, productSet);
//                    // throwing recurssion limit exception - logger.LogReturnValueInfo(productset);
//                    return productset;
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }

//        }

//        public void ValidatePackageDeal(int masterCommonDealId)
//        {

//            logger.LogMethoInfo(masterCommonDealId);

//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.ValidatePackageDeal(masterCommonDealId);
//                    // TODO: should this return boolean or it only only updating in DB?
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public void ValidatePackageDeal(int masterCommonDealId, PackageType packageType)
//        {

//            logger.LogMethoInfo(masterCommonDealId);

//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.ValidateSpecificPackageDeal(masterCommonDealId, packageType);
//                    // TODO: should this return boolean or it only only updating in DB?
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public List<IdValuePair> GetAllAllowanceType()
//        {

//            logger.LogMethoInfo();

//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    List<IdValuePair> allowanceTypes = proxy.GetAllAllowanceType();

//                    logger.LogReturnValueInfo(allowanceTypes);

//                    return allowanceTypes;
//                }


//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//            return null;
//        }

//        public string GetDeals(int dealId)
//        {
//            string retVal = "";

//            logger.LogMethoInfo(dealId);
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    retVal = proxy.GetDeals(dealId);

//                    logger.LogReturnValueInfo(retVal);

//                    return retVal;

//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }

//            return null;

//        }

//        public bool IsProductSetNameAvailable(int commonDealId, string productSetName)
//        {
//            logger.LogMethoInfo(new object[] { commonDealId, productSetName });

//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    bool isAVailable = proxy.IsProductSetNameAvailable(commonDealId, productSetName);
//                    logger.LogReturnValueInfo(isAVailable);
//                    return isAVailable;
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }

//            return false;
//        }

//        public ProductSetWithAllItems GetProductSetWithAllItemsUsingId(int commonDealId, int productSetID)
//        {
//            logger.LogMethoInfo(new object[] { commonDealId, productSetID });
//            try
//            {
//                ProductSetWithAllItems productSet;
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    productSet = proxy.GetProductSetWithAllItemsUsingId(commonDealId, productSetID);
//                    logger.LogReturnValueInfo(productSet);

//                }
//                return productSet;
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//            return null;
//        }

//        public ProductSetWithAllItems GetProductSetWithAllItems(int commonDealId, string productSetName)
//        {
//            logger.LogMethoInfo(new object[] { commonDealId, productSetName });
//            try
//            {
//                ProductSetWithAllItems productSet;
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    productSet = proxy.GetProductSetWithAllItems(commonDealId, productSetName);
//                    logger.LogReturnValueInfo(productSet);

//                }
//                return productSet;
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//            return null;
//        }

//        public List<IdValuePair> GetAllProductSetRelationType()
//        {
//            try
//            {
//                logger.LogMethoInfo();
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    List<IdValuePair> productReleationTypes = proxy.GetAllProductSetRelationType();
//                    logger.LogReturnValueInfo(productReleationTypes);
//                    return productReleationTypes;

//                }

//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//            return null;

//        }

//        public List<IdValuePair> GetAllProductSetRelationTypeWithGet()
//        {
//            try
//            {
//                logger.LogMethoInfo();
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    List<IdValuePair> productReleationTypes = proxy.GetAllProductSetRelationTypeWithGet();
//                    logger.LogReturnValueInfo(productReleationTypes);
//                    return productReleationTypes;
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//            return null;

//        }

//        public List<IdValuePair> GetAllPackageRelationType()
//        {
//            logger.LogMethoInfo();
//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    List<IdValuePair> releationTypes = proxy.GetAllPackageRelationType();
//                    logger.LogReturnValueInfo(releationTypes);
//                    return releationTypes;
//                }

//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//            return null;
//        }
//        public ProductSet GetProductSet(int productSetID)
//        {
//            logger.LogMethoInfo(productSetID);
//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    ProductSet productSet = proxy.GetProductSet(productSetID);
//                    logger.LogReturnValueInfo(productSet);
//                    return productSet;
//                }

//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//            return null;
//        }

//        public List<IdValuePair> GetAllQuantityType()
//        {

//            logger.LogMethoInfo();

//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    List<IdValuePair> quantityTypes = proxy.GetAllQuantityType();

//                    logger.LogReturnValueInfo(quantityTypes);

//                    return quantityTypes;
//                }


//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//            return null;
//        }

//        #endregion

//        #region Allowances(Event Type) related methods

//        public List<AllowanceResponse> SaveAllowances(List<AllowanceRequest> commonEventType)
//        {
//            logger.LogMethoInfo();

//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    return proxy.SoapSaveAllowances(commonEventType);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public void DeleteAllowances(int commonDealId)
//        {
//            logger.LogMethoInfo();

//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.DeleteAllowances(commonDealId);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public void DeleteAllowanceById(int Id)
//        {
//            logger.LogMethoInfo();

//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.DeleteAllowanceById(Id);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        #endregion

//        public AppUser GetUserFromLoginName(string loginName)
//        {
//            logger.LogMethoInfo();

//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    AppUser appUser = proxy.SoapGetUserFromLoginName(loginName);

//                    logger.LogReturnValueInfo(appUser);

//                    return appUser;
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//            return null;
//        }

//        public List<Party> GetRepsForRepAdmin(int repAdminPartyId)
//        {
//            logger.LogMethoInfo();

//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    List<Party> parties = proxy.GetRepsForRepAdmin(repAdminPartyId);

//                    logger.LogReturnValueInfo(parties);

//                    return parties;
//                }


//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//            return null;
//        }

//        public void UpdateDealSponsor(int dealId, DealSponsor sponsor, bool isDSDDeal)
//        {
//            logger.LogMethoInfo();
//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.UpdateDealSponsor(dealId, sponsor,isDSDDeal);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }

//        }
//        public void LocationBatchUpdate(DealLocationsUpdateRequest locationBatchUpdateRequest)
//        {
//            logger.LogMethoInfo();
//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.LocationBatchUpdate(locationBatchUpdateRequest);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }

//        }
//        public void UpdateRUOField(UpdateRUOFieldRequest request)
//        {
//            logger.LogMethoInfo();
//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.UpdateRUOField(request);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }

//        }
//        public void UpdateOSADates(UpdateOSADatesRequest updateOSADatesRequest)
//        {
//            logger.LogMethoInfo();
//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.UpdateOSADates(updateOSADatesRequest);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }

//        }
//        public void UpdateItemRetailerCostAndMatchDescription(UpdateItemRetailerCostAndMatchDescriptionRequest request)
//        {
//            logger.LogMethoInfo();
//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.UpdateItemRetailerCostAndMatchDescription(request);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }

//        }
//        public void CalculateEstimatedValues(int masterDealId)
//        {
//            logger.LogMethoInfo();
//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.CalculateEstimatedValues(masterDealId);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }

//        }
//        public void CalculdateOSADates(CalculdateOSADatesRequest request)
//        {
//            logger.LogMethoInfo();
//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.CalculdateOSADates(request);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }

//        }
//        public void UpdateDealItems(int dealId, DealItemsUpdateRequest items)
//        {
//            logger.LogMethoInfo();
//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.UpdateDealItems(dealId, items);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }

//        }

//        public List<string> GetARCustomerNumbers(int retailerPartyId, int vendorPartyId)
//        {
//            logger.LogMethoInfo();

//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    List<string> customerNumbers = proxy.GetARCustomerNumbers(retailerPartyId, vendorPartyId);

//                    logger.LogReturnValueInfo(customerNumbers);

//                    return customerNumbers;
//                }


//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//            return null;
//        }

//        public List<string> GetAPPayeeNumbers(int retailerPartyId, int vendorPartyId)
//        {
//            logger.LogMethoInfo();

//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    List<string> customerNumbers = proxy.GetAPPayeeNumbers(retailerPartyId, vendorPartyId);

//                    logger.LogReturnValueInfo(customerNumbers);

//                    return customerNumbers;
//                }


//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//            return null;
//        }

//        public List<string> GetAPVendorNumbers(int retailerPartyId, int vendorPartyId)
//        {
//            logger.LogMethoInfo();

//            try
//            {

//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    List<string> aPVendorNumbers = proxy.GetAPVendorNumbers(retailerPartyId, vendorPartyId);

//                    logger.LogReturnValueInfo(aPVendorNumbers);

//                    return aPVendorNumbers;
//                }


//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//            return null;
//        }


//        public void DeleteLocations(List<int> locationDealIds)
//        {
//            logger.LogMethoInfo();
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.DeleteLocations(locationDealIds);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }


//        public void AddStoreGroups(AddStoreGroupsRequest storeGroupsToAdd)
//        {
//            logger.LogMethoInfo();
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.AddStoreGroups(storeGroupsToAdd);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public void AddStore(AddStoresRequest storesToAdd)
//        {
//            logger.LogMethoInfo();
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.AddStores(storesToAdd);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public void AddStoreDivisional(AddStoresRequest storesToAdd)
//        {
//            logger.LogMethoInfo();
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.AddStoresDivisional(storesToAdd);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public void DeleteStoresAndGroupsDivisional(DeleteStoresAndGroupsRequest storesAndGroupsToDelete)
//        {
//            logger.LogMethoInfo();
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                        proxy.DeleteStoresAndGroupsDivisional(storesAndGroupsToDelete);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public void DeleteStoresAndGroups(DeleteStoresAndGroupsRequest storesAndGroupsToDelete)
//        {
//            logger.LogMethoInfo();
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.DeleteStoresAndGroups(storesAndGroupsToDelete);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }
//        public void SaveItemsRUOFieldsForAWG(int commondealID, List<RetailerItemExtension_AWG> awgRetailerItemsExtn, List<int> divisions)
//        {
//            logger.LogMethoInfo();
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.SaveRetailerItemExtensionAWG(commondealID, awgRetailerItemsExtn, divisions);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public void SaveItemsRUOFieldsForANP(int commondealID, List<RetailerItemExtension_ANP> anpRetailerItemsExtn, List<int> divisions)
//        {
//            logger.LogMethoInfo();
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.SaveRetailerItemExtensionANP(commondealID, anpRetailerItemsExtn, divisions);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public void SaveItemsRUOFieldsForSAMS(int commondealID, List<RetailerItemExtension_SAMS> samsRetailerItemsExtn, List<int> divisions)
//        {
//            logger.LogMethoInfo();
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.SaveRetailerItemExtensionSAMS(commondealID, samsRetailerItemsExtn, divisions);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public void DeleteItemsRUOFields(int commondealID, List<RetailerEntityExtensionId> retailerItemsExtnIds, List<int> divisions)
//        {
//            logger.LogMethoInfo();
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.DeleteRetailerItemExtension(commondealID, retailerItemsExtnIds, divisions);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public void ClearItemsRUOFields(int commondealID)
//        {
//            logger.LogMethoInfo();
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    proxy.ClearRetailerItemExtension(commondealID);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        #region Multiple Product Category Manager
//        public List<DealSubmitRouteInformation> GetAllDealsSubmitRoute(List<int> commondealIDs)
//        {
//            logger.LogMethoInfo();
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    return proxy.GetRouteOnDealSubmit(commondealIDs);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        public List<CategoryBuyerPair> GetBuyerAndCategoryPairsList(int retailerPartyId)
//        {
//            logger.LogMethoInfo();
//            try
//            {
//                using (var proxy = factory.CreateDealsProxy())
//                {
//                    return proxy.GetCategoryBuyerPairs(retailerPartyId);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex);
//                throw ex;
//            }
//        }

//        #endregion

//        public void SetDefaultAddress(PartyAddressRequest MfrAddress, PartyAddressRequest VndrAddress, int retailpartyid)
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                proxy.SetSponsorDefaults(MfrAddress, VndrAddress, retailpartyid);
//            }
//        }

//        public void UpdateCommonDealDefaultAddress(int mfrPartyId, int vendorPartyId, int dealId)
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                proxy.UpdateDefaultAddressForCommonDeal(mfrPartyId, vendorPartyId, dealId);
//            }
//        }

//        public DealSponsor GetDealSponsor(int dealId)
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                return proxy.GetDealSponsor(dealId);
//            }
//        }

//        #region Performance Allowance 

//        public void DeletePerformanceAllowance(int rcEventTypeId, List<int> performanceId)
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                proxy.DeletePerformanceForAllowance(rcEventTypeId, performanceId);
//            }
        
//        }

//        public void DeleteAllowanceForPerformance(int performanceId, List<int> rcEventtypeId)
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                proxy.DeleteAllowanceForPerformance(performanceId, rcEventtypeId);
//            }  
//        }

//        public DTOPageOfAllowanceDefinition GetAllowancesForPerformance(int performanceId, int pageIndex, int pageSize)
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                DTOPageOfAllowanceDefinition x =  proxy.GetAllowancesForPerformance(performanceId, pageIndex, pageSize);
//                return x;

//            }
//        }

//        public DTOPageOfAllowanceDefinition GetAllowancesNotAddedToPerformance(int performanceId, int pageIndex, int pageSize)
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                DTOPageOfAllowanceDefinition x = proxy.GetAllowancesNotAddedToPerformance(performanceId, pageIndex, pageSize);
//                return x;

//            }
//        }

//        public DTOPageOfPerformanceNameDate GetPerformancesForAllowance(int rcEventTypeId, int pageIndex, int pageSize)
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                DTOPageOfPerformanceNameDate x = proxy.GetPerformancesForAllowance(rcEventTypeId, pageIndex,
//                                                                                            pageSize);
//                return x;
//            }
//        }

//        public DTOPageOfPerformanceNameDate GetPerformancesNotAddedForAllowance(int rcEventTypeId, int pageIndex, int pageSize)
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                DTOPageOfPerformanceNameDate x = proxy.GetPerformancesNotAddedForAllowance(rcEventTypeId, pageIndex,
//                                                                                            pageSize);
//                return x;
//            }
//        }

//        public void AssociateAllowancesToPerformance(int performanceId, List<int> allowanceIds)
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                proxy.AssociateAllowancesToPerformance(performanceId, allowanceIds);
//            };
//        }

//        public void AssociatePerfromancesToAllowance(int allowanceId, List<int> performanceIds)
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                proxy.AssociatePerfromancesToAllowance(allowanceId, performanceIds);
//            };
//        }

//        public void AssociateAllowanceToAllPerformances(int allowanceId)
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                proxy.AssociateAllowanceToAllPerformances(allowanceId);
//            }
//        }

//        public void AssociatePerformanceToAllAllowances(int performanceId)
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                proxy.AssociatePerformanceToAllAllowances(performanceId);
//            }
//        }

//        #endregion


//        public string GetVersion()
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                return proxy.GetVersion();
//            }
//        }

//        public string GetConfigInfo()
//        {
//            using (var proxy = factory.CreateDealsProxy())
//            {
//                return proxy.GetConfigInfo();
//            }
//        }


//    }
//}


