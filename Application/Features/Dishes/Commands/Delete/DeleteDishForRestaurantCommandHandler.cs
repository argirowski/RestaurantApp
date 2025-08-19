using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Dishes.Commands.Delete
{
    public class DeleteDishForRestaurantCommandHandler(
        ILogger<DeleteDishForRestaurantCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IRestaurantAuthorizationServices restaurantAuthorizationServices) : IRequestHandler<DeleteDishForRestaurantCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteDishForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogWarning("Deleting dish for restaurant with ID: {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                logger.LogWarning("Restaurant with ID {RestaurantId} not found.", request.RestaurantId);
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            if (!restaurantAuthorizationServices.Authorize(restaurant, ResourceOperationEnum.Delete))
            {
                logger.LogWarning("Unauthorized attempt to delete restaurant with ID: {RestaurantId}", request.RestaurantId);
                throw new AccessForbiddenException();
            }

            return Unit.Value;
        }
    }
}
