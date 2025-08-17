using MediatR;

namespace Application.Features.UserDetails.Commands.Update
{
    public class UpdateUserDetailsCommand : IRequest<Unit>
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
