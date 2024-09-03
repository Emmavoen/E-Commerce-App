using ECommerceApp.Application.Contracts.GenericRepository;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Contracts.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}