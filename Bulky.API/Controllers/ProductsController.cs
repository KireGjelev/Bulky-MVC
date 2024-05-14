using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace Bulky.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] 
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/products
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetAll();
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productRepository.Get(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
