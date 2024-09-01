using ECommerceApp.Application.Contracts;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace ECommerceApp.Persistence.RepositoryImplementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
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