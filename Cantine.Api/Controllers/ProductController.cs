using System;
using System.Threading.Tasks;
using Cantine.Exceptions;
using Cantine.Interfaces.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cantine.Api.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductManager productManager, ILogger<ProductController> logger)
        {
            _productManager = productManager;
            _logger = logger;
        }

        [HttpGet("products/{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                return Ok(await _productManager.GetById(id));
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                return Ok(await _productManager.GetAllAsync());
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
