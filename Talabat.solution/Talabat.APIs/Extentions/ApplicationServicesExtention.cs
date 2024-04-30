using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helper;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talabat.APIs.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationExtention(this IServiceCollection Services)
        {

            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MappingProfile));
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {

                    var errors = actionContext.ModelState.Where(E => E.Value.Errors.Count() > 0)
                    .SelectMany(E => E.Value.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray();

                    var apiValidationErorrResponse = new ApiValidationErorrResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(apiValidationErorrResponse);
                };

            });

            return Services;
        }
    }
}
