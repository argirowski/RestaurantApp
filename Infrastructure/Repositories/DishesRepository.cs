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

        public Task DeleteDishAsync(Dish dish)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Dish>> GetAllDishesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Dish?> GetDishByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDishAsync(Dish dish)
        {
            throw new NotImplementedException();
        }
    }
}
