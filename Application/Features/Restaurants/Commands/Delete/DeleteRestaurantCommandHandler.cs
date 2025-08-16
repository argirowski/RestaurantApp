using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Restaurants.Commands.Delete
{
    public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommand> logger, IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand, Unit>
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
            await restaurantsRepository.DeleteRestaurantAsync(restaurant);
            return Unit.Value;
        }
    }
}
