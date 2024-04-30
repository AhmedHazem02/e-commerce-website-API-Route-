using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity.Dataseed
{
    public static class Identityseeding
    {
        public static async Task SeedUserAsyn(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "AhmedHazem",
                    Email = "ahmed23@gmail.com",
                    UserName = "AhmedHazem",
                    PhoneNumber = "01068298970",
                };
                await userManager.CreateAsync(user);
            }
        }
    }
}
