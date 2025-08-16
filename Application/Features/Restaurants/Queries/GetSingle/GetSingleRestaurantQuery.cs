using Application.DTOs;
using MediatR;

namespace Application.Features.Restaurants.Queries.GetSingle
{
    public class GetSingleRestaurantQuery : IRequest<RestaurantDTO>
    {
        public GetSingleRestaurantQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
