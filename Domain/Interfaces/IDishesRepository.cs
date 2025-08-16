using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDishesRepository
    {
        Task<IEnumerable<Dish>> GetAllDishesAsync();
        Task<Dish?> GetDishByIdAsync(Guid id);
        Task<Guid> CreateDishAsync(Dish dish);
        Task DeleteDishAsync(Dish dish);
        Task UpdateDishAsync(Dish dish);
    }
}
