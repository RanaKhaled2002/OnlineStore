using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Core.Entities.Identity;
using OnlineStore.Core.Services.Contract.Jwt;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       

        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManger)
        {

            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.DisplayName),
            };

            var UserRoles = await userManger.GetRolesAsync(user); 

            foreach(var role in UserRoles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role,role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            var token = new JwtSecurityToken
            (
                issuer : _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audienece"],
                expires : DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
