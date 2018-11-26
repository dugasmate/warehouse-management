using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;
using WarehouseManagement.Repositories;
using WarehouseManagement.Services;

namespace WarehouseManagement.Configurations
{
    public static class InjectionConfig
    {
        public static void AddInjections(this IServiceCollection services)
        {
            services.AddScoped<ICRUDRepository<Product>, WarehouseRepository>();
            services.AddScoped<ProductService>();
            services.AddScoped<StockService>();
            services.AddScoped<StatService>();
            services.AddScoped<MNBService>();
        }
    }
}
