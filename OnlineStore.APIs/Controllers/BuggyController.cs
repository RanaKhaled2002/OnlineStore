using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.APIs.Error;
using OnlineStore.Repository.Data.Contexts;

namespace OnlineStore.APIs.Controllers
{
    
    public class BuggyController : BaseApiController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("BadRequest")]
        public async Task<IActionResult> GetBadRequestError()
        {
            return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("BadRequest/{id}")]
        public async Task<IActionResult> GetBadRequestError(int id)
        {
            return Ok();
        }

        [HttpGet("NotFound")]
        public async Task<IActionResult> GetNotFoundError()
        {
            var brands = await _context.Brands.FindAsync(100);
            if (brands is null) return NotFound(new ApiErrorResponse(400,"Brand With Id: 100 Not Found"));
            return Ok(brands);
        }

        [HttpGet("ServerErorr")]
        public async Task<IActionResult> GetServerError()
        {
            var brands = await _context.Brands.FindAsync(100);

            var brandsToString = brands.ToString();

            return Ok(brands);
        }

        [HttpGet("Unauthorized")]
        public async Task<IActionResult> GetUnAuthorizedError()
        {
            return Unauthorized(new ApiErrorResponse(401));
        }
    }
}
