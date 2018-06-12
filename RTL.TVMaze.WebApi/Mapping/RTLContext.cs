using Microsoft.EntityFrameworkCore;
using RTL.TVMaze.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTL.TVMaze.WebApi.Mapping
{
    public class RTLContext : DbContext
    {
        #region Constructors
        public RTLContext() : base() { }
        public RTLContext(DbContextOptions<RTLContext> options) : base(options) { }
        #endregion


        #region Entities
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<Cast> Casts { get; set; }
        #endregion
    }
}
