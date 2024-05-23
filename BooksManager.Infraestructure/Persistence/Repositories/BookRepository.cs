using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksManager.Infraestructure.Persistence.Repositories
{
    public class BookRepository(BooksDbContext context) : IBookRepository
    {

        public async Task CreateAsync(Book book)
        {
            await context.Books.AddAsync(book);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await context.Books
                                .AsNoTracking()
                                .Where(b => b.DeletedAt == null)
                                .ToListAsync();
        }

        public async Task<Book> GetByIdAsync(Guid id)
        {
            return await context.Books
                .FirstAsync(b => b.Id == id && b.DeletedAt == null);
        }
    }
}
