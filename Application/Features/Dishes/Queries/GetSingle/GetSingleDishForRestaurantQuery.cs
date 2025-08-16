using Application.DTOs;
using MediatR;

namespace Application.Features.Dishes.Queries.GetSingle
{
    public class GetSingleDishForRestaurantQuery(Guid restaurantId, Guid dishId) : IRequest<DishDTO>
    {
        public Guid RestaurantId { get; } = restaurantId;
        public Guid DishId { get; } = dishId;
    }
}
