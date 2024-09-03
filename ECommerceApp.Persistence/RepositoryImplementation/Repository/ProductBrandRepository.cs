using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApp.Application.Contracts.Repository;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Persistence.DatabaseContext;
using ECommerceApp.Persistence.RepositoryImplementation.GenericRepository;

namespace ECommerceApp.Persistence.RepositoryImplementation.Repository
{
    public class ProductBrandRepository : GenericRepository<ProductBrand>, IProductBrandRepository
    {
        public ProductBrandRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}