using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRestaurantsService
    {
        Task<IEnumerable<RestaurantDTO>> GetAllRestaurantsAsync();
        Task<RestaurantDTO?> GetRestaurantByIdAsync(Guid id);
    }
}
