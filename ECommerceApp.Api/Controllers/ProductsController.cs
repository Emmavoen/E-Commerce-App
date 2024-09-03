using ECommerceApp.Application.Contracts;
using ECommerceApp.Application.Contracts.Repository;
using ECommerceApp.Application.Dto;
using ECommerceApp.Domain.Entities;
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
        [HttpGet("GetById")]
        public async Task<IActionResult> GetProductByIdWithInclude(int id)
        {
            //var product =  await _repository.GetProductsAsync();
            var product = await _repository.GetByIdAsyncWithInclude(id, p => p.ProductBrand, p => p.ProductType);
            return Ok(new ProductResponseDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name,


            });
        }

        [HttpGet("GellAllProducts")]
        public async Task<IActionResult> GetAllProductWithInclude()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            

            var product = await _repository.GetAllAsyncWithInclude(p => p.ProductBrand, p => p.ProductType);
            return Ok(product.Select
            (
                product => new ProductResponseDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    PictureUrl = $"{baseUrl}/{product.PictureUrl}",
                    Price = product.Price,
                    ProductBrand = product.ProductBrand.Name,
                    ProductType = product.ProductType.Name,
                }
            ));
        }
    }
}