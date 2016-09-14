using System;
using System.Collections.Generic;
using Demandtec.DealManagement.Contracts.Data;
using Demandtec.DealManagement.Services.Client.Reference;
namespace Demandtec.DealManagement.Services.Client.Proxy
{
    public class DllDealsClient : IDealsClient
    {
        #region Implementation of IDeals

        public List<CommonDealPackage> GetPackages(int commonDealId)
        {
            throw new NotImplementedException();
        }

        public List<CommonDealPackage> SavePackages(List<CommonDealPackage> commonDealPackages)
        {
            throw new NotImplementedException();
        }

        public void ValidatePackageDeal(int masterCommonDealId)
        {
            throw new NotImplementedException();
        }

        public void ValidateSpecificPackageDeal(int masterCommonDealId, PackageType packageType)
        {
            throw new NotImplementedException();
        }

        public void DeletePackage(int commonDealPackageId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllPackagesForMasterDeaId(int masterCommonDealId)
        {
            throw new NotImplementedException();
        }

        public void CanItemsBeDeletedFromDeal(int commonDealId, List<int> retailItemIds)
        {
            throw new NotImplementedException();
        }

        public List<IdValuePair> GetAllAllowanceType()
        {
            throw new NotImplementedException();
        }

        public List<IdValuePair> GetAllProductSetRelationType()
        {
            throw new NotImplementedException();
        }

        public List<IdValuePair> GetAllPackageRelationType()
        {
            throw new NotImplementedException();
        }

        public List<IdValuePair> GetAllProductSetRelationTypeWithGet()
        {
            throw new NotImplementedException();
        }

        public bool IsProductSetNameAvailable(int commonDealId, string productSetName)
        {
            throw new NotImplementedException();
        }

        public ProductSet CreateOrUpdateProductSet(int commonDealId, ProductSet productSet)
        {
            throw new NotImplementedException();
        }

        public ProductSetWithAllItems GetProductSetWithAllItemsUsingId(int commonDealId, int productSetID)
        {
            throw new NotImplementedException();
        }

        public ProductSetWithAllItems GetProductSetWithAllItems(int commonDealId, string productSetName)
        {
            throw new NotImplementedException();
        }

        public ProductSet GetProductSet(int productSetID)
        {
            throw new NotImplementedException();
        }

        public List<IdValuePair> GetAllQuantityType()
        {
            throw new NotImplementedException();
        }

        public DealEventResponse GetDealEvent(int commonDealId)
        {
            throw new NotImplementedException();
        }

        public void SaveDealEvent(DealEventRequest dealEvent, int commonDealId)
        {
            throw new NotImplementedException();
        }

        public void SaveAllowances(List<AllowanceRequest> allowances, int commonDealId)
        {
            throw new NotImplementedException();
        }

        public void SaveAllowance(AllowanceRequest allowance, int commonDealId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllowances(int commonDealId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllowanceById(int Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateDealSponsor(int dealId, DealSponsor sponsor)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAPVendorNumbers(int retailerPartyId, int vendorPartyId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetARCustomerNumbers(int retailerPartyId, int vendorPartyId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAPPayeeNumbers(int retailerPartyId, int vendorPartyId)
        {
            throw new NotImplementedException();
        }

        public List<AllowanceResponse> GetAllowances(int commonDealId)
        {
            throw new NotImplementedException();
        }

        public List<AllowanceResponse> GetItemAllowances(int commonDealId)
        {
            throw new NotImplementedException();
        }

        public List<AllowanceResponse> GetFlatAllowances(int commonDealId)
        {
            throw new NotImplementedException();
        }

        public List<IdValuePair> LookupAllowanceEventTypes(AllowanceTypeBase allowanceBase, int? eventCategory)
        {
            throw new NotImplementedException();
        }

        public List<AllowanceEventType> GetAllowanceEventTypes(AllowanceTypeBase allowanceBase, int? eventCategory)
        {
            throw new NotImplementedException();
        }

        public AllowanceEventType GetAllowanceEventType(int allowanceTypeId)
        {
            throw new NotImplementedException();
        }

        public List<CodeValuePair> LookupBasis(int allowanceEventTypeId)
        {
            throw new NotImplementedException();
        }

        public List<CodeValuePair> LookupPaymentMethod(int allowanceEventTypeId)
        {
            throw new NotImplementedException();
        }

        public List<PerformanceNameDate> GetPerformanceNameDates(int retailPartyId, int? parentRetailPartyId, string NameStartsWith)
        {
            throw new NotImplementedException();
        }

        public AppUser GetUserFromLoginName(string loginName)
        {
            throw new NotImplementedException();
        }

        public List<Party> GetRepsForRepAdmin(int repAdminPartyId)
        {
            throw new NotImplementedException();
        }

        public List<Party> GetVendors(int repPartyId)
        {
            throw new NotImplementedException();
        }

        public List<Party> GetManufacturers(int repPartyId, int vendorPartyId)
        {
            throw new NotImplementedException();
        }

        public List<Party> GetRetailers(int manufacturerPartyId)
        {
            throw new NotImplementedException();
        }

        public void ValidateNewDealItems(int vendorPartyId, int retailerPartyId, List<AllowanceRequest> itemAllowanceRequests, DealItemsRequest items)
        {
            throw new NotImplementedException();
        }

        public void ValidateExistingDealItems(int dealId, List<AllowanceRequest> itemAllowanceRequests, DealItemsRequest items)
        {
            throw new NotImplementedException();
        }

        public void ValidateDeleteDealItems(int dealId, List<int> itemIds)
        {
            throw new NotImplementedException();
        }

        public void ValidateDealEvent(DealEventRequest dealEvent)
        {
            throw new NotImplementedException();
        }

        public void ValidateAllowances(List<AllowanceRequest> allowances)
        {
            throw new NotImplementedException();
        }

        public void ValidateAllowance(AllowanceRequest allowance)
        {
            throw new NotImplementedException();
        }

        public void ValidateDealRequest(DealRequest deal)
        {
            throw new NotImplementedException();
        }

        public string GetDeals(int value)
        {
            throw new NotImplementedException();
        }

        public void DeleteDeal(int commonDealId, int userId)
        {
            throw new NotImplementedException();
        }

        public string CreateDeal(DealRequest deal)
        {
            throw new NotImplementedException();
        }

        public DealResponse GetOffer(string offerNumber)
        {
            throw new NotImplementedException();
        }

        public void ValidateDealSubVendorDivisions(int retailerPartyId, int vendorPartyId, System.Collections.Generic.List<int> vendorRetailerIds)
        {
            throw new NotImplementedException();
        }

        public void ValidateDealRetailerDivisions(int retailerPartyId, System.Collections.Generic.List<int> divisionRetailerPartyIds)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<Demandtec.DealManagement.Services.Client.Reference.RetailDealVendorItem> FindVendorItems(int vendorPartyId, int repPartyId, Demandtec.DealManagement.Services.Client.Reference.RetailDealVendorItemSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<Demandtec.DealManagement.Services.Client.Reference.RetailDealRetailItem> FindRetailItems(int retailerPartyId, Demandtec.DealManagement.Services.Client.Reference.RetailDealRetailItemSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<Demandtec.DealManagement.Services.Client.Reference.RetailerDivision> FindRetailerDivisions(int retailerPartyId, Demandtec.DealManagement.Services.Client.Reference.RetailerDivisionSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<Demandtec.DealManagement.Services.Client.Reference.SubVendorDivision> FindSubVendorDivisions(int retailerPartyId, int vendorPartyId, Demandtec.DealManagement.Services.Client.Reference.SubVendorDivisionSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public void DeleteDealItems(int dealId, System.Collections.Generic.List<int> itemIds)
        {
            throw new NotImplementedException();
        }

        public void UpdateDealItems(int dealId, Demandtec.DealManagement.Services.Client.Reference.DealItemsUpdateRequest items)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
