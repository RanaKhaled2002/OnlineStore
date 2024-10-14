using Microsoft.AspNetCore.Identity;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Entities.Identity;
using OnlineStore.Repository.Data.Contexts;
using OnlineStore.Repository.Identity.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineStore.Repository.Identity
{
    public static class StoreIdentitySeed
    {
        public static async Task SeedAsync(UserManager<AppUser> _userManger)
        {
            if (_userManger.Users.Count() == 0)
            {
                var User = new AppUser()
                {
                    Email = "ranakhaeled@gmail.com",
                    DisplayName = "Rana Khaled",
                    UserName = "Rana.Khaled",
                    Address = new Address()
                    {
                        FName = "Rana",
                        LName = "Khaled",
                        City = "New Cairo",
                        Country = "Egypt",
                        Street = "ElShabab"
                    }
                };
                await _userManger.CreateAsync(User,"P@ssW0rd");
            }
            
        }
    }
}
