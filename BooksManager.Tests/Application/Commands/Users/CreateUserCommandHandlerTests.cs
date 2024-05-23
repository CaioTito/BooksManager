using BooksManager.Application.Commands.Users;
using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Core.Interfaces.Services;
using BooksManager.Tests.Mocks;
using Moq;
using Moq.AutoMock;

namespace BooksManager.Tests.Application.Commands.Users
{
    public class CreateUserCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly CreateUserCommandHandler _createUserCommandHandler;

        public CreateUserCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _createUserCommandHandler = new CreateUserCommandHandler(_mocker.GetMock<IUnitOfWork>().Object);
        }

        [Fact]
        public async Task InputDataIsOK_Executed_ReturnUserId()
        {
            //Arrange
            var createUserCommand = UserMock.CreateUserCommandFaker.Generate();

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Users)
                .Returns(_mocker.GetMock<IUserRepository>().Object);

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Auth)
                .Returns(_mocker.GetMock<IAuthService>().Object);

            //Act
            var id = await _createUserCommandHandler.Handle(createUserCommand, new CancellationToken());

            //Assert
            Assert.IsType<Guid>(id);

            _mocker.GetMock<IUnitOfWork>().Verify(c => c.Users.CreateAsync(It.IsAny<User>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(c => c.Auth.GeneratePasswordHash(It.IsAny<string>()), Times.Once);
        }
    }
}
