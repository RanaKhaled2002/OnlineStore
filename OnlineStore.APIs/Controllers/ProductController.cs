using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.APIs.Error;
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

        [ProducesResponseType(typeof(PaginationResponse<ProductDto>) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse) , StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProduct([FromQuery] ProductSpecParams productSpec)
        {
            var Result = await _productService.GetAllProductsAsync(productSpec);

            if (Result is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(Result);
        }

        [ProducesResponseType(typeof(PaginationResponse<BrandTypeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("brands")]
        public async Task<ActionResult<PaginationResponse<BrandTypeDto>>> GetAllBrands()
        {
            var Result = await _productService.GetAllBrandsAsync();

            if (Result is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(Result);
        }

        [ProducesResponseType(typeof(PaginationResponse<BrandTypeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("types")]
        public async Task<ActionResult<PaginationResponse<BrandTypeDto>>> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();
            if (result is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(result);
        }

        [ProducesResponseType(typeof(PaginationResponse<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetProductById(int? id)
        {
            if (id is null) return BadRequest(StatusCodes.Status400BadRequest);

            var result = await _productService.GetProductById(id.Value);

            if (result is null) return NotFound(StatusCodes.Status404NotFound);

            return Ok(result);
        }
    }
}
