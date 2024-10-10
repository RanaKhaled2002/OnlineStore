using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.DTOs.Products;
using OnlineStore.Core.Helper;
using OnlineStore.Core.Services.Contract;
using OnlineStore.Core.Specification.Products;
using OnlineStore.Service.Services.Products;

namespace OnlineStore.APIs.Controllers
{
    
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] ProductSpecParams productSpec)
        {
            var Result = await _productService.GetAllProductsAsync(productSpec);
            return Ok(Result);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var Result = await _productService.GetAllBrandsAsync();
            return Ok(Result);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id is null) return BadRequest("Invalid Id !!");

            var result = await _productService.GetProductById(id.Value);

            if (result is null) return NotFound($"The Product With Id: {id} Not Found In Database !!");

            return Ok(result);
        }
    }
}
