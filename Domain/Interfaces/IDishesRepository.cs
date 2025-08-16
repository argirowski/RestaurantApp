using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDishesRepository
    {
        Task<Guid> CreateDishAsync(Dish dish);
        Task DeleteDishAsync(IEnumerable<Dish> dishes);
    }
}
