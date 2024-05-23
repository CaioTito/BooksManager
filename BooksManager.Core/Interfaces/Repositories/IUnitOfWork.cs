namespace BooksManager.Core.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IBookRepository Books { get; }
        ILendingRepository Lendings { get; }
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
    }
}
