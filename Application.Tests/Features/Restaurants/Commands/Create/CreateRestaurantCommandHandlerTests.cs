using Application.Claims;
using Application.DTOs;
using Application.Features.Restaurants.Commands.Create;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Tests.Features.Restaurants.Commands.Create
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact]
        public async Task Handle_CreatesRestaurantAndReturnsDTO()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
            var mapperMock = new Mock<IMapper>();
            var repoMock = new Mock<IRestaurantsRepository>();
            var userContextMock = new Mock<IUserContext>();

            var command = new CreateRestaurantCommand
            {
                Name = "Testaurant",
                Description = "A test restaurant",
                Category = Domain.Enums.RestaurantCategoryEnum.Japanese,
                HasDelivery = true,
                City = "Tokyo",
                Street = "Main St",
                PostalCode = "100-0001"
            };

            var restaurant = new Restaurant { Id = Guid.NewGuid(), Name = command.Name, Description = command.Description, Category = command.Category, OwnerId = "user-1" };
            var restaurantDTO = new RestaurantDTO { Id = restaurant.Id, Name = restaurant.Name, Description = restaurant.Description, Category = restaurant.Category.ToString() };
            var currentUser = new CurrentUser("user-1", "user@example.com", new[] { "Owner" }, "Japanese", null);

            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);
            mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);
            repoMock.Setup(r => r.CreateRestaurantAsync(restaurant)).ReturnsAsync(restaurant);
            mapperMock.Setup(m => m.Map<RestaurantDTO>(restaurant)).Returns(restaurantDTO);

            var handler = new CreateRestaurantCommandHandler(loggerMock.Object, mapperMock.Object, repoMock.Object, userContextMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(restaurantDTO.Id, result.Id);
            Assert.Equal(restaurantDTO.Name, result.Name);
            Assert.Equal(restaurantDTO.Description, result.Description);
            Assert.Equal(restaurantDTO.Category, result.Category);
            repoMock.Verify(r => r.CreateRestaurantAsync(restaurant), Times.Once);
            mapperMock.Verify(m => m.Map<Restaurant>(command), Times.Once);
            mapperMock.Verify(m => m.Map<RestaurantDTO>(restaurant), Times.Once);
        }
    }
}
