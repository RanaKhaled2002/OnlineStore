using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.APIs.Error;
using OnlineStore.Core.Entities.Identity;
using System.Security.Claims;

namespace OnlineStore.APIs.Extention
{
    public static class UserMangerExtention
    {
        public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return null;
            
            var user = await userManager.Users.Include(U=>U.Address).FirstOrDefaultAsync(U => U.Email == userEmail);

            if (user is null) return null;

            return user;
        }
    }
}
