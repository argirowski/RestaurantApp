namespace Application.DTOs
{
    public class DishDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int? KilogrammeCalories { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
