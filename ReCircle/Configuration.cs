using Newtonsoft.Json;
using ReCircle.Classes.http;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Certificates;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;

namespace ReCircle
{
    public class Configuration
    {
        public static Configuration Instance { get; set; }


        public static void Setup()
        {
            Instance = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText("Config.json", Encoding.UTF8));
        }

        public string BaseURL { get; set; }

        public static async Task<UpdateDto> CheckUpdate()
        {
            var url = $"{Configuration.Instance.BaseURL}/api/update";

            var filter = new HttpBaseProtocolFilter();
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Expired);
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.InvalidName);


            var client = new HttpClient(filter);
            client.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            

            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                result = await client.SendRequestAsync(request);

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occured on checking for an update: {ex}");
            }

            if (result.IsSuccessStatusCode)
            {
                try
                {
                    return JsonConvert.DeserializeObject<UpdateDto>(result.Content.ToString());
                }
                catch(Exception ex)
                {
                    Debug.WriteLine($"An exception occured on checking for an update: {ex}");
                }
            }
            return null;
        }
    }
}
