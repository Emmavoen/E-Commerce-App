using ECommerceApp.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var product =  await _repository.GetProductsAsync();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            
            return Ok(await _repository.GetProductByIdAsync(id));
        }

        [HttpGet("Brands")]
        public async Task<IActionResult> GetProductType()
        {
            return Ok(await _repository.GetProductTypesAsync());
        }

        [HttpGet("Types")]
        public async Task<IActionResult> GetProductBrand()
        {
            return Ok(await _repository.GetProductBrandsAsync());
        }

    }
}