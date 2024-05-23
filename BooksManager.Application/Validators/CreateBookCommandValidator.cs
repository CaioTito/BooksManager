using BooksManager.Application.Commands.Books;
using FluentValidation;

namespace BooksManager.Application.Validators
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(b => b.Author)
                .NotEmpty()
                .NotNull()
                .WithMessage("Author is required");

            RuleFor(b => b.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("Title is required");

            RuleFor(b => b.Isbn)
                .NotEmpty()
                .NotNull()
                .WithMessage("ISBN is required");

            RuleFor(b => b.YearOfPublication)
                .NotEmpty()
                .NotNull()
                .WithMessage("Year of publication is required");
        }
    }
}
