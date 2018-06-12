using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RTL.TVMaze.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RTL.TVMaze.ConsoleApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<RTLContext>(option =>
                option.UseSqlServer(connectionString: Configuration.GetConnectionString("TRLConnection")));

            // Add functionality to inject IOptions<T>
            services.AddOptions();
        }

        public void ConfigureProduction(IApplicationBuilder app)
        {
        }
    }
}
