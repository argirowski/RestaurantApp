using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Restaurants.Commands.Update
{
    public class UpdateRestaurantCommandHandler(
        ILogger<UpdateRestaurantCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IMapper mapper,
        IRestaurantAuthorizationServices restaurantAuthorizationServices
    ) : IRequestHandler<UpdateRestaurantCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating restaurant with ID: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
            var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.Id);

            if (restaurant == null)
            {
                logger.LogWarning("Restaurant with ID: {Id} not found", request.Id);
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }

            if (!restaurantAuthorizationServices.Authorize(restaurant, ResourceOperationEnum.Update))
            {
                logger.LogWarning("Unauthorized attempt to update restaurant with ID: {Id}", request.Id);
                throw new AccessForbiddenException();
            }

            mapper.Map(request, restaurant);
            await restaurantsRepository.UpdateRestaurantAsync(restaurant);
            return Unit.Value;
        }
    }
}
