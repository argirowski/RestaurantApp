using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Seed
{
    public class RestaurantSeeder(RestaurantsDBContext dBContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dBContext.Database.CanConnectAsync())
            {
                if (!dBContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dBContext.Restaurants.AddRange(restaurants);
                    await dBContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Id = Guid.Parse("a3f9c8b2-1e4f-4d6a-9b7e-2c3f8a9d2e1f"),
                    Name = "Pasta Palace",
                    Description = "Authentic Italian pasta dishes.",
                    Category = "Italian",
                    HasDelivery = true,
                    ContactEmail = "info@pastapalace.com",
                    ContactNumber = "123-456-7890",
                    Address = new Address
                    {
                        City = "Rome",
                        PostalCode = "00100",
                        Street = "123 Pasta"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Id = Guid.Parse("d2e4a1c7-9f3b-4a6e-8c1d-7b2f9e3a4c5d"),
                            Name = "Spaghetti Carbonara",
                            Description = "Classic Roman pasta with eggs, cheese, pancetta, and pepper.",
                            Price = 12.99m,
                            RestaurantId = Guid.Parse("f3a7c9e2-6d1b-4f8e-9a2c-1e3b7d5f4c9a")
                        },
                        new Dish
                        {
                            Id = Guid.Parse("f7b1d9e3-2c4a-4f8e-9a6b-1d3e7c2f5a9b"),
                            Name = "Margherita Pizza",
                            Description = "Traditional pizza with fresh tomatoes, mozzarella, and basil.",
                            Price = 10.99m,
                            RestaurantId = Guid.Parse("f3a7c9e2-6d1b-4f8e-9a2c-1e3b7d5f4c9a")
                        }
                    }
                },
                new Restaurant
                {
                    Id = Guid.Parse("c9e2f1a4-3b7d-4a9e-8f2c-6d1a3b9e7f4c"),
                    Name = "Sushi Spot",
                    Description = "Fresh sushi and sashimi.",
                    Category = "Japanese",
                    HasDelivery = true,
                    ContactEmail = "contact@sushispot.com",
                    ContactNumber = "987-654-3210",
                    Address = new Address
                    {
                        City = "Tokyo",
                        PostalCode = "100-0001",
                        Street = "456 Sushi"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Id = Guid.Parse("e1a3c9f7-5d2b-4f6e-9c8a-3b7e1d2f4a6c"),
                            Name = "California Roll",
                            Description = "Sushi roll with crab, avocado, and cucumber.",
                            Price = 8.99m,
                            RestaurantId = Guid.Parse("d1f3a9c7-8b2e-4f6e-9a3c-2e7d1b5f4c8a")

                        },
                        new Dish
                        {
                            Id = Guid.Parse("b2f9e3a1-7c4d-4a6e-8f1b-2d3c9a7e5f1d"),
                            Name = "Salmon Sashimi",
                            Description = "Fresh slices of salmon served with wasabi and soy sauce.",
                            Price = 14.99m,
                            RestaurantId = Guid.Parse("d1f3a9c7-8b2e-4f6e-9a3c-2e7d1b5f4c8a")
                        }
                    }
                },
            };
            return restaurants;
        }
    }
}
