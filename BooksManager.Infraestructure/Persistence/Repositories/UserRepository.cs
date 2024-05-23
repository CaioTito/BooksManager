using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;

namespace BooksManager.Infraestructure.Persistence.Repositories
{
    public class UserRepository(BooksDbContext context) : IUserRepository
    {
        public async Task CreateAsync(User user)
        {
            await context.Users.AddAsync(user);
        }
    }
}
