using BooksManager.Core.Interfaces.Repositories;
using MediatR;

namespace BooksManager.Application.Commands.Books
{
    public class DeleteBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBookCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await unitOfWork.Books.GetByIdAsync(request.Id);

            book.Delete(DateTime.Now);
            await unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
