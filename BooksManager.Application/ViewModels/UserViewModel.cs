using System.Xml.Linq;

namespace BooksManager.Application.ViewModels
{
    public class UserViewModel(Guid id, string name, string email)
    {
        public Guid Id { get; private set; } = id;
        public string Name { get; private set; } = name;
        public string Email { get; private set; } = email;
    }
}
