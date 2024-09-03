using ECommerceApp.Application.Contracts.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductBrandController : ControllerBase
    {
        private readonly IProductBrandRepository _repository;

        public ProductBrandController(IProductBrandRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("GetAllBrand")]
        public async Task<IActionResult> GetAllProductBrand()
        {
            var product =  await _repository.GetAll();
            return Ok(product);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetProduct(int id)
        {
            
            return Ok(await _repository.GetByColumnAsync(x => x.Id == id));
        }

    }
}