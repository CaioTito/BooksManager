using BooksManager.Application.Commands.Books;
using BooksManager.Application.Validators;
using FluentValidation.TestHelper;

namespace BooksManager.Tests.Application.Validators
{
    public class CreateBookCommandValidatorTests
    {
        private readonly CreateBookCommandValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Author_Is_Empty()
        {
            var model = new CreateBookCommand { Author = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(book => book.Author);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Author_Is_Not_Empty()
        {
            var model = new CreateBookCommand { Author = "Author Name" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(book => book.Author);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            var model = new CreateBookCommand { Title = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(book => book.Title);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Title_Is_Not_Empty()
        {
            var model = new CreateBookCommand { Title = "Book Title" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(book => book.Title);
        }

        [Fact]
        public void Should_Have_Error_When_Isbn_Is_Empty()
        {
            var model = new CreateBookCommand { Isbn = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(book => book.Isbn);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Isbn_Is_Not_Empty()
        {
            var model = new CreateBookCommand { Isbn = "123-4567891234" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(book => book.Isbn);
        }

        [Fact]
        public void Should_Have_Error_When_YearOfPublication_Is_Empty()
        {
            var model = new CreateBookCommand { YearOfPublication = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(book => book.YearOfPublication);
        }

        [Fact]
        public void Should_Not_Have_Error_When_YearOfPublication_Is_Not_Empty()
        {
            var model = new CreateBookCommand { YearOfPublication = 2021 };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(book => book.YearOfPublication);
        }
    }
}
