using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CoreBot2.BL
{
    public class HttpMethods
    {
        public async Task<string> getUserInfo(string userName)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56597/api/values/shreyas");
            string address = client.BaseAddress.AbsoluteUri;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            HttpResponseMessage response = client.GetAsync(address).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return "Network connectivity error..";
            }
        }

        public async Task<string> getMarketHealthReport(string userName)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56597/api/markethealth/shreyas");
            string address = client.BaseAddress.AbsoluteUri;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(address).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return "Network connectivity error..";
            }
        }

        public async Task<string> getFileStatus(string userName)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56597/api/filestatus/shreyas");
            string address = client.BaseAddress.AbsoluteUri;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(address).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return "Network connectivity error..";
            }
        }
    }
}
