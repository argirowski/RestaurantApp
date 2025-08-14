using FluentValidation;

namespace Application.Features.Commands.Delete
{
    public class DeleteRestaurantCommandValidator : AbstractValidator<DeleteRestaurantCommand>
    {
        public DeleteRestaurantCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Restaurant Id must not be empty.");
        }
    }
}
