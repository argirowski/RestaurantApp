using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Authorization;
using Infrastructure.Authorization.AuthorizationServices;
using Infrastructure.Authorization.Requirements;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Seed;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");
            services.AddDbContext<RestaurantsDBContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<RestaurantsDBContext>();

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNamesEnum.HasNationality.ToString(), builder => builder.RequireClaim(PolicyNamesEnum.Nationality.ToString(), "Macedonian", "Japanese"))
                .AddPolicy(PolicyNamesEnum.IsAdult.ToString(), builder => builder.AddRequirements(new MinimumAgeRequirement(18)))
                .AddPolicy(PolicyNamesEnum.CreatedAtLeastTwoRestaurants.ToString(), builder => builder.AddRequirements(new CreateMultipleRestaurantsRequirement(2)));

            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, CreateMultipleRestaurantsRequirementHandler>();
            services.AddScoped<IRestaurantAuthorizationServices, RestaurantAuthorizationServices>();
        }
    }
}
