using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task<Restaurant?> GetRestaurantByIdAsync(Guid id);
    }
}
