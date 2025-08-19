using Application.Claims;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Authorization.Requirements
{
    internal class CreateMultipleRestaurantsRequirementHandler(
        IRestaurantsRepository restaurantsRepository,
        ILogger<CreateMultipleRestaurantsRequirementHandler> logger,
        IUserContext userContext) : AuthorizationHandler<CreateMultipleRestaurantsRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateMultipleRestaurantsRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();

            var restaurants = await restaurantsRepository.GetAllRestaurantsAsync();

            var userRestaurantsCreated = restaurants
                .Count(r => r.OwnerId == currentUser!.Id);

            if (userRestaurantsCreated >= requirement.MinimumRestaurantsCreated)
            {
                logger.LogInformation("User {CurrentUser} meets the minimum restaurants created requirement.",
                    currentUser);
                context.Succeed(requirement);
            }
            else
            {
                logger.LogWarning("User {CurrentUser} does not meet the minimum restaurants created requirement.",
                    currentUser);
                context.Fail();
            }

        }
    }
}
