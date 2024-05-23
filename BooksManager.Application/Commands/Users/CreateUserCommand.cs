using BooksManager.Core.Enums;
using MediatR;

namespace BooksManager.Application.Commands.Users
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Roles Role { get; set; }
    }
}
