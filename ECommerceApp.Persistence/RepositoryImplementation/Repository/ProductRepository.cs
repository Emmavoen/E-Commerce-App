using ECommerceApp.Application.Contracts.Repository;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Persistence.DatabaseContext;
using ECommerceApp.Persistence.RepositoryImplementation.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Persistence.RepositoryImplementation.Repository
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
           return await _context.productBrands.ToListAsync();
            
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
           return await _context.Products.
           Include(p => p.ProductType).
            Include(p => p.ProductBrand).FirstOrDefaultAsync(p  =>p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products.
            Include(p => p.ProductType).
            Include(p => p.ProductBrand).ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync(); 
        }
    }
}