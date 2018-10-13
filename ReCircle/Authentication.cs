using Newtonsoft.Json;
using ReCircle.Classes;
using ReCircle.Classes.http.dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Certificates;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;

namespace ReCircle
{
    public class Authentication
    {
        public static Authentication Instance { get; set; }
        public string Token { get; set; }
        public List<string> Role { get; set; }
        public User CurrentUser { get; set; }

        public static async Task<bool> Login(string UserName, string Password)
        {
            var url = $"{Configuration.Instance.BaseURL}/api/auth/token";
            /*var filter = new HttpBaseProtocolFilter();
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Expired);
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.InvalidName);*/

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));

            request.Content = new HttpStringContent(JsonConvert.SerializeObject(new { UserName, Password }),
                Windows.Storage.Streams.UnicodeEncoding.Utf8,
                "application/json");

            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                result = await client.SendRequestAsync(request);

                if (result.StatusCode == HttpStatusCode.Ok)
                {
                    Instance = JsonConvert.DeserializeObject<Authentication>(result.Content.ToString());
                }
                else
                {
                    Instance = null;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"An exception occured on Authentication: {ex}");
            }
            
            var def = new { token = "" };

            return (Instance != null);
        }

        public static async Task<HttpResponseMessage> NewUser(UserModelDto model)
        {
            Configuration.Setup();

            var url = $"{Configuration.Instance.BaseURL}api/auth/newuser";

            var filter = new HttpBaseProtocolFilter();
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Expired);
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.InvalidName);

            var client = new HttpClient(filter);
            client.DefaultRequestHeaders.Accept.Add(new Windows.Web.Http.Headers.HttpMediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));

            request.Content = new HttpStringContent(JsonConvert.SerializeObject(model),
                Windows.Storage.Streams.UnicodeEncoding.Utf8,
                "application/json");

            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                result = await client.SendRequestAsync(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occured on Authentication: {ex}");
            }
            return result;
        }
    }
}
