using BooksManager.Core.Entities;

namespace BooksManager.Tests.Core.Entities
{
    public class BookTests
    {
        [Fact]
        public void Book_EmptyConstructor_ShouldInitializeWithDefaultValues()
        {
            // Act
            var book = new Book();

            // Assert
            Assert.NotNull(book);
            Assert.Equal(string.Empty, book.Title);
            Assert.Equal(string.Empty, book.Author);
            Assert.Equal(string.Empty, book.Isbn);
            Assert.Equal(0, book.YearOfPublication);
            Assert.NotNull(book.Lendings);
        }

        [Fact]
        public void Book_ParametrizedConstructor_ShouldInitializeWithProvidedValues()
        {
            // Arrange
            var title = "Sample Title";
            var author = "Sample Author";
            var isbn = "1234567890";
            var yearOfPublication = 2021;

            // Act
            var book = new Book(title, author, isbn, yearOfPublication);

            // Assert
            Assert.Equal(title, book.Title);
            Assert.Equal(author, book.Author);
            Assert.Equal(isbn, book.Isbn);
            Assert.Equal(yearOfPublication, book.YearOfPublication);
            Assert.NotNull(book.Lendings);
        }
    }
}
