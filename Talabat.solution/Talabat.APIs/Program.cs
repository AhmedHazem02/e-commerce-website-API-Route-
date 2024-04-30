using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.APIs.Helper;
using Talabat.APIs.MiddelWare;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Repository.Identity.Dataseed;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Services
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(Option =>
            {
                Option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(Option =>
            {
                Option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            });

            

            builder.Services.AddApplicationExtention();
            //For Identity
            builder.Services.AddIdentityServices(builder.Configuration);




            var app = builder.Build();
            #endregion
            #region Update DataBase
            using var Scope=app.Services.CreateScope();
            var Service=Scope.ServiceProvider;
            var LoggreFactory=Service.GetRequiredService<ILoggerFactory>();
            try
            { 
                var dbcontext = Service.GetRequiredService<StoreContext>();
                await dbcontext.Database.MigrateAsync();

                var appIdentityDbContext = Service.GetRequiredService<AppIdentityDbContext>();
                await appIdentityDbContext.Database.MigrateAsync();

                var appIdentity = Service.GetRequiredService<UserManager<AppUser>>();

                await Identityseeding.SeedUserAsyn(appIdentity);


                await StoreContextSeed.SeedAsync(dbcontext);
                 
                 
            }
            catch (Exception ex)
            {
                var logger= LoggreFactory.CreateLogger<Program>();
                logger.LogError(ex, "Error in Migration");
            }
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExpctionMiddelWare>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
          
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles(); 
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
