using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Restaurants.Commands.Create
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<CreateRestaurantCommand, RestaurantDTO>
    {
        public async Task<RestaurantDTO> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new restaurant with name: {@Restaurant}", request);
            var restaurant = mapper.Map<Restaurant>(request);
            var restaurantRepo = await restaurantsRepository.CreateRestaurantAsync(restaurant);
            return mapper.Map<RestaurantDTO>(restaurantRepo);
        }
    }
}
