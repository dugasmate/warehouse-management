using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManagement.Context;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;
using WarehouseManagement.Repositories;
using WarehouseManagement.Services;

namespace WarehouseManagement
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<ICRUDRepository<Product>, ProductRepository>();
            services.AddScoped<ICRUDRepository<Stock>, StockRepository>();
            services.AddScoped<StockService>();
            services.AddDbContext<WarehouseContext>(options =>
  options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Warehouse;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
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
