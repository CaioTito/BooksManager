using BooksManager.Core.Enums;

namespace BooksManager.Core.Entities
{
    public class User(string name, string email, string password, Roles role) : EntityBase
    {
        public string Name { get; private set; } = name;
        public string Email { get; private set; } = email;
        public string Password { get; private set; } = password;
        public Roles Role { get; private set; } = role;
        public IEnumerable<Lending> Lendings { get; private set; }
    }
}
