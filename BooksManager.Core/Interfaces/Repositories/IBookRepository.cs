using BooksManager.Core.Entities;

namespace BooksManager.Core.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task CreateAsync(Book book);
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(Guid id);
    }
}
