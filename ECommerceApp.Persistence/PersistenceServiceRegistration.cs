using ECommerceApp.Application.Contracts.Repository;
using ECommerceApp.Persistence.DatabaseContext;
using ECommerceApp.Persistence.RepositoryImplementation.Repository;
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
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IProductBrandRepository, ProductBrandRepository>()
            .AddScoped<IProductTypeRepository, ProductTypeRepository>()
            ;
        }
    }
}   