using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class DishesRepository(RestaurantsDBContext dBContext) : IDishesRepository
    {
        public async Task<Guid> CreateDishAsync(Dish dish)
        {
            dBContext.Dishes.Add(dish);
            await dBContext.SaveChangesAsync();
            return dish.Id;
        }

        public async Task DeleteDishAsync(IEnumerable<Dish> dishes)
        {
            dBContext.Dishes.RemoveRange(dishes);
            await dBContext.SaveChangesAsync();
        }
    }
}
