namespace BooksManager.Application.ViewModels
{
    public class BookViewModel(Guid id, string title, string author, string isbn, int yearOfPublication)
    {
        public Guid Id { get; private set; } = id;
        public string Title { get; private set; } = title;
        public string Author { get; private set; } = author;
        public string Isbn { get; private set; } = isbn;
        public int YearOfPublication { get; private set; } = yearOfPublication;
    }
}
