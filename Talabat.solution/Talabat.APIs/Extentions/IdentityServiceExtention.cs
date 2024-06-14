using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Service;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.APIs.Extentions
{
    public static class IdentityServiceExtention
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services,IConfiguration _configuration)
        {

            Services.AddScoped<ITokenService, TokenService>();
            Services.AddScoped<IPaymentService, PaymentService>();
            Services.AddIdentity<AppUser, IdentityRole>()
                             .AddEntityFrameworkStores<AppIdentityDbContext>();
            Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                     .AddJwtBearer(Options =>
                     {
                         Options.TokenValidationParameters = new TokenValidationParameters()
                         {
                             ValidateIssuer = true,
                             ValidIssuer = _configuration["JWT:ValidIssure"],
                             ValidateAudience = true,
                             ValidAudience = _configuration["JWT:ValidAudieuce"],
                             ValidateLifetime = true,
                             ValidateIssuerSigningKey = true,
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]))
                         };
                     });

            return Services;

        }
    }
}
