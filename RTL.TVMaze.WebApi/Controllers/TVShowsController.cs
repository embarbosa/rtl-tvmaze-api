using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTL.TVMaze.Data.Models;
using RTL.TVMaze.WebApi.Repositories;

namespace RTL.TVMaze.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TVShowsController : Controller
    {
        private const int pageSize = 10;
        private readonly ITVShowsRepository _tvShowsRepository;

        public TVShowsController(ITVShowsRepository tvShowsRepository)
        {
            _tvShowsRepository = tvShowsRepository;
        }
        
        [HttpGet]
        [HttpGet("{page}/")]
        public IEnumerable<TVShow> Get(int? page)
        {
            int curPage = page.GetValueOrDefault(1) - 1;
            return _tvShowsRepository.Get().Skip(pageSize * curPage).Take(pageSize);
        }
    }

}
