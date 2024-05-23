using Microsoft.VisualBasic.FileIO;

namespace BooksManager.Core.Entities
{
    public class Book(string title, string author, string isbn, int yearOfPublication) : EntityBase
    {
        public Book() : this(string.Empty, string.Empty, string.Empty, 0)
        {
        }
        public string Title { get; private set; } = title;
        public string Author { get; private set; } = author;
        public string Isbn { get; private set; } = isbn;
        public int YearOfPublication { get; private set; } = yearOfPublication;
        public IEnumerable<Lending> Lendings { get; private set; } = [];
    }
}
