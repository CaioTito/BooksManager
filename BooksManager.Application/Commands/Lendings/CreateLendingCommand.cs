using MediatR;

namespace BooksManager.Application.Commands.Lendings
{
    public class CreateLendingCommand : IRequest<Guid>
    {
        public Guid BookId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
