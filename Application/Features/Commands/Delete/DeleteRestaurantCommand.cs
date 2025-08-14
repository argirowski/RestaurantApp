using MediatR;

namespace Application.Features.Commands.Delete
{
    public class DeleteRestaurantCommand(Guid id) : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
