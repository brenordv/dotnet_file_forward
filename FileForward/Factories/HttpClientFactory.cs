using System;
using System.Net.Http;

namespace FileForward.Factories
{
    public class HttpClientFactory
    {
        private static HttpClient _clientInstance;
        private readonly string _baseUrl;

        public HttpClientFactory(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public HttpClient GetClient()
        {
            if (_clientInstance != null) return _clientInstance;
                
            _clientInstance = new HttpClient();
            if (!string.IsNullOrEmpty(_baseUrl))
                _clientInstance.BaseAddress = new Uri(_baseUrl);

            return _clientInstance;            
        }
    }
}