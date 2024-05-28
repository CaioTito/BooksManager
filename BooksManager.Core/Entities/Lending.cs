namespace BooksManager.Core.Entities
{
    public class Lending(Guid userId, Guid bookId, DateTime returnDate) : EntityBase
    {
        public Lending() : this(Guid.Empty, Guid.Empty, DateTime.MinValue)
        {
        }

        public Guid UserId { get; private set; } = userId;
        public Guid BookId { get; private set; } = bookId;
        public DateTime ReturnDate { get; private set; } = returnDate;
        public User Client { get; private set; }
        public Book Book { get; private set; }
    }
}
