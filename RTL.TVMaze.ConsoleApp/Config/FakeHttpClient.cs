using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RTL.TVMaze.ConsoleApp
{
    public class FakeHttpClient : IFakeHttpClient
    {
        private readonly string _url;

        public FakeHttpClient(string url = null)
        {
            _url = url ?? string.Empty;
        }
        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client.GetAsync(requestUri);
        }
    }
}
