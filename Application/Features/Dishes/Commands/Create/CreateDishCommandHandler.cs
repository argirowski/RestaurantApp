using Application.Features.Restaurants.Commands.Create;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Dishes.Commands.Create
{
    public class CreateDishCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository) : IRequestHandler<CreateDishCommand, Guid>
    {
        public async Task<Guid> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new dish: {@DisheRequest}", request);
            var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                logger.LogWarning("Restaurant with ID {RestaurantId} not found.", request.RestaurantId);
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }
            var dish = mapper.Map<Dish>(request);
            return await dishesRepository.CreateDishAsync(dish);
        }
    }
}
