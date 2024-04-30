using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.MiddelWare
{
    public class ExpctionMiddelWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExpctionMiddelWare> logger;
        private readonly IHostEnvironment env;

        public ExpctionMiddelWare(RequestDelegate Next,ILogger<ExpctionMiddelWare>logger,IHostEnvironment env)
        {
            next = Next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(Exception ex) {

                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = env.IsDevelopment() ? new ApiServerErorres((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
                                                     new ApiServerErorres((int)HttpStatusCode.InternalServerError);

                var Option = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var ResJson = JsonSerializer.Serialize(response,Option);
                context.Response.WriteAsync(ResJson);
            }
        }
    }
}
