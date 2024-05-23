using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksManager.Infraestructure.Persistence.Repositories
{
    public class UserRepository(BooksDbContext context) : IUserRepository
    {
        public async Task CreateAsync(User user)
        {
            await context.Users.AddAsync(user);
        }
        public async Task<User> GetByIdAsync(Guid id)
        {
            return await context.Users
                .FirstAsync(b => b.Id == id && b.DeletedAt == null);
        }

        public async Task<User> GetByPasswordAndEmailAsync(string email, string password)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password && u.DeletedAt == null);

            if (user == null)
                return null;

            return user;
        }
    }
}
