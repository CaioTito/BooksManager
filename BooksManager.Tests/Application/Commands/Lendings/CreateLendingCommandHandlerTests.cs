using BooksManager.Application.Commands.Lendings;
using BooksManager.Application.Commands.Users;
using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Core.Interfaces.Services;
using BooksManager.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.AutoMock;
using System.Security.Claims;

namespace BooksManager.Tests.Application.Commands.Lendings
{
    public class CreateLendingCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly CreateLendingCommandHandler _createLendingCommandHandler;

        public CreateLendingCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _createLendingCommandHandler = new CreateLendingCommandHandler(_mocker.GetMock<IUnitOfWork>().Object, _mocker.GetMock<IHttpContextAccessor>().Object);
        }

        [Fact]
        public async Task InputDataIsOK_Executed_ReturnUserId()
        {
            //Arrange
            var createLendingCommand = LendingMock.CreateLendingCommandFaker.Generate();

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Lendings)
                .Returns(_mocker.GetMock<ILendingRepository>().Object);

            var userClaims = new List<Claim>
            {
                new Claim("UserId", Guid.NewGuid().ToString())
            };
            var identity = new ClaimsIdentity(userClaims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            };

            _mocker.GetMock<IHttpContextAccessor>().Setup(x => x.HttpContext).Returns(httpContext);

            //Act
            var id = await _createLendingCommandHandler.Handle(createLendingCommand, new CancellationToken());

            //Assert
            Assert.IsType<Guid>(id);

            _mocker.GetMock<IUnitOfWork>().Verify(c => c.Lendings.CreateAsync(It.IsAny<Lending>()), Times.Once);
        }
    }
}
