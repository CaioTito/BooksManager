using BooksManager.Core.Entities;

namespace BooksManager.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByPasswordAndEmailAsync(string email, string password);
    }
}
