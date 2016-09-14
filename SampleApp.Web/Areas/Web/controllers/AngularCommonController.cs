using System.Collections;
using System.Data;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System;
using System.Web.Compilation;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace AppWeb.Controllers
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.HttpContext.Response.Cache.SetNoStore();
            base.OnResultExecuting(context);
        }
    }

    [NoCacheAttribute]
    public abstract class AngularCommonController : Controller
    {
        //private static readonly ILogger logger = LogManager.GetLogger("ProjectPageBase");
        //private ICache sessionCache = null;
        //public ICache SessionCache
        //{
        //    get
        //    {
        //        this.sessionCache = this.sessionCache ?? AppCache.GetSessionCache();
        //        return this.sessionCache;
        //    }
        //}

        public void SetCultureInfo(string cultureCode)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureCode);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);
        }

       

        public JsonResult IncreasedJsonResponse(JsonResult result)
        {
            result.MaxJsonLength = Int32.MaxValue;
            return result;
        }

        protected bool IsValidReferrer()
        {
            bool throwReferrerIssue = false;
            if (Request.UrlReferrer == null)
                return true;
            if (string.IsNullOrEmpty(Request.UrlReferrer.Host))
                return true;
            if (Request.UrlReferrer.Host.IndexOf("localhost", StringComparison.OrdinalIgnoreCase) >= 0)
                return true;

            string a = Request.UrlReferrer.Host;
            a = a.ToUpper();
            if (a.Contains("XYZ") || a.Contains("ABC"))
                return true;


            try
            {
                IPAddress address = IPAddress.Parse(Request.UrlReferrer.Host);
                return true;
            }
            catch (Exception)
            { }
            if (Request.UrlReferrer.Host.IndexOf("bogus", StringComparison.OrdinalIgnoreCase) >= 0)
                throwReferrerIssue = true;
            else if (Request.UrlReferrer.Host.IndexOf("xyz.com", StringComparison.OrdinalIgnoreCase) < 0)
                throwReferrerIssue = true;

            if (throwReferrerIssue)
            {
                //if (Request.Url != null)
                //    logger.Error("REFERRERRR: Request.UrlReferrer.Host >>|" + Request.UrlReferrer.Host + "| Request.Url.Host >>|" + Request.Url.Host + "|");
                return false;
            }
            return true;
        }

        public enum DsdFlag
        {
            OnlyNonDSD = 0,
            OnlyDsd = 1,
            BothDsdNonDsd = 2
        }

        public enum Modules
        {
            UserManagement = 1
        }

        public List<PartyElement> GetPCList(string vendoruid, string retailuid, string pcName, string itemNumber, string UpcConsumer,Modules bmName)
        {
            List<PartyElement> pcs = new List<PartyElement>();
            
            return pcs;
        }

        public string GetTempFileName(string sFileName)
        {
            string sFolder = ConfigurationManager.AppSettings["ReportOutputPath"];

            if (sFolder == null)
            {
                sFolder = @"\";
            }
            else if (!(sFolder.EndsWith(@"\")))
            {
                sFolder += @"\";
            }

            if (!string.IsNullOrEmpty(sFolder) && !Directory.Exists(sFolder))
                Directory.CreateDirectory(sFolder);

            return Convert.ToString(sFolder + sFileName);
        }

        public List<SelectListItem> GetRetailers()
        {

            var list = new List<SelectListItem>();

            


            DataTable dtRetailers = new DataTable();
            //using (var s = DBAccess.CreateCommand("Party_ActiveCorpRetailer"))
            //{
            //    s.CommandType = CommandType.StoredProcedure;
            //    s.Parameters.Clear();
            //    DBAccess.FillTable(dtRetailers, s);
            //}

            int partyId;
            //if (dtRetailers != null && dtRetailers.Rows.Count > 0)
            //{

            //    foreach (DataRow drRetailer in dtRetailers.Rows)
            //    {

            //        partyId = DataUtility.GetColumnValueAsInt(drRetailer, "PartyId");
            //        if (isSysAdmin || retailersForCurrentUser.IndexOf(partyId) > -1)
            //        {
            //            list.Add(
            //                new SelectListItem
            //                {
            //                    Value = partyId.ToString(),
            //                    Text = DataUtility.GetColumnValueAsString(drRetailer, "FullName"),
            //                    Selected = (partyId == selectedRetailer)
            //                });

            //        }
            //    }
            //}

            return list;

        }
    }

    public class PartyElement
    {
        public PartyElement() { }
        public string ID;
        public string Name;
        public string ChildId;
    }

    public class itemSearchFilter
    {
        public itemSearchFilter() { }
        public string FieldName;
        public string SearchValue;
    }

    
    public class AjaxCallResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    
        public static AjaxCallResult GetSuccess()
        {
            return new AjaxCallResult()
                       {
                           Success = true,
                           Message = ""
                       };
        }
        public static AjaxCallResult SessionTimeOut()
        {
            return new AjaxCallResult()
            {
                Success = false,
                Message = "Session timed out"
            };
        }
        public static AjaxCallResult UnAthrorized()
        {
            return new AjaxCallResult()
            {
                Success = false,
                Message = "Can not access this page"
            };
        }
        public static AjaxCallResult UnknownError()
        {
            return new AjaxCallResult()
            {
                Success = false,
                Message = "Error while fetchig data"
            };
        }
        public static AjaxCallResult CustomError(string error)
        {
            return new AjaxCallResult()
            {
                Success = false,
                Message = error
            };
        }
    }



}