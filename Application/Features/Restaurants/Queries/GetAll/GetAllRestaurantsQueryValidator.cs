using Application.DTOs;
using FluentValidation;

namespace Application.Features.Restaurants.Queries.GetAll
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
    {
        private int[] allowedPageSizes = [5, 10, 15, 20, 25, 30];
        private string[] allowedSortByColumnNames = [nameof(RestaurantDTO.Name), nameof(RestaurantDTO.Category), nameof(RestaurantDTO.Description)];
        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                        .GreaterThanOrEqualTo(1)
                        .WithMessage("The Page Number must be at least one.");
            RuleFor(x => x.PageSize)
                .Must(x => allowedPageSizes.Contains(x))
                .WithMessage($"Page Size must be in [{string.Join(",", allowedPageSizes)}]");

            RuleFor(x => x.SortBy)
                .Must(x => allowedSortByColumnNames.Contains(x))
                .When(x => x.SortBy is not null)
                .WithMessage($"Sort By is optional,or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}
