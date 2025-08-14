using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.GetAll
{
    public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDTO>>
    {
        public async Task<IEnumerable<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Fetching all restaurants from the repository.");
            var restaurants = await restaurantsRepository.GetAllRestaurantsAsync();
            var restaurantDTOs = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
            return restaurantDTOs;
        }
    }
}
