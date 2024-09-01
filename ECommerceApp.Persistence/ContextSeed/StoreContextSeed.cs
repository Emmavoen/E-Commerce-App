using System.Text.Json;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Persistence.DatabaseContext;

namespace ECommerceApp.Persistence.ContextSeed
{
    public class StoreContextSeed
    {
        public async Task SeedAsync(AppDbContext context)
        {
            if(!context.productBrands.Any())
            {
                var brandsData = File.ReadAllText("../ECommerceApp.Persistence/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.productBrands.AddRange(brands);

            }

             if(!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../ECommerceApp.Persistence/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);

            }

             if(!context.Products.Any())
            {
                var productsData = File.ReadAllText("../ECommerceApp.Persistence/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);

            }

            if(context.ChangeTracker.HasChanges()) await context. SaveChangesAsync();
        }
    }
}