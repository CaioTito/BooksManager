using MediatR;

namespace BooksManager.Application.Commands.Books
{
    public class DeleteBookCommand(Guid id) : IRequest<Unit>
    {
        public Guid Id { get; set; } = id;
    }
}
