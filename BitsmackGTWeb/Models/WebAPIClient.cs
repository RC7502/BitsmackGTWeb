using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BitsmackGTWeb.Models
{
    class WebAPIClient
    {

        public static T Get<T>(string apiName) where T : new()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.APIURL());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token",Constants.Token());

                HttpResponseMessage response = client.GetAsync(apiName).Result;
                if (response.IsSuccessStatusCode)
                {
                    var records = response.Content.ReadAsAsync<T>();
                    return records.Result;
                }

            }
            return new T();
        }

        public static async Task Post<T>(string apiName, T newrec)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.APIURL());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", Constants.Token());

                string content = JsonConvert.SerializeObject(newrec);
                HttpResponseMessage response = await client.PostAsync(apiName, new StringContent(content, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();

            }
        }

        public static async Task Put<T>(string apiName, int id, T updatedRec)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.APIURL());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", Constants.Token());

                string content = JsonConvert.SerializeObject(updatedRec);
                HttpResponseMessage response = await client.PutAsync(apiName + "/" + id, new StringContent(content, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();

            }
        }
    }
}
