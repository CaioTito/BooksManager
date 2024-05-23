using BooksManager.Core.Entities;
using Moq;

namespace BooksManager.Tests.Core.Entities
{
    public class LendingTests
    {
        [Fact]
        public void Lending_EmptyConstructor_ShouldInitializeWithDefaultValues()
        {
            // Act
            var lending = new Lending();

            // Assert
            Assert.NotNull(lending);
            Assert.Equal(Guid.Empty, lending.UserId);
            Assert.Equal(Guid.Empty, lending.BookId);
            Assert.Equal(DateTime.MinValue, lending.ReturnDate);
            Assert.NotNull(lending.Client);
            Assert.NotNull(lending.Book);
        }

        [Fact]
        public void Lending_ParametrizedConstructor_ShouldInitializeWithProvidedValues()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var returnDate = DateTime.Now.AddDays(14);

            // Act
            var lending = new Lending(userId, bookId, returnDate);

            // Assert
            Assert.Equal(userId, lending.UserId);
            Assert.Equal(bookId, lending.BookId);
            Assert.Equal(returnDate, lending.ReturnDate);
            Assert.NotNull(lending.Client);
            Assert.NotNull(lending.Book);
        }
    }
}
