using BooksManager.Application.Commands.Users;
using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Core.Interfaces.Services;
using BooksManager.Tests.Mocks;
using Moq;
using Moq.AutoMock;

namespace BooksManager.Tests.Application.Commands.Users
{
    public class LoginUserCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly LoginUserCommandHandler _loginUserCommandHandler;

        public LoginUserCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _loginUserCommandHandler = new LoginUserCommandHandler(_mocker.GetMock<IUnitOfWork>().Object);
        }

        [Fact]
        public async Task InputDataIsOK_Executed_ReturnToken()
        {
            //Arrange
            var loginUserCommand = UserMock.LoginUserCommandFaker.Generate();
            var user = UserMock.UserFaker.Generate();

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Users)
                .Returns(_mocker.GetMock<IUserRepository>().Object);

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Auth)
                .Returns(_mocker.GetMock<IAuthService>().Object);

            _mocker.GetMock<IUnitOfWork>()
                .Setup(uow => uow.Users.GetByPasswordAndEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            _mocker.GetMock<IUnitOfWork>()
                .Setup(uow => uow.Auth.GenerateJwtToken(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<Guid>()))
                .Returns("token");

            //Act
            var token = await _loginUserCommandHandler.Handle(loginUserCommand, new CancellationToken());

            //Assert
            Assert.IsType<string>(token);

            _mocker.GetMock<IUnitOfWork>().Verify(c => c.Users.GetByPasswordAndEmailAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(c => c.Auth.GeneratePasswordHash(It.IsAny<string>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(c => c.Auth.GenerateJwtToken(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task EmailOrPasswordWrong_Executed_ReturnNull()
        {
            //Arrange
            var loginUserCommand = UserMock.LoginUserCommandFaker.Generate();

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Users)
                .Returns(_mocker.GetMock<IUserRepository>().Object);

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Auth)
                .Returns(_mocker.GetMock<IAuthService>().Object);

            //Act
            var token = await _loginUserCommandHandler.Handle(loginUserCommand, new CancellationToken());

            //Assert
            Assert.Null(token);

            _mocker.GetMock<IUnitOfWork>().Verify(c => c.Users.GetByPasswordAndEmailAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(c => c.Auth.GeneratePasswordHash(It.IsAny<string>()), Times.Once);
        }
    }
}
