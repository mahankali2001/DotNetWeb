using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using SampleApp.Services.Client.Logger;
using SampleApp.Services.Client.Model;
using SampleApp.Services.Client.OAuth;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

namespace SampleApp.Services.Client
{
    public class Rest : IDisposable
    {

        //private static readonly SampleApp.Core.Logger.ILogger logger = SampleApp.Core.Logger.LogManager.GetLogger("Rest");


        #region Member Variables

        private static volatile Rest _instance;
        private static readonly object SyncRoot = new Object();
        private readonly ServiceClientLogger _restlogger = new ServiceClientLogger();

        #endregion

        #region Constructor & Destructors

        private static readonly string URI = ConfigurationManager.AppSettings["RESTApiUri"];
        private RestClient _client = new RestClient();

        private Rest()
        {
        }

        public static Rest Instance
        {
            get { return new Rest(); }
        }

        public void Dispose()
        {
            this._client = null;
        }

        #endregion

        private void SetHeaders(RestRequest request, Uri uri, string httpMethod)
        {
            string o = OAuthBase.Instance.GenerateSignatureForDMWebClient(uri, httpMethod);
            request.AddHeader("OAuth", o);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            //request.AddHeader("retailerCode", Deals.Header.RetailerCode);
            //request.AddHeader("cultureCode", Deals.Header.CultureInfo);
            //request.AddHeader("userName", Deals.Header.UserName);
            //request.AddHeader("appUserId", Deals.Header.AppUserId.ToString(CultureInfo.InvariantCulture));
            //request.AddHeader("partyId", Deals.Header.PartyId.ToString(CultureInfo.InvariantCulture));
            request.AddHeader("retailerCode", "XYZ_DEV_00");
            request.AddHeader("cultureCode", "en-US");
            request.AddHeader("userName", "");
            request.AddHeader("appUserId", "2");
            request.AddHeader("partyId", "2");
        }

        private void SetHeadersWithoutSession(RestRequest request, Uri uri, string httpMethod)
        {
            string o = OAuthBase.Instance.GenerateSignatureForDMWebClient(uri, httpMethod);
            request.AddHeader("OAuth", o);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("retailerCode", ConfigurationManager.AppSettings["RetailerCode"]); 
            request.AddHeader("cultureCode", "en-US"); 
            request.AddHeader("userName", "admin"); 
        }

        public string Test(string test)
        {
            string uri = URI + "/Analytics/Test/{0}";
            const Method m = RestSharp.Method.GET;
            var request = new RestRequest(string.Format(uri, System.Web.HttpUtility.UrlEncode(test)), m);
            SetHeaders(request, new Uri(uri), m.ToString());

            var c = _client.Execute(request);
            return c.Content;
        }
        
        public string GetUsers()
        {
            string uri = string.Empty;
            uri = String.Format("{0}/app/users/", URI);
            const Method m = Method.GET;
            var request = new RestRequest(uri, m);
            SetHeaders(request, new Uri(uri), m.ToString());

            //var ser = new DataContractJsonSerializer(typeof(RetailerGetRequest));
            //var mem = new MemoryStream();
            //ser.WriteObject(mem, req);
            //string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            //string d = string.Format("{{\"req\":{0}}}", data);
            request.RequestFormat = DataFormat.Json;
            //request.AddParameter("text/json", d, ParameterType.RequestBody);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var content = _client.Execute(request);
            var result = content.Content.ToString();
            return result;
        }
        public string GetUser(string uid)
        {
            string uri = string.Empty;
            uri = string.Format("{0}/app/users/{1}/", URI, uid);
            const Method m = Method.GET;
            var request = new RestRequest(uri, m);
            SetHeadersWithoutSession(request, new Uri(uri), m.ToString());
            request.RequestFormat = DataFormat.Json;
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            IRestResponse content = _client.Execute(request);
            if (content == null) throw new ArgumentNullException("content");
            var result = content.Content.ToString(CultureInfo.InvariantCulture);
            return result;
        }


        public void DeleteUser(string uid)
        {
            string uri = string.Empty;
            uri = string.Format("{0}/app/users/{1}/", URI, uid);
            const Method m = Method.DELETE;
            var request = new RestRequest(uri, m);
            SetHeadersWithoutSession(request, new Uri(uri), m.ToString());
            request.RequestFormat = DataFormat.Json;
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            IRestResponse content = _client.Execute(request);//
            if (content == null) throw new ArgumentNullException("content");
            //var result = content.Content.ToString(CultureInfo.InvariantCulture);
            //return result;
        }


        public string SaveUsers(UserRequest req)
        {
            string uri = string.Empty;
            uri = String.Format("{0}/app/users/input/", URI);
            const Method m = Method.POST;
            var request = new RestRequest(uri, m);
            SetHeaders(request, new Uri(uri), m.ToString());

            var ser = new DataContractJsonSerializer(typeof(UserRequest));
            var mem = new MemoryStream();
            ser.WriteObject(mem, req);
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            string d = string.Format("{{\"req\":{0}}}", data);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("text/json", d, ParameterType.RequestBody);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var content = _client.Execute(request);
            var result = content.Content.ToString();
            return result;
        }
    }
}
