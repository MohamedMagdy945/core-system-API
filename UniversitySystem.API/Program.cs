using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversitySystem.API.DependencyInjection;
using UniversitySystem.Application;
using UniversitySystem.Domain.Identity;
using UniverstySystem.Infrastructure;
using UniverstySystem.Infrastructure.Middlewares;
using UniverstySystem.Infrastructure.Models;
using UniverstySystem.Infrastructure.Persistence;

namespace UniversitySystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services
            .AddIdentity<AppUser, IdentityRole<int>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();


            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 9;
                options.Password.RequireNonAlphanumeric = false;
            });

            var jwtSettings = new JwtSettings();

            builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);

            builder.Services.AddSingleton(jwtSettings);

            builder.Services.AddJwtAuthentication(jwtSettings);

            builder.Services.AddApplicationService().AddInfrastructureService(builder.Configuration);




            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();


            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
