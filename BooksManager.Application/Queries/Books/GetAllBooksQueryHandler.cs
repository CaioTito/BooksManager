using BooksManager.Application.ViewModels;
using BooksManager.Core.Interfaces.Repositories;
using MediatR;

namespace BooksManager.Application.Queries.Books
{
    public class GetAllBooksQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllBooksQuery, List<BookViewModel>>
    {
        public async Task<List<BookViewModel>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await unitOfWork.Books.GetAllAsync();

            return books
                .Select(b => new BookViewModel(b.Id, b.Title, b.Author, b.Isbn, b.YearOfPublication))
                .ToList();
        }
    }
}
