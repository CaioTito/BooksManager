using System.Xml.Linq;
using BooksManager.Core.Enums;

namespace BooksManager.Application.ViewModels
{
    public class UserViewModel(Guid id, string name, string email, Roles role)
    {
        public Guid Id { get; private set; } = id;
        public string Name { get; private set; } = name;
        public string Email { get; private set; } = email;
        public Roles Role { get; private set; } = role;
    }
}
