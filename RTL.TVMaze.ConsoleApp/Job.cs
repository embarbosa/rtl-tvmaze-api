using RTL.TVMaze.Data.Command;
using RTL.TVMaze.Data.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RTL.TVMaze.ConsoleApp
{
    public class Job
    {
        private string _url;

        private ITVMazeAPI _tvMazeAPI = null;
        private ITVMazeCommand _command = null;

        public Job(string url, ITVMazeAPI tvMazeAPI = null, ITVMazeCommand command = null)
        {
            _url = url;
            _tvMazeAPI = tvMazeAPI ?? new TVMazeAPI(url);
            _command = command ?? new TVMazeCommand();
        }

        public void Run()
        {
            try
            {
                List<TVMazeTVShow> listTVShow = RunAsync().GetAwaiter().GetResult();
                UpdateDatabase(listTVShow);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UpdateDatabase(List<TVMazeTVShow> listTVShow)
        {
            foreach (var itemTVShow in listTVShow)
            {
                TVShow tvShow = new TVShow()
                {
                    ID = itemTVShow.ID,
                    Name = itemTVShow.Name
                };
                _command.AddTVShow(tvShow);

                foreach (var itemPerson in itemTVShow.Cast)
                {
                    Cast cast = new Cast()
                    {
                        ID = itemPerson.Person.ID,
                        Name = itemPerson.Person.Name,
                        Birthday = itemPerson.Person.Birthday,
                        TVShowId = tvShow.ID
                    };

                    _command.AddCast(cast);
                }
            }            
        }

        async Task<List<TVMazeTVShow>> RunAsync()
        {
            List<TVMazeTVShow> listTVShows = new List<TVMazeTVShow>();
            try
            {
                listTVShows = await _tvMazeAPI.GetTVShowsAsync();

                if (listTVShows != null && listTVShows.Count > 0)
                {
                    for (int i = 0; i < listTVShows.Count; i++)
                    {
                        TVMazeTVShow item = listTVShows[i];
                        List<TVMazeCast> listCast = await _tvMazeAPI.GetCastAsync(item.ID);
                        item.Cast = listCast;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return listTVShows;
        }
    }
}
