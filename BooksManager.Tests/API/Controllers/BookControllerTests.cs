using System.Net;
using System.Security.Claims;
using BooksManager.API.Controllers;
using BooksManager.Application.Commands.Books;
using BooksManager.Application.Queries.Books;
using BooksManager.Application.ViewModels;
using BooksManager.Tests.Mocks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BooksManager.Tests.API.Controllers
{
    public class BookControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly BookController _controller;

        public BookControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new BookController(_mediatorMock.Object);

            // Set up the user roles for authorization
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Role, "Administrator")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [Fact]
        public async Task Create_ReturnsCreatedResult()
        {
            // Arrange
            var command = new CreateBookCommand();
            var bookId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBookCommand>(), default)).ReturnsAsync(bookId);

            // Act
            var result = await _controller.Create(command);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, createdResult.StatusCode);
            Assert.Equal(bookId, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResultWithBooks()
        {
            // Arrange
            var books = BookMock.BookViewModelFaker.Generate(3);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllBooksQuery>(), default)).ReturnsAsync(books);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.Equal(books, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsOkResultWithBook()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = BookMock.BookViewModelFaker.Generate();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), default)).ReturnsAsync(book);

            // Act
            var result = await _controller.GetById(bookId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.Equal(book, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFoundResult()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), default)).ReturnsAsync((BookViewModel)null);

            // Act
            var result = await _controller.GetById(bookId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteBookCommand>(), default)).ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.Delete(bookId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
