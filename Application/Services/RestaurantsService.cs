using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger, IMapper mapper) : IRestaurantsService
    {
        public async Task<IEnumerable<RestaurantDTO>> GetAllRestaurantsAsync()
        {
            logger.LogInformation("Fetching all restaurants from the repository.");
            var restaurants = await restaurantsRepository.GetAllRestaurantsAsync();
            var restaurantDTOs = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
            return restaurantDTOs;
        }

        public async Task<RestaurantDTO?> GetRestaurantByIdAsync(Guid id)
        {
            logger.LogInformation("Fetching restaurant with ID: {Id}", id);
            var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(id);
            var restaurantDTO = mapper.Map<RestaurantDTO?>(restaurant);
            return restaurantDTO;
        }
        public async Task<RestaurantDTO> CreateRestaurantAsync(CreateRestaurantDTO createRestaurantDTO)
        {
            logger.LogInformation("Creating a new restaurant with name: {Name}", createRestaurantDTO.Name);
            var restaurant = mapper.Map<Restaurant>(createRestaurantDTO);
            var restaurantRepo = await restaurantsRepository.CreateRestaurantAsync(restaurant);
            return mapper.Map<RestaurantDTO>(restaurantRepo);
        }
    }
}
