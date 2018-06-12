using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RTL.TVMaze.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string urlTVMazeAPI = "http://api.tvmaze.com";

            Job job = new Job(urlTVMazeAPI);
            job.Run();
        }
    }
}
