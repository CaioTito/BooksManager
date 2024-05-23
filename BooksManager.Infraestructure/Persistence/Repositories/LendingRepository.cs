using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksManager.Infraestructure.Persistence.Repositories
{
    public class LendingRepository(BooksDbContext context) : ILendingRepository
    {
        public async Task CreateAsync(Lending lending)
        {
            await context.Lendings.AddAsync(lending);
        }

        public List<Lending> CheckLendingReturnDate()
        {
            var products = context.Lendings.Where(p => EF.Functions.DateDiffDay(DateTime.Now, p.ReturnDate) < 3 && p.DeletedAt == null).ToList();

            if (products == null)
                return null;

            return products;
        }

        public async Task<Lending> GetByIdAsync(Guid id)
        {
            return await context.Lendings
                .FirstAsync(b => b.Id == id && b.DeletedAt == null);
        }
    }
}
