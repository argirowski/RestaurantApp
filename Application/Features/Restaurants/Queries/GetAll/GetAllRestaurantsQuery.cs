using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Features.Restaurants.Queries.GetAll
{
    public class GetAllRestaurantsQuery : IRequest<PagedResults<RestaurantDTO>>
    {
        public string? SearchParams { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
