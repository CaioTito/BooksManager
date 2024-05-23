using MediatR;

namespace BooksManager.Application.Commands.Books
{
    public class CreateBookCommand : IRequest<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public int YearOfPublication { get; set; }
    }
}
