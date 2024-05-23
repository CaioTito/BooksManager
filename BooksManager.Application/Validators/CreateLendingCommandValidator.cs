using BooksManager.Application.Commands.Lendings;
using FluentValidation;

namespace BooksManager.Application.Validators
{
    public class CreateLendingCommandValidator : AbstractValidator<CreateLendingCommand>
    {
        public CreateLendingCommandValidator()
        {
            RuleFor(l => l.BookId)
                .NotEmpty()
                .NotNull()
                .WithMessage("BookId is required");

            RuleFor(l => l.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserId is required");

            RuleFor(l => l.ReturnDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("Return date is required");
        }
    }
}
