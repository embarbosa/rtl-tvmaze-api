using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RTL.TVMaze.ConsoleApp
{
    public interface IFakeHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
    }
}
