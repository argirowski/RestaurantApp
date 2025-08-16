using MediatR;

namespace Application.Features.Dishes.Commands.Create
{
    public class CreateDishCommand : IRequest
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int? KilogrammeCalories { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
