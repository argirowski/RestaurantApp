using MediatR;

namespace Application.Features.Commands.Delete
{
    public class DeleteRestaurantCommand(Guid id) : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
