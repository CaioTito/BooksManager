using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;

namespace BooksManager.Infraestructure.Persistence.Repositories
{
    public class LendingRepository(BooksDbContext context) : ILendingRepository
    {
        public async Task CreateAsync(Lending lending)
        {
            await context.Lendings.AddAsync(lending);
        }
    }
}
