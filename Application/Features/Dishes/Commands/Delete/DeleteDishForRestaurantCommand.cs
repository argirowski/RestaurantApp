using MediatR;

namespace Application.Features.Dishes.Commands.Delete
{
    public class DeleteDishForRestaurantCommand(Guid restaurantId) : IRequest<Unit>
    {
        public Guid RestaurantId { get; } = restaurantId;
    }
}
