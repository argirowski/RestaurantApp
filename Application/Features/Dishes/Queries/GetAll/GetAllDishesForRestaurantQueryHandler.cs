using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Dishes.Queries.GetAll
{
    public class GetAllDishesForRestaurantQueryHandler
        (ILogger<GetAllDishesForRestaurantQueryHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IMapper mapper) : IRequestHandler<GetAllDishesForRestaurantQuery, IEnumerable<DishDTO>>
    {
        public async Task<IEnumerable<DishDTO>> Handle(GetAllDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving all dishes for restaurant with ID {RestaurantId}", request.RestaurantId);

            var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                logger.LogWarning("Restaurant with ID {RestaurantId} not found.", request.RestaurantId);
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            var dishes = mapper.Map<IEnumerable<DishDTO>>(restaurant.Dishes);
            return dishes;
        }
    }
}
