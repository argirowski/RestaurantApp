using Application.Features.Restaurants.Commands.Create;
using Domain.Enums;

namespace Application.Tests.Features.Restaurants.Commands.Create
{
    public class CreateRestaurantCommandTests
    {
        [Fact]
        public void Properties_AreSetAndGetCorrectly()
        {
            // Arrange
            var command = new CreateRestaurantCommand
            {
                Name = "Testaurant",
                Description = "A test restaurant",
                Category = RestaurantCategoryEnum.Japanese,
                HasDelivery = true,
                ContactEmail = "test@restaurant.com",
                ContactNumber = "123456789",
                City = "Tokyo",
                Street = "Main St",
                PostalCode = "100-0001"
            };

            // Assert
            Assert.Equal("Testaurant", command.Name);
            Assert.Equal("A test restaurant", command.Description);
            Assert.Equal(RestaurantCategoryEnum.Japanese, command.Category);
            Assert.True(command.HasDelivery);
            Assert.Equal("test@restaurant.com", command.ContactEmail);
            Assert.Equal("123456789", command.ContactNumber);
            Assert.Equal("Tokyo", command.City);
            Assert.Equal("Main St", command.Street);
            Assert.Equal("100-0001", command.PostalCode);
        }
    }
}
