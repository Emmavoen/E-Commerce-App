using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApp.Application.Contracts.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeRepository _repository;

        public ProductTypeController(IProductTypeRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("GetAllProductType")]
        public async Task<IActionResult> GetAllProductType()
        {
            var product =  await _repository.GetAll();
            return Ok(product);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetProduct(int id)
        {
            
            return Ok(await _repository.GetByColumnAsync(x =>x.Id ==id));
        }
    }
}