using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Claims
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("User context is not present.");
            }
            if (!user.Identity.IsAuthenticated || user.Identity == null)
            {
                return null;
            }

            var userId = user.FindFirst(t => t.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(t => t.Type == ClaimTypes.Email)!.Value;
            var roles = user.Claims.Where(t => t.Type == ClaimTypes.Role)!
                                       .Select(t => t.Value);

            return new CurrentUser(userId, email, roles);
        }
    }
}
