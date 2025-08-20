using Application.Claims;

namespace Application.Tests.Claims
{
    public class CurrentUserTests
    {
        [Fact]
        public void IsInRole_ReturnsTrue_WhenRoleExists()
        {
            // Arrange
            var roles = new List<string> { "Admin", "User" };
            var user = new CurrentUser("1", "test@example.com", roles, "Macedonian", null);

            // Act
            var result = user.IsInRole("Admin");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsInRole_ReturnsFalse_WhenRoleDoesNotExist()
        {
            // Arrange
            var roles = new List<string> { "User" };
            var user = new CurrentUser("2", "user@example.com", roles, null, null);

            // Act
            var result = user.IsInRole("Admin");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Properties_AreSetCorrectly()
        {
            // Arrange
            var roles = new List<string> { "Owner" };
            var dob = new DateOnly(1990, 1, 1);
            var user = new CurrentUser("3", "owner@example.com", roles, "Japanese", dob);

            // Assert
            Assert.Equal("3", user.Id);
            Assert.Equal("owner@example.com", user.Email);
            Assert.Equal(roles, user.Roles);
            Assert.Equal("Japanese", user.Nationality);
            Assert.Equal(dob, user.DateOfBirth);
        }
    }
}
