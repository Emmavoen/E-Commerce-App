using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public string GetProducts()
        {
            return "";
        }

        [HttpGet("{id}")]
        public string GetProduct(int id)
        {
            return "";
        }

    }
}