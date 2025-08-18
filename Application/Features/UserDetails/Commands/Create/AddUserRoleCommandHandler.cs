using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.UserDetails.Commands.Create
{
    public class AddUserRoleCommandHandler(
        ILogger<AddUserRoleCommandHandler> logger,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager) : IRequestHandler<AddUserRoleCommand, Unit>
    {
        public async Task<Unit> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Assigning role '{@Request}' to user with email '{UserEmail}'", request, request.UserEmail);

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

            await userManager.AddToRoleAsync(user, roleExists.Name!);
            return Unit.Value;
        }
    }
}
