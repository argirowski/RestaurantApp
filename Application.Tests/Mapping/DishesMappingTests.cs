using Application.DTOs;
using Application.Features.Dishes.Commands.Create;
using Application.Mapping;
using AutoMapper;
using Domain.Entities;

namespace Application.Tests.Mapping
{
    public class DishesMappingTests
    {
        private readonly IMapper _mapper;

        public DishesMappingTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DishesMapping>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Map_Dish_To_DishDTO_MapsAllProperties()
        {
            // Arrange
            var dish = new Dish { Id = Guid.NewGuid(), Name = "Sushi", Price = 12.5M };

            // Act
            var dto = _mapper.Map<DishDTO>(dish);

            // Assert
            Assert.Equal(dish.Id, dto.Id);
            Assert.Equal(dish.Name, dto.Name);
            Assert.Equal(dish.Price, dto.Price);
        }

        [Fact]
        public void Map_CreateDishCommand_To_Dish_MapsAllProperties()
        {
            // Arrange
            var command = new CreateDishCommand { Name = "Pizza", Price = 8.99M };

            // Act
            var dish = _mapper.Map<Dish>(command);

            // Assert
            Assert.Equal(command.Name, dish.Name);
            Assert.Equal(command.Price, dish.Price);
        }
    }
}
