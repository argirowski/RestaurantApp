using FluentValidation;

namespace Application.Features.Commands.Update
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Restaurant Id must not be empty.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(3, 100)
                .WithMessage("Name must be between 3 and 100 characters.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.");
        }
    }
}
