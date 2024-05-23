using BooksManager.Application.ViewModels;
using MediatR;

namespace BooksManager.Application.Queries.Books
{
    public class GetBookByIdQuery(Guid id) : IRequest<BookViewModel>
    {
        public Guid Id { get; set; } = id;
    }
}
