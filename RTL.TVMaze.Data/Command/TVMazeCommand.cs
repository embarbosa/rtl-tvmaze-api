using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RTL.TVMaze.Data.Mapping;
using RTL.TVMaze.Data.Models;

namespace RTL.TVMaze.Data.Command
{
    public class TVMazeCommand : ITVMazeCommand
    {
        private readonly RTLContext _context;

        public TVMazeCommand(RTLContext context = null)
        {
            _context = context ?? new RTLContext();
        }

        public void AddCast(Cast cast)
        {
            cast.Updated = DateTime.Now;
            Cast oldItem = null;
            if ((oldItem = _context.Casts.SingleOrDefault(c => c.ID == cast.ID)) != null)
            {
                _context.Casts.Remove(oldItem);
            }

            _context.Casts.Add(cast);
            
            _context.SaveChanges();
            
        }

        public void AddTVShow(TVShow tvShow)
        {
            tvShow.Updated = DateTime.Now;
            TVShow oldItem = null;

            if ((oldItem = _context.TVShows.SingleOrDefault(tv => tv.ID == tvShow.ID)) != null)
            {
                _context.TVShows.Remove(oldItem);           
            }

            _context.TVShows.Add(tvShow);
            
            _context.SaveChanges();
        }
    }
}
