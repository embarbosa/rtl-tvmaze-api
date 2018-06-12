using RTL.TVMaze.Data.Models;
using System.Collections.Generic;

namespace RTL.TVMaze.WebApi.Repositories
{
    public interface ITVShowsRepository
    {
        List<TVShow> Get();
    }
}