using BooksManager.Core.Interfaces.Services;

namespace BooksManager.Core.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IAuthService Auth { get; }
        IBookRepository Books { get; }
        IEmailService Email { get; }
        ILendingRepository Lendings { get; }
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
    }
}
