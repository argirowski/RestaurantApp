using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task<Restaurant?> GetRestaurantByIdAsync(Guid id);
        Task<Restaurant> CreateRestaurantAsync(Restaurant restaurant);
        Task DeleteRestaurantAsync(Restaurant restaurant);
        Task UpdateRestaurantAsync(Restaurant restaurant);
        Task<(IEnumerable<Restaurant>, int)> GetAllMatchingRestaurantResultsAsync(string? searchParams, int pageSize, int pageNumber, string? sortBy, SortDirectionEnum sortDirectionEnum);
    }
}
