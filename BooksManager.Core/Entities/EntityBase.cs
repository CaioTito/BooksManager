namespace BooksManager.Core.Entities
{
    public class EntityBase
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime UpdatedAt { get; private set; } = DateTime.Now;
        public DateTime? DeletedAt { get; private set; }

        public void Delete(DateTime deletedAt)
        {
            DeletedAt = deletedAt;
        }
    }
}
