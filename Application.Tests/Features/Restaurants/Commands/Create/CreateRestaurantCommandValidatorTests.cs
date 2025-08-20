using Application.Features.Restaurants.Commands.Create;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Application.Tests.Features.Restaurants.Commands.Create
{
    public class CreateRestaurantCommandValidatorTests
    {
        private readonly CreateRestaurantCommandValidator _validator = new();

        [Fact]
        public void ValidCommand_PassesValidation()
        {
            var command = new CreateRestaurantCommand
            {
                Name = "Testaurant",
                Description = "A test restaurant",
                Category = RestaurantCategoryEnum.Japanese,
                ContactEmail = "test@restaurant.com",
                ContactNumber = "+1234567890",
                PostalCode = "12-345"
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Name_TooShort_FailsValidation()
        {
            var command = new CreateRestaurantCommand { Name = "Te", Description = "desc", Category = RestaurantCategoryEnum.Japanese };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void Description_Empty_FailsValidation()
        {
            var command = new CreateRestaurantCommand { Name = "ValidName", Description = "", Category = RestaurantCategoryEnum.Japanese };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Description);
        }

        [Fact]
        public void Category_Invalid_FailsValidation()
        {
            var command = new CreateRestaurantCommand { Name = "ValidName", Description = "desc", Category = (RestaurantCategoryEnum)999 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Category);
        }

        [Fact]
        public void ContactEmail_Invalid_FailsValidation()
        {
            var command = new CreateRestaurantCommand { Name = "ValidName", Description = "desc", Category = RestaurantCategoryEnum.Japanese, ContactEmail = "invalidemail" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        }

        [Fact]
        public void ContactNumber_Invalid_FailsValidation()
        {
            var command = new CreateRestaurantCommand { Name = "ValidName", Description = "desc", Category = RestaurantCategoryEnum.Japanese, ContactNumber = "abc" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.ContactNumber);
        }

        [Fact]
        public void PostalCode_Invalid_FailsValidation()
        {
            var command = new CreateRestaurantCommand { Name = "ValidName", Description = "desc", Category = RestaurantCategoryEnum.Japanese, PostalCode = "12345" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }
    }
}
