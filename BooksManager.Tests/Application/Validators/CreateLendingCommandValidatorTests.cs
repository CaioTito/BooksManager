using BooksManager.Application.Commands.Lendings;
using BooksManager.Application.Validators;
using FluentValidation.TestHelper;

namespace BooksManager.Tests.Application.Validators
{
    public class CreateLendingCommandValidatorTests
    {
        private readonly CreateLendingCommandValidator _validator;

        public CreateLendingCommandValidatorTests()
        {
            _validator = new CreateLendingCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_BookId_Is_Empty()
        {
            var model = new CreateLendingCommand { BookId = Guid.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(lending => lending.BookId);
        }

        [Fact]
        public void Should_Not_Have_Error_When_BookId_Is_Not_Empty()
        {
            var model = new CreateLendingCommand { BookId = Guid.NewGuid() };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(lending => lending.BookId);
        }

        [Fact]
        public void Should_Have_Error_When_ReturnDate_Is_MinValue()
        {
            var model = new CreateLendingCommand { ReturnDate = DateTime.MinValue };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(lending => lending.ReturnDate);
        }

        [Fact]
        public void Should_Not_Have_Error_When_ReturnDate_Is_Not_Empty()
        {
            var model = new CreateLendingCommand { ReturnDate = DateTime.Now };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(lending => lending.ReturnDate);
        }
    }
}
