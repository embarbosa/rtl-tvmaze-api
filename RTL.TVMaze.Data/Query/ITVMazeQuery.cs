using RTL.TVMaze.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RTL.TVMaze.Data.Query
{
    public interface ITVMazeQuery
    {
        IEnumerable<TVShow> GetTVShows();
    }
}
