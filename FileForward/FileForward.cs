using System;
using System.Net.Http;
using System.Threading.Tasks;
using FileForward.Factories;
using FileForward.Helpers;

namespace FileForward
{
    public class ForwardEngine
    {
        private readonly HttpClientFactory _httpClientFactory;

        public ForwardEngine(string targetUrl)
        {
            _httpClientFactory = new HttpClientFactory(targetUrl);
        }

        public async Task<HttpResponseMessage> PostBytes(string endpointUrl, byte[] content)
        {
            ValidationHelper.CheckEndpointUrl(endpointUrl);
            ValidationHelper.MustHaveContent(content);
            
            var client = _httpClientFactory.GetClient();
            var httpResponse =  await client.PostAsync(new Uri(endpointUrl, UriKind.Relative), new ByteArrayContent(content));
            
            return httpResponse;
        }

        public async Task<HttpResponseMessage> PostBytes(string endpointUrl, string filename)
        {
            ValidationHelper.CheckFilename(filename);
            return await PostBytes(endpointUrl, FileHelper.GetFileContent(filename));
        }
    }
}