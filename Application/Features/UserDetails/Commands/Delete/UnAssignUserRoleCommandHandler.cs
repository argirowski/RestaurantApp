using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.UserDetails.Commands.Delete
{
    public class UnAssignUserRoleCommandHandler(
        ILogger<UnAssignUserRoleCommandHandler> logger,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager) : IRequestHandler<UnAssignUserRoleCommand, Unit>
    {
        public async Task<Unit> Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("UnAssigning role '{@Request}' to user with email '{UserEmail}'", request, request.UserEmail);

            var user = await userManager.FindByEmailAsync(request.UserEmail);

            if (user == null)
            {
                logger.LogWarning("User with email '{UserEmail}' not found", request.UserEmail);
                throw new NotFoundException(nameof(User), request.UserEmail);
            }

            var roleExists = await roleManager.FindByNameAsync(request.RoleName);

            if (roleExists == null)
            {
                logger.LogWarning("Role '{RoleName}' does not exist", request.RoleName);
                throw new NotFoundException(nameof(IdentityRole), request.RoleName);
            }

            await userManager.RemoveFromRoleAsync(user, roleExists.Name!);
            return Unit.Value;
        }
    }
}

