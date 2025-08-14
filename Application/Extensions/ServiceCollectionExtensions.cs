using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
            services.AddScoped<IRestaurantsService, RestaurantsService>();
            // Register auto-mapper profiles from this assembly
            services.AddAutoMapper(applicationAssembly);
            // Register FluentValidation validators from this assembly
            services.AddValidatorsFromAssembly(applicationAssembly);
        }
    }
}
