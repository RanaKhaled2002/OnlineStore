using Microsoft.AspNetCore.Identity;
using OnlineStore.Core.DTOs.Auth;
using OnlineStore.Core.Entities.Identity;
using OnlineStore.Core.Services.Contract.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Services.Contract.Jwt;

namespace OnlineStore.Service.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManger;
        private readonly IJwtService _token;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManger,IJwtService token)
        {
            _userManager = userManager;
            _signInManger = signInManger;
            _token = token;
        }

        public async Task<UserDTO> LoginAsync(LoginDTO login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user is null) return null;
            var result =  await _signInManger.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded) return null;

            return new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _token.CreateTokenAsync(user, _userManager)
            };
           
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO register)
        {
            if (await CheckEmailExistAsync(register.Email)) return null;

            var user = new AppUser()
            {
                Email = register.Email,
                DisplayName = register.DisplayName,
                PhoneNumber = register.PhoneNumber,
                UserName = register.Email.Split("@")[0]
            };

           var result =await  _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded) return null;

            return new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _token.CreateTokenAsync(user, _userManager)
            };

        }

        public async Task<bool> CheckEmailExistAsync(string email)
        {
           return await _userManager.FindByEmailAsync(email) is not null;
        }

       
    }
}
