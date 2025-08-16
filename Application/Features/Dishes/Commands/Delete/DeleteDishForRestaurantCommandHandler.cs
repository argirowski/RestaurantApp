using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Dishes.Commands.Delete
{
    public class DeleteDishForRestaurantCommandHandler(
        ILogger<DeleteDishForRestaurantCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishesRepository) : IRequestHandler<DeleteDishForRestaurantCommand, Unit>
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
            return Unit.Value;
        }
    }
}
