using MediatR;

namespace Application.Features.Restaurants.Commands.Update
{
    public class UpdateRestaurantCommand : IRequest<Unit>

    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool HasDelivery { get; set; }
    }
}
