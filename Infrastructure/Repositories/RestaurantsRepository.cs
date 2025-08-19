using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingRestaurantResultsAsync(
            string? searchParams,
             int pageSize, int pageNumber,
              string? sortBy,
               SortDirectionEnum sortDirectionEnum)
        {
            var searchPhraseLower = searchParams?.ToLower();

            var baseQuery = dBContext
                .Restaurants
                .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                       || r.Description.ToLower().Contains(searchPhraseLower)));

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name), r => r.Name },
                { nameof(Restaurant.Description), r => r.Description },
                { nameof(Restaurant.Category), r => r.Category },
            };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirectionEnum == SortDirectionEnum.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var restaurants = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (restaurants, totalCount);
        }
    }
}