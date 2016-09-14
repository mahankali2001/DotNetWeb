using System;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace SampleApp
{
    public class GlobalBase : HttpApplication
    {
        #region Members

        //private readonly ILogger logger = LogManager.GetLogger(typeof(GlobalBase).FullName);
        private string myFacadeCacheId;
       // private IOUtility myIOUtility;
        private CacheItemRemovedCallback myOnRemove;

        #endregion

        #region Constructors

        public GlobalBase()
        {
            EndRequest += new EventHandler(GlobalBase_EndRequest);
        }

        #endregion

        #region Destructor

        ~GlobalBase()
        {
            Dispose(false);
        }

        #endregion

        #region Private methods

        private void GlobalBase_EndRequest(object sender, EventArgs e)
        {
            //logger.Debug("GlobalBase_EndRequest() called.");
            //GetFacade().ApplicationContext.Database.CloseConnection();
            //GetFacade().ApplicationContext.Database.DbRetrievalStamp = Convert.ToDateTime(null);
        }

        #endregion

        #region Protected methods

        protected void Dispose(bool disposing)
        {
            
            if (disposing)
            {
                base.Dispose();
            }
        }

        #endregion

        #region Properties

        private string FacadeCacheId
        {
            get
            {
                if (myFacadeCacheId == null)
                {
                    int num1 = GetNextCacheId();
                    myFacadeCacheId = "Facade" + num1.ToString();
                }

                return myFacadeCacheId;
            }
        }

        
        #endregion

        #region Virtual methods

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            //logger.Debug("*** Application begin request. ***");


            // -----------------------------------------------------------------------
            // Block incoming HTTP GET requests which contains webform POST Parameters
            // -----------------------------------------------------------------------
            if (Request.HttpMethod == "GET")
            {
                // if contains ASP.NET post parameters
                if( (Request.QueryString["__EVENTTARGET"] ??
                                     Request.QueryString["__VIEWSTATE"] ??
                                     Request.QueryString["__EVENTVALIDATION"] ??
                                     Request.QueryString["__EVENTARGUMENT"]) != null)
                {
                    throw new ApplicationException(string.Format("DM - Suspicious GET Request, URL : {0}", Request.Url.ToString()));
                }
            }
            // -----------------------------------------------------------------------

            if (Request.Url.Scheme.ToLower() == "https")
                Response.AddHeader("Strict-Transport-Security", "max-age=31536000");

            var url = Request.Url.ToString().ToLower();

            if(url.Contains(".cshtml") || url.Contains(".json") || url.Contains(".axd") )
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
            }
        }

        protected virtual void Application_EndRequest(Object sender, EventArgs e)
        {
            //logger.Debug("*** Application end request. ***");
        }

        protected virtual void Application_Start(object sender, EventArgs e)
        {
            //logger.Debug("*** Application start. ***");
            //Clear SQL Connection Pools
            Application.Lock();
            Application.UnLock();
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlConnection.ClearPool(conn);
            conn.Close();
        }

        protected virtual void Application_End(object sender, EventArgs e)
        {
            //logger.Debug("*** Application end. ***");
            IEnumerator enumerator1 = null;
            try
            {
                enumerator1 = Application.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    string text1 = Convert.ToString(enumerator1.Current);
                    object obj1 = Application[text1];
                    MethodInfo info1 = obj1.GetType().GetMethod("Dispose");
                    if (info1 != null)
                    {
                        info1.Invoke(obj1, null);
                    }
                }
            }
            finally
            {
                if (enumerator1 is IDisposable)
                {
                    ((IDisposable)(enumerator1)).Dispose();
                }
            }

            foreach (string s in Application.AllKeys)
            {
               // logger.Debug("Application Key: " + s);
            }
            Application.RemoveAll();
        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {
            //logger.Debug("*** Application error. ***");
            //ProjectPageBase ProjectPageBase = new ProjectPageBase();
            //ProjectPageBase.PMAppError.AddProcessList("Unhandled Application error encountered.");
            StringBuilder exceptionMessage = new StringBuilder();
            exceptionMessage.AppendLine("GlobalException:");
            exceptionMessage.AppendLine(Server.GetLastError().GetBaseException().ToString());
            if (Server.GetLastError().GetBaseException().InnerException == null)
                exceptionMessage.AppendLine("Inner exception: Null");
            else
            {
                exceptionMessage.AppendLine("Inner exception: ");
                exceptionMessage.AppendLine(Server.GetLastError().GetBaseException().InnerException.ToString());
            }
            //logger.Error(exceptionMessage.ToString());
            if (Application.Count == 0)
            {
                Exception ex = Server.GetLastError().GetBaseException();
                //ProjectPageBase.ApplicationExceptionHandler(ex, "default.aspx", "Global.Application_Error", true, "");
            }
            else
            {
                Exception transTemp0 = Server.GetLastError().GetBaseException();
                //ProjectPageBase.ApplicationExceptionHandler(transTemp0, "default.aspx", "Global.Application_Error", "");
            }
            //ProjectPageBase.DisplayApplicationException();
        }

        protected virtual void Session_Start(object sender, EventArgs e)
        {
            //logger.Debug("*** Session start. ***");
        }

        protected virtual void Session_End(object sender, EventArgs e)
        {
            //logger.Debug("*** Session end. ***");
            if ((string.Compare(Session.SessionID, string.Empty, false) != 0) & (ConfigurationManager.AppSettings.GetValues("ReportOutputPath") != null))
            {
                string text1 = ConfigurationManager.AppSettings.GetValues("ReportOutputPath")[0] + Session.SessionID;
                //IO.DeleteDirectory(text1, true);
            }
        }

        #endregion

        #region Public methods

        

        public int GetNextCacheId()
        {
            int num1 = 0;
            Application.Lock();
            try
            {
                if (Application["LastCacheId"] != null)
                {
                    num1 = Convert.ToInt32(Application["LastCacheId"]);
                }

                num1 += 1;
                Application["LastCacheId"] = num1;
            }
            finally
            {
                Application.UnLock();
            }

            return num1;
        }

        

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        #endregion
    }
}
