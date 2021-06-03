using Otus.CSharp.HW4.Absract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Otus.CSharp.HW4.Concrete
{
    public class HttpConnector : IHttpConnector
    {
        private readonly HttpClient _httpClient;

        public HttpConnector(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LoadPageContentAsync(string url)
        {
            var content = "";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            return content;
        }

        public async Task<Stream> LoadImageAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);

            Stream content = null;
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStreamAsync();
            }

            return content;
        }

        public string PrepareImageUrl(string imageUrl, string baseUrl)
        {

            if (imageUrl.Contains(baseUrl))
            {
                return imageUrl;
            }
            else
            {
                return baseUrl + imageUrl;
            }
        }
    }
}
