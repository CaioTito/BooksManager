namespace BooksManager.Core.Entities
{
    public class User(string name, string email) : EntityBase
    {
        public string Name { get; private set; } = name;
        public string Email { get; private set; } = email;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime UpdatedAt { get; private set; } = DateTime.Now;
        public DateTime? DeletedAt { get; private set; }
        public IEnumerable<Lending> Lendings { get; private set; }
    }
}
