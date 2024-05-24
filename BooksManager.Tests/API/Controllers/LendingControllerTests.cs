using System.Net;
using System.Security.Claims;
using BooksManager.API.Controllers;
using BooksManager.Application.Commands.Lendings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BooksManager.Tests.API.Controllers
{
    public class LendingControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly LendingController _controller;

        public LendingControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new LendingController(_mediatorMock.Object);

            // Set up the user roles for authorization
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Administrator"),
                new Claim(ClaimTypes.Role, "Customer")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [Fact]
        public async Task Create_ReturnsOkResultWithId()
        {
            // Arrange
            var command = new CreateLendingCommand();
            var lendingId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateLendingCommand>(), default)).ReturnsAsync(lendingId);

            // Act
            var result = await _controller.Create(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.Equal(lendingId, okResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult()
        {
            // Arrange
            var lendingId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteLendingCommand>(), default)).ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.Delete(lendingId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
