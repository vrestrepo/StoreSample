using Application.Common;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace StoreSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<ProductModel>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<ProductModel>>), 400)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProductModel>>>> GetProducts()
        {
            var response = await _productService.GetProductsAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}