using Application.Common;
using Application.DTOs;
using Domain.Enums;
using MediatR;

namespace Application.Features.Restaurants.Queries.GetAll
{
    public class GetAllRestaurantsQuery : IRequest<PagedResults<RestaurantDTO>>
    {
        public string? SearchParams { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirectionEnum? SortDirection { get; set; }
    }
}
