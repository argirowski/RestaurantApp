using Application.Common;
using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Restaurants.Queries.GetAll
{
    public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, PagedResults<RestaurantDTO>>
    {
        public async Task<PagedResults<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Fetching all restaurants from the repository.");
            var (restaurants, totalCount) = await restaurantsRepository.GetAllMatchingRestaurantResultsAsync(request.SearchParams, request.PageSize, request.PageNumber);
            var restaurantDTOs = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

            var result = new PagedResults<RestaurantDTO>
            (
                restaurantDTOs,
                totalCount,
                request.PageSize,
                request.PageNumber
            );
            return result;
        }
    }
}
