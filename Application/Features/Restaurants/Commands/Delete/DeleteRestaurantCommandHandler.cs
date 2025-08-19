using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Restaurants.Commands.Delete
{
    public class DeleteRestaurantCommandHandler(
        ILogger<DeleteRestaurantCommand> logger,
        IRestaurantsRepository restaurantsRepository,
        IRestaurantAuthorizationServices restaurantAuthorizationServices) : IRequestHandler<DeleteRestaurantCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting restaurant with ID: {RestaurantId}", request.Id);
            var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.Id);

            if (restaurant == null)
            {
                logger.LogWarning("Restaurant with ID: {Id} not found", request.Id);
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }

            if (!restaurantAuthorizationServices.Authorize(restaurant, ResourceOperationEnum.Delete))
            {
                logger.LogWarning("Unauthorized attempt to delete restaurant with ID: {Id}", request.Id);
                throw new AccessForbiddenException();
            }
            await restaurantsRepository.DeleteRestaurantAsync(restaurant);
            return Unit.Value;
        }
    }
}
