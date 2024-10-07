using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Services.Contract;
using OnlineStore.Service.Services.Products;

namespace OnlineStore.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery]string? sort, [FromQuery] int? brandId, [FromQuery] int? typeId, [FromQuery] int? pageIndex=1, [FromQuery] int? pageSize=5)
        {
            var Result = await _productService.GetAllProductsAsync(sort, brandId, typeId,pageIndex,pageSize);
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
