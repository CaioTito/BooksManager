using MediatR;

namespace BooksManager.Application.Commands.Users
{
    public class DeleteUserCommand(Guid id) : IRequest<Unit>
    {
        public Guid Id { get; set; } = id;
    }
}
