using MediatR;

namespace Application.Features.UserDetails.Commands.Create
{
    public class AddUserRoleCommand : IRequest<Unit>
    {
        public string UserEmail { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}
