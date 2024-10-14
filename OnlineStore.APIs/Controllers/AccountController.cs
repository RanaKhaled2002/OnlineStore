using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.APIs.Error;
using OnlineStore.Core.DTOs.Auth;
using OnlineStore.Core.Services.Contract.User;

namespace OnlineStore.APIs.Controllers
{
   
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
        {
            var result =  await _userService.LoginAsync(login);
            if (result is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO register)
        {
            var result = await _userService.RegisterAsync(register);
            if (result is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"Invalid Registration!!"));
            return Ok(result);
        }
    }
}
