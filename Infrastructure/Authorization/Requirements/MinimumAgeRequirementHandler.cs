using Application.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Authorization.Requirements
{
    public class MinimumAgeRequirementHandler(
        ILogger<MinimumAgeRequirementHandler> logger,
        IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("Handling MinimumAgeRequirement for user with email: {Email} and date of birth {DoB}", currentUser.Email, currentUser.DateOfBirth);

            if (currentUser == null)
            {
                logger.LogWarning("User Date Of Birth is null.");
                context.Fail();
                return Task.CompletedTask;
            }

            if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
            {
                logger.LogInformation("Authorization Successful.");
                context.Succeed(requirement);
            }

            else
            {
                logger.LogWarning("Authorization Failed. User does not meet the minimum age requirement.");
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
