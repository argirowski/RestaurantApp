using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authorization.Requirements
{
    public class CreateMultipleRestaurantsRequirement(int minimumRestaurantsCreated) : IAuthorizationRequirement
    {
        public int MinimumRestaurantsCreated { get; } = minimumRestaurantsCreated;
    }
}
