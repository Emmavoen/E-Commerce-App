using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApp.Application.Contracts;
using ECommerceApp.Persistence.DatabaseContext;
using ECommerceApp.Persistence.RepositoryImplementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApp.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection RegisterPersistenceService(this IServiceCollection services, IConfiguration conn)
        {
            return services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlite(conn.GetConnectionString("DefaultConnection"));
            })
            .AddScoped<IProductRepository, ProductRepository>();
        }
    }
}   