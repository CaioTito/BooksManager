using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;
using MediatR;

namespace BooksManager.Application.Commands.Books
{
    public class CreateBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBookCommand, Guid>
    {
        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book(request.Title, request.Author, request.Isbn, request.YearOfPublication);

            await unitOfWork.Books.CreateAsync(book);
            await unitOfWork.SaveChangesAsync();

            return book.Id;
        }
    }
}
