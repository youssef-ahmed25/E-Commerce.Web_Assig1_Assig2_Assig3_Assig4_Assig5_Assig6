using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataAsync(this WebApplication app)
        {
            var Scope = app.Services.CreateScope();
            var seed = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await  seed.DataSeedAsync();
        }

        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWare(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
