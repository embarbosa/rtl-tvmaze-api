using System;
using System.Collections.Generic;
using System.Text;

namespace RTL.TVMaze.ConsoleApp
{
    public class TVMazeTVShow
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<TVMazeCast> Cast { get; set; }
    }
}