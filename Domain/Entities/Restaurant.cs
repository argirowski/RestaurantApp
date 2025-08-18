using Domain.Enums;

namespace Domain.Entities
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public RestaurantCategoryEnum Category { get; set; }
        public bool HasDelivery { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public Address? Address { get; set; }
        public List<Dish> Dishes { get; set; } = new List<Dish>();

        public User Owner { get; set; } = default!;
        public string OwnerId { get; set; } = default!;
    }
}
