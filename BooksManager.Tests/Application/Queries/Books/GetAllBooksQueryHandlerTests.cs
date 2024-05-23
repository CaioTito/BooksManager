using BooksManager.Application.Commands.Books;
using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Tests.Mocks;
using Moq.AutoMock;
using Moq;
using BooksManager.Application.Queries.Books;
using BooksManager.Application.ViewModels;

namespace BooksManager.Tests.Application.Queries.Books
{
    public class GetAllBooksQueryHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly GetAllBooksQueryHandler _getAllBookCommandHandler;

        public GetAllBooksQueryHandlerTests()
        {
            _mocker = new AutoMocker();
            _getAllBookCommandHandler = new GetAllBooksQueryHandler(_mocker.GetMock<IUnitOfWork>().Object);
        }

        [Fact]
        public async Task InputDataIsOK_Executed_ReturnAllBooks()
        {
            //Arrange
            var getAllBooksQuery = BookMock.GetAllBookQueryFaker.Generate();
            var books = BookMock.BookFaker.Generate(3);

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Books)
                .Returns(_mocker.GetMock<IBookRepository>().Object);

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Books.GetAllAsync())
                .ReturnsAsync(books);

            //Act
            var id = await _getAllBookCommandHandler.Handle(getAllBooksQuery, new CancellationToken());

            //Assert
            Assert.IsType<List<BookViewModel>>(id);

            _mocker.GetMock<IUnitOfWork>().Verify(c => c.Books.GetAllAsync(), Times.Once);
        }
    }
}
