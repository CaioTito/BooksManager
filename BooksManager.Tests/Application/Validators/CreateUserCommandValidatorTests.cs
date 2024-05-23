using BooksManager.Application.Commands.Users;
using BooksManager.Application.Validators;
using BooksManager.Core.Enums;
using FluentValidation.TestHelper;

namespace BooksManager.Tests.Application.Validators
{
    public class CreateUserCommandValidatorTests
    {
        private readonly CreateUserCommandValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            var model = new CreateUserCommand { Email = "invalidEmail" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Email);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Email_Is_Valid()
        {
            var model = new CreateUserCommand { Email = "valid.email@example.com" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(user => user.Email);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Invalid()
        {
            var model = new CreateUserCommand { Password = "weak" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Password);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Password_Is_Valid()
        {
            var model = new CreateUserCommand { Password = "Strong1!" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(user => user.Password);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new CreateUserCommand { Name = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Name);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Name_Is_Not_Empty()
        {
            var model = new CreateUserCommand { Name = "John Doe" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(user => user.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Role_Is_Invalid()
        {
            var model = new CreateUserCommand { Role = (Roles)99 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Role);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Role_Is_Valid()
        {
            var model = new CreateUserCommand { Role = Roles.Administrator };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(user => user.Role);

            model = new CreateUserCommand { Role = Roles.Customer };
            result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(user => user.Role);
        }
    }
}
