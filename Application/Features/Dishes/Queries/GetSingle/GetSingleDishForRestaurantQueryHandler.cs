using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Dishes.Queries.GetSingle
{
    public class GetSingleDishForRestaurantQueryHandler(
        ILogger<GetSingleDishForRestaurantQueryHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IMapper mapper) : IRequestHandler<GetSingleDishForRestaurantQuery, DishDTO>
    {
        public async Task<DishDTO> Handle(GetSingleDishForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving a single dish with ID:{DishId} for restaurant with ID {RestaurantId}", request.DishId, request.RestaurantId);

            var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                logger.LogWarning("Restaurant with ID {RestaurantId} not found.", request.RestaurantId);
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }
            var singleDish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
            if (singleDish == null)
            {
                logger.LogWarning("Dish with ID {DishId} not found.", request.DishId);
                throw new NotFoundException(nameof(Restaurant), request.DishId.ToString());
            }

            var singleDishResult = mapper.Map<DishDTO>(singleDish);
            return singleDishResult;
        }
    }
}
