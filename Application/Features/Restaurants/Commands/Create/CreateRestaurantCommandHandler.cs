using Application.Claims;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Restaurants.Commands.Create
{
    public class CreateRestaurantCommandHandler(
        ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper, IRestaurantsRepository restaurantsRepository,
        IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, RestaurantDTO>
    {
        public async Task<RestaurantDTO> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("{UserName} [{UserId}] is creating a new restaurant with name: {@Restaurant}", currentUser.Email, currentUser.Id, request);
            var restaurant = mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUser.Id;
            var restaurantRepo = await restaurantsRepository.CreateRestaurantAsync(restaurant);
            return mapper.Map<RestaurantDTO>(restaurantRepo);
        }
    }
}
