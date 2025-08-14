using Application.Features.Commands.Delete;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Commands.Update
{
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommand> logger, IRestaurantsRepository restaurantsRepository) : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating restaurant with ID: {Id}", request.Id);
            var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.Id);

            if (restaurant == null)
            {
                logger.LogWarning("Restaurant with ID: {Id} not found", request.Id);
                return false;
            }
            restaurant.Name = request.Name;
            restaurant.Description = request.Description;
            restaurant.HasDelivery = request.HasDelivery;

            await restaurantsRepository.UpdateRestaurantAsync(restaurant);
            return true;
        }
    }
}
