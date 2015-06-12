using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace Pharmacy.Controllers
{

    [Authorize]
    public class AuthorizationManagementController : Controller
    {
        private static OauthConfiguration _instanceOauthConfiguration;

        public static ValDict ReturnVal(string email, string scope)
        {
            var key = new KeyDict(email, scope);
            if (!GetInstance().Dict.ContainsKey(key))
                throw new InvalidOperationException();
            ValDict val;
            _instanceOauthConfiguration.Dict.TryGetValue(key, out val);
            return val;
        }

        public static void Reset()
        {

            GetInstance().Dict.Clear();
        }

        private static void Update(Dictionary<KeyDict, ValDict> dict)
        {
            _instanceOauthConfiguration.Dict = dict;
        }
        private static OauthConfiguration GetInstance()
        {
            return _instanceOauthConfiguration ?? (_instanceOauthConfiguration = new OauthConfiguration());
        }
        //
        // GET: /AuthorizationManagement/
        public ActionResult Index(string code, string state)
        {
            WebRequest request = WebRequest.Create("https://dione.disi.unige.it/oauth/v2/oauth/token");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("SSD_PharmacyApp:7e0e9979f96d797400ed5e6503f81964"));
            string postData = "grant_type=authorization_code&code=" + code + "&redirect_uri=http://localhost:55555/AuthorizationManagement";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            //accetta tutti i certificati, altrimenti solleva eccezione dicendo che trova problemi con sistemi ssl/tsl
            ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStreamResponse = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStreamResponse);
            string responseFromServer = reader.ReadToEnd();
            JObject bigToken = JObject.Parse(responseFromServer);

            JObject accessToken = Base64DecodeJson(bigToken["access_token"].ToString());
            JObject token = JObject.Parse(accessToken.ToString());
            var individualTokens = token.GetEnumerator();
            _instanceOauthConfiguration = GetInstance();
            while (individualTokens.MoveNext())
            {
                string uri = individualTokens.Current.Key;
                var individualToken = individualTokens.Current.Value;
                string tokenPortion = individualToken["token_portion"].ToString();
                var scopes = individualToken["scopes"].ToString().Split(' ');
                var dicUpdate = new Dictionary<KeyDict, ValDict>();
                foreach (var scope in scopes)
                {
                    var key = new KeyDict(User.Identity.GetUserName(), scope);
                    var value = new ValDict(uri, tokenPortion);
                    dicUpdate.Add(key, value);
                }
                Update(dicUpdate);

            }

            reader.Close();
            dataStream.Close();
            response.Close();
            return RedirectToAction("Index", "Home");
        }
        private static JObject Base64DecodeJson(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            var str = Encoding.UTF8.GetString(base64EncodedBytes);
            return JObject.Parse(str);
        }

    }
}