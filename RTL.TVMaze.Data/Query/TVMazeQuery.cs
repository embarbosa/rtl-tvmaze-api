using System.Collections.Generic;
using RTL.TVMaze.Data.Mapping;
using RTL.TVMaze.Data.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace RTL.TVMaze.Data.Query
{
    public class TVMazeQuery: ITVMazeQuery
    {
        private readonly RTLContext _context;

        public TVMazeQuery(RTLContext context)
        {
            _context = context?? new RTLContext();
        }

        public IEnumerable<TVShow> GetTVShows()
        {
            return
                (from tvShow in _context.TVShows
                 join cast in _context.Casts.DefaultIfEmpty() on tvShow.ID equals cast.TVShowId
                 into Casts
                 select new TVShow()
                 {
                     ID = tvShow.ID,
                     Name = tvShow.Name,
                     Cast = Casts.ToList()
                 });
        }
    }
}