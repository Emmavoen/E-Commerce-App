using ECommerceApp.Application.Contracts.Repository;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Persistence.DatabaseContext;
using ECommerceApp.Persistence.RepositoryImplementation.GenericRepository;

namespace ECommerceApp.Persistence.RepositoryImplementation.Repository
{
    public class ProductTypeRepository : GenericRepository<ProductType> , IProductTypeRepository
    {
        public ProductTypeRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}