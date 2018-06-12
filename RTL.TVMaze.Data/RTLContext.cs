using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RTL.TVMaze.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RTL.TVMaze.Data.Mapping
{
    public class RTLContext : DbContext
    {
            #region Constructors
        public RTLContext() : base() { }
        public RTLContext(DbContextOptions<RTLContext> options) : base(options) { }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"server=localhost; database=RTLTVMaze_Database; user id=RtlDatabaseUser; password=P@55w0rd;");
            }
        }        

        #region Entities
        public virtual DbSet<TVShow> TVShows { get; set; }
        public virtual DbSet<Cast> Casts { get; set; }
        
        #endregion
    }
}
