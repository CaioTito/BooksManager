using BooksManager.Application.ViewModels;
using BooksManager.Core.Interfaces.Repositories;
using MediatR;

namespace BooksManager.Application.Queries.Books
{
    public class GetBookByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBookByIdQuery, BookViewModel>
    {
        public async Task<BookViewModel> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await unitOfWork.Books.GetByIdAsync(request.Id);

            return new BookViewModel(book.Id, book.Title, book.Author, book.Isbn, book.YearOfPublication);
        }
    }
}