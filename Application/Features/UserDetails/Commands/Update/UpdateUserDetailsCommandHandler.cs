using Application.Claims;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.UserDetails.Commands.Update
{
    public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger, IUserContext userContext, IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Updating the user: {UserId} with {@Request}", user!.Id, request);

            var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

            if (dbUser == null)
            {
                logger.LogWarning("User with ID {UserId} not found", user.Id);
                throw new NotFoundException(nameof(User), user!.Id);
            }

            dbUser.DateOfBirth = request.DateOfBirth;
            dbUser.Nationality = request.Nationality;

            await userStore.UpdateAsync(dbUser, cancellationToken);

            logger.LogInformation("User {UserId} updated successfully", user.Id);
            return Unit.Value;
        }
    }
}
