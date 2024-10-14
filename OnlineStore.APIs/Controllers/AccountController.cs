﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.APIs.Error;
using OnlineStore.Core.DTOs.Auth;
using OnlineStore.Core.Entities.Identity;
using OnlineStore.Core.Services.Contract.Jwt;
using OnlineStore.Core.Services.Contract.User;
using System.Security.Claims;

namespace OnlineStore.APIs.Controllers
{
   
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtService _token;

        public AccountController(IUserService userService,UserManager<AppUser> userManager,IJwtService token)
        {
            _userService = userService;
            _userManager = userManager;
            _token = token;
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

        [HttpGet("GetCrruentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var user = await _userManager.FindByEmailAsync(userEmail);

            if(user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _token.CreateTokenAsync(user,_userManager)
            });
        }
    }
}
