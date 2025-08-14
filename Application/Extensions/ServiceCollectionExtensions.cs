using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
            services.AddMediatR(cfg =>
            {
                // Register MediatR handlers from this assembly
                cfg.RegisterServicesFromAssembly(applicationAssembly);
            });
            // Register auto-mapper profiles from this assembly
            services.AddAutoMapper(applicationAssembly);
            // Register FluentValidation validators from this assembly
            services.AddValidatorsFromAssembly(applicationAssembly);
        }
    }
}
