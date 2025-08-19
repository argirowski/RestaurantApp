using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RestaurantsRepository(RestaurantsDBContext dBContext) : IRestaurantsRepository
    {
        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            var restaurants = await dBContext.Restaurants
                .Include(r => r.Dishes)
                .ToListAsync();
            return restaurants;
        }

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingRestaurantResultsAsync(string? searchParams, int pageSize, int pageNumber)
        {
            var searchParamsLower = searchParams?.ToLowerInvariant();

            var baseQuery = dBContext.Restaurants
                .Where(r => searchParamsLower == null || (r.Name.ToLowerInvariant().Contains(searchParamsLower) ||
                            r.Description.ToLowerInvariant().Contains(searchParamsLower)))
                .Include(r => r.Dishes);

            var totalCount = await baseQuery.CountAsync();

            var restaurants = await baseQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (restaurants, totalCount);
        }

        public async Task<Restaurant?> GetRestaurantByIdAsync(Guid id)
        {
            var restaurant = await dBContext.Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefaultAsync(r => r.Id == id);
            return restaurant;
        }
        public async Task<Restaurant> CreateRestaurantAsync(Restaurant restaurant)
        {
            dBContext.Restaurants.Add(restaurant);
            await dBContext.SaveChangesAsync();
            return restaurant;
        }

        public async Task DeleteRestaurantAsync(Restaurant restaurant)
        {
            dBContext.Restaurants.Remove(restaurant);
            await dBContext.SaveChangesAsync();
        }

        public async Task UpdateRestaurantAsync(Restaurant restaurant)
        {
            dBContext.Restaurants.Update(restaurant);
            await dBContext.SaveChangesAsync();
        }
    }
}
