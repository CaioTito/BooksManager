using BooksManager.Application.Commands.Books;
using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Tests.Mocks;
using Moq;
using Moq.AutoMock;

namespace BooksManager.Tests.Application.Commands.Books
{
    public class DeleteBookCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly DeleteBookCommandHandler _deleteBookCommandHandler;

        public DeleteBookCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _deleteBookCommandHandler = new DeleteBookCommandHandler(_mocker.GetMock<IUnitOfWork>().Object);
        }

        [Fact]
        public async Task InputDataIsOK_Executed_CallDelete()
        {
            //Arrange
            var deleteBookCommand = BookMock.DeleteBookCommandFaker.Generate();
            var book = BookMock.BookFaker.Generate();

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Books)
                .Returns(_mocker.GetMock<IBookRepository>().Object);

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Books.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(book);

            //Act
            await _deleteBookCommandHandler.Handle(deleteBookCommand, new CancellationToken());

            //Assert
            Assert.NotNull(book.DeletedAt);
        }
    }
}
