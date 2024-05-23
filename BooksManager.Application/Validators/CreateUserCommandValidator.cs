using BooksManager.Application.Commands.Users;
using FluentValidation;

namespace BooksManager.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.Email)
                .EmailAddress()
                .WithMessage("E-mail is not valid");

            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name is required");
        }
    }
}
