using BooksManager.Application.ViewModels;
using MediatR;

namespace BooksManager.Application.Queries.Books
{
    public class GetAllBooksQuery : IRequest<List<BookViewModel>>
    {
    }
}
