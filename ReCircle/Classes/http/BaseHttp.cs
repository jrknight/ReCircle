using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace ReCircle.Classes.http
{
    public class BaseHttp
    {
        public static async Task<T> Get<T>(string url)
        {
            /*await ($"{Configuration.Instance.BaseURL}/{url}")
                .WithHeader("Authentication", $"Bearer {Authentication.Instance.Token}")
                .GetJsonAsync<T>();*/
            HttpClient httpClient = new HttpClient();

            var headers = httpClient.DefaultRequestHeaders;

            try
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Authentication.Instance.Token}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occured in adding request headers: {ex}");
            }



            Uri uri = new Uri($"{Configuration.Instance.BaseURL}/{url}");

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string responseBody = "";

            try
            {
                httpResponse = await httpClient.GetAsync(uri);
                httpResponse.EnsureSuccessStatusCode();

                responseBody = await httpResponse.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                responseBody = $"Exception in BaseHttp: {ex}";
                Debug.WriteLine($"Exception in BaseHttp while getting type: {ex}");
            }


            try
            {
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"There was an error getting data: {ex}");
            }
            return (T)Convert.ChangeType(null, typeof(T));
        }

        public static async Task<HttpResponseMessage> Post<T>(string url, T dto)
        {
            HttpClient httpClient = new HttpClient();
            
            var headers = httpClient.DefaultRequestHeaders;
            httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Authentication.Instance.Token}");


            Uri uri = new Uri($"{Configuration.Instance.BaseURL}/{url}");

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequest.Content = new HttpStringContent(JsonConvert.SerializeObject(dto),
                Windows.Storage.Streams.UnicodeEncoding.Utf8,
                "application/json");

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                response = await httpClient.SendRequestAsync(httpRequest);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in BaseHttp while posting type: {ex}");
            }
            return response;
        }

        public static async Task<HttpResponseMessage> GetLibThing<T>()
        {
            Uri uri = new Uri("http://www.librarything.com/services/rest/1.1/?method=librarything.ck.getwork&id=1060&apikey=d231aa37c9b4f5d304a60a3d0ad1dad4");

            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

            HttpResponseMessage response;

            try
            {
                response = await client.GetAsync(uri);

                XmlSerializer serializer = new XmlSerializer(typeof(Item));
                var content = response.Content.ToString();
                TextReader reader = new StringReader(content);


                Item b = (Item) serializer.Deserialize(reader);
                Debug.WriteLine(b);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return new HttpResponseMessage();
        }

        public static async Task<HttpResponseMessage> Delete<T> (string url, T dto)
        {
            HttpClient httpClient = new HttpClient();

            var headers = httpClient.DefaultRequestHeaders;
            httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Authentication.Instance.Token}");
            
            Uri uri = new Uri($"{Configuration.Instance.BaseURL}/{url}");

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Delete, uri);

            httpRequest.Content = new HttpStringContent(JsonConvert.SerializeObject(dto),
                Windows.Storage.Streams.UnicodeEncoding.Utf8,
                "application/json");

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                response = await httpClient.SendRequestAsync(httpRequest);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in BaseHttp while posting type: {ex}");
            }
            return response;
        }
    }
}
