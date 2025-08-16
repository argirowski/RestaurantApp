using Application.DTOs;
using Domain.Enums;
using MediatR;

namespace Application.Features.Restaurants.Commands.Create
{
    public class CreateRestaurantCommand : IRequest<RestaurantDTO>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public RestaurantCategoryEnum Category { get; set; }
        public bool HasDelivery { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}
