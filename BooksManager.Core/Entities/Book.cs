namespace BooksManager.Core.Entities
{
    public class Book(string title, string author, string isbn, int yearOfPublication) : EntityBase
    {
        public string Title { get; private set; } = title;
        public string Author { get; private set; } = author;
        public string Isbn { get; private set; } = isbn;
        public int YearOfPublication { get; private set; } = yearOfPublication;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime UpdatedAt { get; private set; } = DateTime.Now;
        public DateTime? DeletedAt { get; private set; }
        public IEnumerable<Lending> Lendings { get; private set; }
    }
}
