using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace ASPI.Model.Measurements
{
    public class RestClient
    {
        private HttpClient _httpClient;
        public RestClient()
        {
            _httpClient = new HttpClient(new HttpClientHandler());
            _httpClient.BaseAddress = new Uri("http://192.168.228.25:55555");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        private void SetRequestHeader()
        {
            if (_httpClient == null)
                throw new NullReferenceException("_httpClient");

            if (_httpClient.DefaultRequestHeaders == null)
                throw new NullReferenceException("_httpClient.DefaultRequestHeaders");

            //_httpClient.DefaultRequestHeaders.Authorization = Authentication.GetAuthenticationHeaderValue(_httpClient);

            if (_httpClient.DefaultRequestHeaders.Authorization == null)
                throw new NullReferenceException("_httpClient.DefaultRequestHeaders.Authorization");
        }

        public async void PoDownload(string poNumber)
        {
            SetRequestHeader();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"http://192.168.228.25:55555/api/v1.0/orders/{poNumber}/commit")
            };

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);

            // var payload = JObject.Parse(await response.Content.ReadAsStringAsync());
           
        }

        public async void PoStart(string poNumber)
        {
            SetRequestHeader();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"http://192.168.228.25:55555/api/v1.0/orders/{poNumber}/start")
            };

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);
        }

        public async void PoStop(string poNumber)
        {
            SetRequestHeader();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"http://192.168.228.25:55555/api/v1.0/orders/{poNumber}/stop?")
            };

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());
           
        }

        public async void PoUpload(string poNumber)
        {
            SetRequestHeader();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"http://192.168.228.25:55555/api/v1.0/orders/{poNumber}/close")
            };

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);
         
        }
    }
}
