using Application.Claims;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Authorization.AuthorizationServices
{
    public class RestaurantAuthorizationServices(ILogger<RestaurantAuthorizationServices> logger, IUserContext userContext) : IRestaurantAuthorizationServices
    {
        public bool Authorize(Restaurant restaurant, ResourceOperationEnum resourceOperationEnum)
        {
            var user = userContext.GetCurrentUser();

            logger.LogInformation("Authorizing user {UserEmail} for operation {Operation} on restaurant {RestaurantName}",
                user.Email, resourceOperationEnum, restaurant.Name);

            if (resourceOperationEnum == ResourceOperationEnum.Create || resourceOperationEnum == ResourceOperationEnum.Read)
            {
                logger.LogInformation("Create / Read operation. Authorization successful");
                return true;
            }

            if (resourceOperationEnum == ResourceOperationEnum.Delete && user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Administrator user, delete operation - successful authorization");
                return true;
            }

            if ((resourceOperationEnum == ResourceOperationEnum.Delete || resourceOperationEnum == ResourceOperationEnum.Update) && user.Id == restaurant.OwnerId)
            {
                logger.LogInformation("Restaurant user, - successful authorization");
                return true;
            }

            return false;
        }
    }
}
