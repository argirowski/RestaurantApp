using FluentValidation;

namespace Application.Features.Commands.Create
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(n => n.Name).Length(3, 100);
            RuleFor(n => n.Description).NotEmpty().WithMessage("Description Is Required.");
            RuleFor(n => n.Category)
                .IsInEnum()
                .WithMessage("Please enter a valid category");
            RuleFor(e => e.ContactEmail).EmailAddress().WithMessage("Please enter a valid email address");
            RuleFor(e => e.ContactNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Please enter a valid phone number in E.164 format (e.g., +1234567890)");
            RuleFor(p => p.PostalCode).Matches(@"^\d{2}-\d{3}$").WithMessage("Please enter a valid postal code in format (xx-xxx)");
        }
    }
}
