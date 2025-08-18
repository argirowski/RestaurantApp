using MediatR;

namespace Application.Features.UserDetails.Commands.Delete
{
    public class UnAssignUserRoleCommand : IRequest<Unit>
    {
        public string UserEmail { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}
