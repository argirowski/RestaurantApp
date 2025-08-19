using Application.Common;
using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Restaurants.Queries.GetAll
{
    public class GetAllRestaurantsQueryHandler(
        ILogger<GetAllRestaurantsQueryHandler> logger,
         IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, PagedResults<RestaurantDTO>>
    {
        public async Task<PagedResults<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants");
            var (restaurants, totalCount) = await restaurantsRepository.GetAllMatchingRestaurantResultsAsync(
                request.SearchParams,
                request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection);

            var restaurantsDTOs = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

            var result = new PagedResults<RestaurantDTO>(restaurantsDTOs, totalCount, request.PageSize, request.PageNumber);
            return result;

        }
    }
}
