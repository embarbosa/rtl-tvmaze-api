using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RTL.TVMaze.ConsoleApp
{
    public class TVMazeAPI : ITVMazeAPI
    {
        IFakeHttpClient _fakeHttpClient = null;
        string _url = string.Empty; 

        public TVMazeAPI(string url = null, IFakeHttpClient fakeHttpClient = null)
        {
            _url = url;
            _fakeHttpClient = fakeHttpClient ?? new FakeHttpClient(_url);
        }

        public async Task<List<TVMazeCast>> GetCastAsync(int tvShowID)
        {
            string urlCast = string.Format("{0}/shows/{1}/cast", _url, tvShowID);

            List<TVMazeCast> listCast = null;
            HttpResponseMessage response = await _fakeHttpClient.GetAsync(urlCast);
                        
            if (response.IsSuccessStatusCode)
            {
                listCast = await response.Content.ReadAsAsync<List<TVMazeCast>>();
            }
            else if((int)response.StatusCode == 429)
            {
                Thread.Sleep(10000);
                return await GetCastAsync(tvShowID);
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

            return listCast;
        }

        public async Task<List<TVMazeTVShow>> GetTVShowsAsync()
        {
            List<TVMazeTVShow> listTVShow = null;
            HttpResponseMessage response = await _fakeHttpClient.GetAsync("/shows");

            if (response.IsSuccessStatusCode)
            {
                listTVShow = await response.Content.ReadAsAsync<List<TVMazeTVShow>>();
            }
            else if ((int)response.StatusCode == 429)
            {
                Thread.Sleep(10000);
                return await GetTVShowsAsync();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
            return listTVShow;
        }        
    }
}
