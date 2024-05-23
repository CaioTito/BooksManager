using MediatR;

namespace BooksManager.Application.Commands.Lendings
{
    public class DeleteLendingCommand(Guid id) : IRequest<Unit>
    {
        public Guid Id { get; set; } = id;
    }
}
