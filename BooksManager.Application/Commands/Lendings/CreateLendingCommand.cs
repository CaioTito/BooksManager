using MediatR;

namespace BooksManager.Application.Commands.Lendings
{
    public class CreateLendingCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
