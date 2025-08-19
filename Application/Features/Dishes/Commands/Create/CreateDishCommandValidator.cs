using FluentValidation;

namespace Application.Features.Dishes.Commands.Create
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            RuleFor(command => command.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than zero.");
            RuleFor(command => command.KilogrammeCalories)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than zero.");
        }
    }
}
