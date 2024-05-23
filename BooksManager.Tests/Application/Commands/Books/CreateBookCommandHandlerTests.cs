using BooksManager.Application.Commands.Books;
using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Tests.Mocks;
using Moq;
using Moq.AutoMock;

namespace BooksManager.Tests.Application.Commands.Books
{
    public class CreateBookCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly CreateBookCommandHandler _createBookCommandHandler;

        public CreateBookCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _createBookCommandHandler = new CreateBookCommandHandler(_mocker.GetMock<IUnitOfWork>().Object);
        }

        [Fact]
        public async Task InputDataIsOK_Executed_ReturnBookId()
        {
            //Arrange
            var createBookCommand = BookMock.CreateBookCommandFaker.Generate();

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Books)
                .Returns(_mocker.GetMock<IBookRepository>().Object);

            //Act
            var id = await _createBookCommandHandler.Handle(createBookCommand, new CancellationToken());

            //Assert
            Assert.IsType<Guid>(id);

            _mocker.GetMock<IUnitOfWork>().Verify(c => c.Books.CreateAsync(It.IsAny<Book>()), Times.Once);
        }
    }
}
