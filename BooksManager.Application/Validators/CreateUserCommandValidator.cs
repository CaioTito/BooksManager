using BooksManager.Application.Commands.Users;
using FluentValidation;
using System.Text.RegularExpressions;
using BooksManager.Core.Enums;

namespace BooksManager.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.Email)
                .EmailAddress()
                .WithMessage("E-mail is not valid");

            RuleFor(p => p.Password)
                .Must(ValidPassword)
                .WithMessage("Password must be contain 8 characters, 1 number, 1 capital letter, 1 lowercase letter e 1 special character");

            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name is required");

            RuleFor(p => p.Role)
                .Must(role => role == Roles.Administrator || role == Roles.Customer)
                .WithMessage("Role must be either 1 (Admin) or 2 (Customer)");

        }
        public bool ValidPassword(string password)
        {
            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%&+=]).*$");

            return regex.IsMatch(password);
        }
    }
}
