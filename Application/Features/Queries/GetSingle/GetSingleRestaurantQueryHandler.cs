using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.GetSingle
{
    internal class GetSingleRestaurantQueryHandler(ILogger<GetSingleRestaurantQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetSingleRestaurantQuery, RestaurantDTO?>
    {
        public async Task<RestaurantDTO?> Handle(GetSingleRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Fetching restaurant with ID: {RestaurantId}", request.Id);
            var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.Id);
            var restaurantDTO = mapper.Map<RestaurantDTO?>(restaurant);
            return restaurantDTO;
        }
    }
}
