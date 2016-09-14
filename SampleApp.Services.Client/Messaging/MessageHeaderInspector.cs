using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using SampleApp.Contracts.Data;
using SampleApp.Services.Core;

namespace SampleApp.Services.Client
{
    public class MessageHeaderInspector : IClientMessageInspector
    {
        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            return;
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            //var e = Deals.Header;
            //if (e == null)
            //{
            //    e = new Header
            //            {
            //                //Ramesh : Commented to avoid defaulting to another DB

            //                //CultureInfo = Thread.CurrentThread.CurrentCulture.ToString(),
            //                //Token = Guid.Empty.ToString(),
            //                //UserName = RetrieveUserName(),
            //                //RetailerCode = String.IsNullOrEmpty(ConfigurationManager.AppSettings["RetailerCode"])
            //                //                   ? "KR_DEV_00"
            //                //                   : ConfigurationManager.AppSettings["RetailerCode"].Trim().ToUpper(),
            //                //AppUserId = 1,
            //                //PartyId = 2
            //            };
            //}
            //ValidateHeader(e);
            //var context = new GenericContext<Header>(e);
            //var genericHeader = new MessageHeader<GenericContext<Header>>(context);
            //request.Headers.Add(genericHeader.GetUntypedHeader(GenericContext<Header>.TypeName,
            //                                                   GenericContext<Header>.TypeNamespace));
            return null;
        }

        private void ValidateHeader(Header header)
        {
            if (header == null)
                throw new NullReferenceException(
                    "SOA Header Exception : Header object is set to null MessageHeaderInspector -> BeforeSendRequest() ");
            if (header.AppUserId <= 0)
                throw new ArgumentException(
                    "SOA Header Exception : AppuserId Cannot be less than 0 - MessageHeaderInspector -> BeforeSendRequest() ");
            if (string.IsNullOrEmpty(header.UserName))
                throw new ArgumentException(
                    "SOA Header Exception : Username cannot be null or empty  - MessageHeaderInspector -> BeforeSendRequest() ");

        }

        #endregion

        /// <summary>
        /// For setting the username from Console / Window Application 
        ///     Console / Window  application Username can be set
        ///         GenericIdentity identity = new GenericIdentity(userName);
        ///         Thread.CurrentPrincipal = new GenericPrincipal(identity, new string[] {});
        ///         Thread.CurrentThread.CurrentCulture = new CultureInfo("en-IN");
        /// For setting the Username from Web Application 
        ///     Web applicateion can set the username using the HTTPContext 
        ///         Either the username can be set 
        ///             HttpContext.Current.Session["UserName"]  = "secretAgent"
        ///         Or
        ///             HttpContext.Current.Session[UsernameKey From the Configuration file]  = "secretAgent"
        /// </summary>
        /// <returns>string UserName</returns>

        private string RetrieveUserName()
        {
            //difference between web and desktop environments
            string userName = string.Empty;
            if (HostingEnvironment.IsHosted)
            {
                if (OperationContext.Current != null)
                    userName = OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
                else
                {
                    if (HttpContext.Current != null)
                    {
                        userName = HttpContext.Current.User.Identity.Name;
                        if (String.IsNullOrEmpty(userName))
                        {
                            //Check for the httpcontext user that would be set from the WEB api 
                            if (HttpContext.Current.Session["UserName"] != null)
                                userName = HttpContext.Current.Session["UserName"].ToString();
                                //Get the username based on the application session variable that was set.
                            else
                            {
                                string applicationSession = ConfigurationManager.AppSettings["UserNameKey"];
                                if (!String.IsNullOrEmpty(applicationSession))
                                    if (HttpContext.Current.Session[applicationSession] != null)
                                        userName = HttpContext.Current.Session[applicationSession].ToString();
                            }
                        }
                    }
                }
            }
            else
                userName = Thread.CurrentPrincipal.Identity.Name;

            return userName;
        }
    }
}