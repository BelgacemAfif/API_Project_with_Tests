using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDemo.Core.Interfaces;
using UnitOfWorkDemo.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;

namespace UnitOfWorkDemo.Infrastructure.ServiceExtension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContextClass>(options =>
            {
                //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                //options.UseSqlite(configuration.GetConnectionString(name: "DefaultConnection"));
                options.UseInMemoryDatabase("TodoList");
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
