using BooksManager.Core.Entities;

namespace BooksManager.Core.Interfaces.Repositories
{
    public interface ILendingRepository
    {
        Task CreateAsync(Lending lending);
        List<Lending> CheckLendingReturnDate();
        Task<Lending> GetByIdAsync(Guid id);
    }
}
