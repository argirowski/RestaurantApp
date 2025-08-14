using Application.DTOs;
using MediatR;

namespace Application.Features.Queries.GetAll
{
    public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDTO>>
    {
    }
}
