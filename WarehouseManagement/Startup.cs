using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManagement.Configurations;
using WarehouseManagement.Context;

namespace WarehouseManagement
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddInjections();
            var connection = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<WarehouseContext>(options => options.UseSqlServer(connection));
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddInjections();
            var connection = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<WarehouseContext>(options => options.UseInMemoryDatabase("WarehouseDB"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
