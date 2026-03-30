using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UniversitySystem.Application.Behaviors;
using UniversitySystem.Application.Features.Students.Models;

namespace UniversitySystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(cfg => { }, typeof(StudentItemDto).Assembly);

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
