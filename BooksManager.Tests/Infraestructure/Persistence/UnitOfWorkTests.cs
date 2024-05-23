using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Core.Interfaces.Services;
using BooksManager.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BooksManager.Tests.Infraestructure.Persistence
{
    public class UnitOfWorkTests
    {
        private readonly Mock<BooksDbContext> _mockContext;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<ILendingRepository> _mockLendingRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IDbContextTransaction> _mockTransaction;
        private readonly Mock<DatabaseFacade> _mockDatabaseFacade;

        public UnitOfWorkTests()
        {
            _mockContext = new Mock<BooksDbContext>(new DbContextOptions<BooksDbContext>());
            _mockAuthService = new Mock<IAuthService>();
            _mockBookRepository = new Mock<IBookRepository>();
            _mockEmailService = new Mock<IEmailService>();
            _mockLendingRepository = new Mock<ILendingRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockTransaction = new Mock<IDbContextTransaction>();
            _mockDatabaseFacade = new Mock<DatabaseFacade>(_mockContext.Object);

            _mockDatabaseFacade.Setup(m => m.BeginTransactionAsync(default)).ReturnsAsync(_mockTransaction.Object);
            _mockContext.Setup(m => m.Database).Returns(_mockDatabaseFacade.Object);
        }

        [Fact]
        public async Task BeginTransactionAsync_ShouldStartTransaction()
        {
            // Arrange
            var unitOfWork = new UnitOfWork(
                _mockContext.Object,
                _mockAuthService.Object,
                _mockBookRepository.Object,
                _mockEmailService.Object,
                _mockLendingRepository.Object,
                _mockUserRepository.Object
            );

            // Act
            await unitOfWork.BeginTransactionAsync();

            // Assert
            _mockDatabaseFacade.Verify(m => m.BeginTransactionAsync(default), Times.Once);
        }

        [Fact]
        public async Task CommitTransactionAsync_ShouldCommitTransaction()
        {
            // Arrange
            var unitOfWork = new UnitOfWork(
                _mockContext.Object,
                _mockAuthService.Object,
                _mockBookRepository.Object,
                _mockEmailService.Object,
                _mockLendingRepository.Object,
                _mockUserRepository.Object
            );

            await unitOfWork.BeginTransactionAsync();

            // Act
            await unitOfWork.CommitTransactionAsync();

            // Assert
            _mockTransaction.Verify(t => t.CommitAsync(default), Times.Once);
            _mockTransaction.Verify(t => t.RollbackAsync(default), Times.Never);
        }

        [Fact]
        public async Task CommitTransactionAsync_ShouldRollbackTransactionOnException()
        {
            // Arrange
            var unitOfWork = new UnitOfWork(
                _mockContext.Object,
                _mockAuthService.Object,
                _mockBookRepository.Object,
                _mockEmailService.Object,
                _mockLendingRepository.Object,
                _mockUserRepository.Object
            );

            await unitOfWork.BeginTransactionAsync();

            _mockTransaction.Setup(t => t.CommitAsync(default)).ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => unitOfWork.CommitTransactionAsync());
            _mockTransaction.Verify(t => t.RollbackAsync(default), Times.Once);
        }

        [Fact]
        public async Task SaveChangesAsync_ShouldSaveChanges()
        {
            // Arrange
            var unitOfWork = new UnitOfWork(
                _mockContext.Object,
                _mockAuthService.Object,
                _mockBookRepository.Object,
                _mockEmailService.Object,
                _mockLendingRepository.Object,
                _mockUserRepository.Object
            );

            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await unitOfWork.SaveChangesAsync();

            // Assert
            Assert.Equal(1, result);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public void Dispose_ShouldDisposeContext()
        {
            // Arrange
            var unitOfWork = new UnitOfWork(
                _mockContext.Object,
                _mockAuthService.Object,
                _mockBookRepository.Object,
                _mockEmailService.Object,
                _mockLendingRepository.Object,
                _mockUserRepository.Object
            );

            // Act
            unitOfWork.Dispose();

            // Assert
            _mockContext.Verify(m => m.Dispose(), Times.Once);
        }
    }
}
