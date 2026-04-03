using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Application.Interfaces.Auth;
using UniversitySystem.Application.Interfaces.Identity;
using UniverstySystem.Infrastructure.Identity.Handler;
using UniverstySystem.Infrastructure.Identity.Provider;
using UniverstySystem.Infrastructure.Persistence;
using UniverstySystem.Infrastructure.Repositories;
using UniverstySystem.Infrastructure.Services.Auth;
using UniverstySystem.Infrastructure.Services.Identity;

namespace UniverstySystem.Infrastructure
{
    public static class InfrastructureRegistrationService
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<ITokenGenerator, TokenGenerator>();


            services.AddScoped<IAppDbContext, AppDbContext>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();


            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
