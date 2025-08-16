using Application.DTOs;
using MediatR;

namespace Application.Features.Dishes.Queries.GetAll
{
    public class GetAllDishesForRestaurantQuery(Guid restaurantId) : IRequest<IEnumerable<DishDTO>>
    {
        public Guid RestaurantId { get; } = restaurantId;
    }
}
