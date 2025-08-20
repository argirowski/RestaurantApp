using Application.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace Application.Tests.Claims
{
    public class UserContextTests
    {
        private ClaimsPrincipal CreateClaimsPrincipal(
            string userId = "1",
            string email = "test@example.com",
            IEnumerable<string>? roles = null,
            string? nationality = null,
            string? dateOfBirth = null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email)
            };
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            if (nationality != null)
            {
                claims.Add(new Claim("Nationality", nationality));
            }
            if (dateOfBirth != null)
            {
                claims.Add(new Claim("DateOfBirth", dateOfBirth));
            }
            return new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthType"));
        }

        [Fact]
        public void GetCurrentUser_ReturnsCurrentUser_WhenAuthenticated()
        {
            // Arrange
            var principal = CreateClaimsPrincipal("1", "test@example.com", new[] { "Admin", "User" }, "Macedonian", "1990-01-01");
            var httpContext = new DefaultHttpContext { User = principal };
            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns(httpContext);
            var userContext = new UserContext(accessorMock.Object);

            // Act
            var currentUser = userContext.GetCurrentUser();

            // Assert
            Assert.NotNull(currentUser);
            Assert.Equal("1", currentUser.Id);
            Assert.Equal("test@example.com", currentUser.Email);
            Assert.Contains("Admin", currentUser.Roles);
            Assert.Contains("User", currentUser.Roles);
            Assert.Equal("Macedonian", currentUser.Nationality);
            Assert.Equal(new DateOnly(1990, 1, 1), currentUser.DateOfBirth);
        }

        [Fact]
        public void GetCurrentUser_ReturnsNull_WhenNotAuthenticated()
        {
            // Arrange
            var principal = new ClaimsPrincipal(new ClaimsIdentity()); // Not authenticated
            var httpContext = new DefaultHttpContext { User = principal };
            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns(httpContext);
            var userContext = new UserContext(accessorMock.Object);

            // Act
            var result = userContext.GetCurrentUser();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetCurrentUser_ThrowsException_WhenNoUserContext()
        {
            // Arrange
            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns((HttpContext)null);
            var userContext = new UserContext(accessorMock.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => userContext.GetCurrentUser());
        }
    }
}
