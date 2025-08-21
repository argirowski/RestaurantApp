using Application.Features.UserDetails.Commands.Create;
using Application.Features.UserDetails.Commands.Delete;
using Application.Features.UserDetails.Commands.Update;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPut("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand updateUserDetailsCommand)
        {
            await mediator.Send(updateUserDetailsCommand);
            return NoContent();
        }

        [HttpPost("userRole")]
        [Authorize(UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRole(AddUserRoleCommand addUserRoleCommand)
        {
            await mediator.Send(addUserRoleCommand);
            return NoContent();
        }

        [HttpDelete("userRole")]
        [Authorize(UserRoles.Admin)]
        public async Task<IActionResult> UnAssignUserRole(UnAssignUserRoleCommand unAssignUserRoleCommand)
        {
            await mediator.Send(unAssignUserRoleCommand);
            return NoContent();
        }

    }
}
