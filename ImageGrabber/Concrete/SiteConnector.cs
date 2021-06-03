using ImageGrabber.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImageGrabber.Concrete
{
    public class SiteConnector : ISiteConnector
    {
        private readonly HttpClient _httpClient;

        public Uri BaseUrl { get; private set; }

        public SiteConnector(HttpClient httpClient, Uri baseUrl)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = baseUrl;

            BaseUrl = baseUrl;

        }

        public async Task<string> LoadPageContentAsync()
        {
            var content = "";

            var response = await _httpClient.GetAsync(BaseUrl);

            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            return content;
        }

        public async Task<Stream> LoadImageAsync(string imageUrl)
        {

            var response = await _httpClient.GetAsync(imageUrl);

            Stream content = null;
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStreamAsync();
            }

            return content;
        }

       
    }
}
