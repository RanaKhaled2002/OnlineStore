using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Repository.Data.Contexts;

namespace OnlineStore.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("BadRequest")]
        public async Task<IActionResult> GetBadRequestError()
        {
            return BadRequest();
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
            if (brands is null) return NotFound();
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
            return Unauthorized();
        }
    }
}
