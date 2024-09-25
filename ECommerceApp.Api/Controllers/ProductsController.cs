using System.Linq.Expressions;
using ECommerceApp.Application.Contracts;
using ECommerceApp.Application.Contracts.Repository;
using ECommerceApp.Application.Dto;
using ECommerceApp.Application.Helper;
using ECommerceApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetPaginatedAsync(int? brandId = null, int? typeId = null,
        string searchTerm = null,
        int pageNumber = 1,int pageSize = 10,string sortBy = "Name",bool ascending = true)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

            Expression<Func<Product, bool>> filter = p =>
            (!brandId.HasValue || p.ProductBrandId == brandId.Value) &&
            (!typeId.HasValue || p.ProductTypeId == typeId.Value)
            &&(string.IsNullOrEmpty(searchTerm) || p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));

            Expression<Func<Product, object>> orderBy = sortBy switch
            {
                "Price" => p => p.Price,
                "Name" => p => p.Name,
                _ => p => p.Name // Default sorting by Name
            };
            var product = await _repository.GetPaginatedAsync(filter,pageNumber,pageSize,orderBy,ascending,p=> p.ProductBrand, p =>p.ProductType);
            var items = product.Items.Select(p => new ProductResponseDto
            { 
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                PictureUrl = baseUrl + "/" + p.PictureUrl,
                ProductType = p.ProductType?.Name ?? "unknown", 
                ProductBrand = p.ProductBrand?.Name ?? "unknown",


            }).ToList();

            return Ok(new PaginatedList<ProductResponseDto>(items,product.TotalCount,pageNumber,pageSize));
            //return Ok(product);
        }
    }
}