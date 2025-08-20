using Application.DTOs;
using Application.Features.Restaurants.Commands.Create;
using Application.Features.Restaurants.Commands.Update;
using Application.Mapping;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Tests.Mapping
{
    public class RestaurantsMappingTests
    {
        private readonly IMapper _mapper;

        public RestaurantsMappingTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<RestaurantsMapping>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Map_Restaurant_To_RestaurantDTO_MapsAddressAndDishes()
        {
            // Arrange
            var restaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "Testaurant",
                Category = RestaurantCategoryEnum.Japanese,
                Description = "Great food",
                Address = new Address { City = "Tokyo", Street = "Main St", PostalCode = "12345" },
                Dishes = new[] { new Dish { Id = Guid.NewGuid(), Name = "Sushi", Price = 10 } }
            };

            // Act
            var dto = _mapper.Map<RestaurantDTO>(restaurant);

            // Assert
            Assert.Equal(restaurant.Address.City, dto.City);
            Assert.Equal(restaurant.Address.Street, dto.Street);
            Assert.Equal(restaurant.Address.PostalCode, dto.PostalCode);
            Assert.Equal(restaurant.Dishes.Count, dto.Dishes.Count);
        }

        [Fact]
        public void Map_UpdateRestaurantCommand_To_Restaurant_MapsProperties()
        {
            // Arrange
            var command = new UpdateRestaurantCommand { Name = "Updated", Description = "New desc", HasDelivery = true };

            // Act
            var restaurant = _mapper.Map<Restaurant>(command);

            // Assert
            Assert.Equal(command.Name, restaurant.Name);
            Assert.Equal(command.HasDelivery, restaurant.HasDelivery);
            Assert.Equal(command.Description, restaurant.Description);
        }

        [Fact]
        public void Map_CreateRestaurantCommand_To_Restaurant_MapsAddress()
        {
            // Arrange
            var command = new CreateRestaurantCommand
            {
                City = "Paris",
                Street = "Rue de Rivoli",
                PostalCode = "75001"
            };

            // Act
            var restaurant = _mapper.Map<Restaurant>(command);

            // Assert
            Assert.NotNull(restaurant.Address);
            Assert.Equal(command.City, restaurant.Address.City);
            Assert.Equal(command.Street, restaurant.Address.Street);
            Assert.Equal(command.PostalCode, restaurant.Address.PostalCode);
        }
    }
}
