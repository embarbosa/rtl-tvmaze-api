using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RTL.TVMaze.Data.Mapping;
using RTL.TVMaze.Data.Models;
using Microsoft.EntityFrameworkCore;
using RTL.TVMaze.Data.Query;

namespace RTL.TVMaze.WebApi.Repositories
{
    public class TVShowRepository : ITVShowsRepository
    {
        private ITVMazeQuery _query;
        public TVShowRepository(ITVMazeQuery query)
        {
            _query = query;
        }

        public List<TVShow> Get()
        {            
            return _query.GetTVShows()
                .OrderBy(tvShow => tvShow.ID)
                .ThenByDescending(tvShow => tvShow.Cast.OrderBy(c =>c.Birthday.GetValueOrDefault(DateTime.MaxValue)))
                .ToList();
        }
    }
}
