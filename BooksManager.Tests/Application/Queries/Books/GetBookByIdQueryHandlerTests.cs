using BooksManager.Application.Queries.Books;
using BooksManager.Application.ViewModels;
using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Tests.Mocks;
using Moq;
using Moq.AutoMock;

namespace BooksManager.Tests.Application.Queries.Books
{
    public class GetBookByIdQueryHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly GetBookByIdQueryHandler _getBookByIdQueryHandler;

        public GetBookByIdQueryHandlerTests()
        {
            _mocker = new AutoMocker();
            _getBookByIdQueryHandler = new GetBookByIdQueryHandler(_mocker.GetMock<IUnitOfWork>().Object);
        }

        [Fact]
        public async Task InputDataIsOK_Executed_ReturnBook()
        {
            //Arrange
            var getBookByIdQuery = BookMock.GetBookByIdQueryFaker.Generate();
            var book = BookMock.BookFaker.Generate();

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Books)
                .Returns(_mocker.GetMock<IBookRepository>().Object);

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Books.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(book);

            //Act
            var bookViewModel = await _getBookByIdQueryHandler.Handle(getBookByIdQuery, new CancellationToken());

            //Assert
            Assert.IsType<BookViewModel>(bookViewModel);

            _mocker.GetMock<IUnitOfWork>().Verify(c => c.Books.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
