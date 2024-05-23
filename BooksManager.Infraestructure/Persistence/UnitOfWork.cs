using BooksManager.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace BooksManager.Infraestructure.Persistence
{
    public class UnitOfWork(BooksDbContext context, IBookRepository books, ILendingRepository lendings, IUserRepository users) : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        public IBookRepository Books { get; } = books;
        public ILendingRepository Lendings { get; } = lendings;
        public IUserRepository Users { get; } = users;

        public async Task BeginTransactionAsync()
        {
            _transaction = await context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
    }
}
