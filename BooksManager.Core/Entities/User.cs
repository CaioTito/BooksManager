namespace BooksManager.Core.Entities
{
    public class User(string name, string email) : EntityBase
    {
        public string Name { get; private set; } = name;
        public string Email { get; private set; } = email;
        public IEnumerable<Lending> Lendings { get; private set; }
    }
}
