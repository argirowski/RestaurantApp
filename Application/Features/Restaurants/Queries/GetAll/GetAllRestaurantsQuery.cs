using Application.DTOs;
using MediatR;

namespace Application.Features.Restaurants.Queries.GetAll
{
    public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDTO>>
    {
    }
}
