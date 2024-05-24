using BooksManager.API.Controllers;
using BooksManager.Application.Commands.Users;
using BooksManager.Application.Queries.Users;
using BooksManager.Application.ViewModels;
using BooksManager.Tests.Mocks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Security.Claims;

namespace BooksManager.Tests.API.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new UserController(_mediatorMock.Object);

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
        public async Task GetById_ReturnsOkResultWithUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = UserMock.UserViewModelFaker.Generate();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetById(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.Equal(user, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFoundResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), default)).ReturnsAsync((UserViewModel)null);

            // Act
            var result = await _controller.GetById(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedResult()
        {
            // Arrange
            var command = new CreateUserCommand();
            var userId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), default)).ReturnsAsync(userId);

            // Act
            var result = await _controller.Create(command);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, createdResult.StatusCode);
            Assert.Equal(userId, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task Login_ReturnsOkResultWithToken()
        {
            // Arrange
            var loginCommand = new LoginUserCommand();
            var token = "some-jwt-token";
            _mediatorMock.Setup(m => m.Send(It.IsAny<LoginUserCommand>(), default)).ReturnsAsync(token);

            // Act
            var result = await _controller.Login(loginCommand);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), default)).ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.Delete(userId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
