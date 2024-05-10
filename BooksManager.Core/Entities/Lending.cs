namespace BooksManager.Core.Entities
{
    public class Lending(Guid userId, Guid bookId) : EntityBase
    {
        public Guid UserId { get; private set; } = userId;
        public Guid BookId { get; private set; } = bookId;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime UpdatedAt { get; private set; } = DateTime.Now;
        public DateTime? DeletedAt { get; private set; }
        public User Client { get; private set; }
        public Book Book { get; private set; }
    }
}
