using Microsoft.AspNetCore.Identity;
using OnlineStore.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Services.Contract.Jwt
{
    public interface IJwtService
    {
        Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManger);
    }
}
