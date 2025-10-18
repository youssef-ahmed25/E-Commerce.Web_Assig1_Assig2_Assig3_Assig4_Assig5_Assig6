
using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data.Contexts;
using Persistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using Shared.ErrorModels;
using System.Threading.Tasks;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Add services to the container
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            #region class ServiceRegistration
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen(); 
            #endregion

            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();

            #region Class infrastructureServiceRegistration
            //builder.Services.AddDbContext<StoreDbContext>(options =>
            //  {
            // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            // });


            //builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            #endregion
            #region Class AplicationServicesRegistiration
            //builder.Services.AddAutoMapper(config => config.AddProfile(new ProductProfile()),typeof(Service.AssemblyReference).Assembly);
            //builder.Services.AddScoped<IServiceManager, ServiceManager>(); 


            #endregion
            #region class ServiceRegistration
            //builder.Services.Configure<ApiBehaviorOptions>((options) =>
            //{
            //    options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;

            //}); 
            #endregion
            #endregion

            var app = builder.Build();

            #region DataSeeding
            //var Scope = app.Services.CreateScope();
            //var seed = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            //seed.DataSeedAsync();
            await  app.SeedDataAsync();
            #endregion


            #region  class WebApplicationRegistration
            // Configure the HTTP request pipeline
            //app.Use(async (RequestContext, NextMiddleWare) =>
            //{
            //    Console.WriteLine("Reauest under processing");
            //    await NextMiddleWare.Invoke();
            //    Console.WriteLine("waiting respone");
            //    Console.WriteLine(RequestContext.Request.Body);
            //}); 
            //app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            app.UseCustomExceptionMiddleWare();
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
                app.UseSwaggerMiddleWare();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
