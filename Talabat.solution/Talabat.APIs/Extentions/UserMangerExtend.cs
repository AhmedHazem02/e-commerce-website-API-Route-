using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity;
namespace Talabat.APIs.Extentions
{
    public static class UserMangerExtend
    {
        public static async Task<AppUser>GetCurrentUserAddress(this UserManager<AppUser>userManager,ClaimsPrincipal User)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user=await userManager.Users.Include(D=>D.address).FirstOrDefaultAsync(u => u.Email == Email);
            return user;
        }
    }
}
