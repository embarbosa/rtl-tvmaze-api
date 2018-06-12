using RTL.TVMaze.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RTL.TVMaze.Data.Command
{
    public interface ITVMazeCommand
    {
        void AddTVShow(TVShow tvShow);
        void AddCast(Cast cast);       
    }
}
