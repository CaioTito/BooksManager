using BooksManager.Application.Commands.Lendings;
using BooksManager.Application.Commands.Users;
using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Tests.Mocks;
using Moq;
using Moq.AutoMock;

namespace BooksManager.Tests.Application.Commands.Users
{
    public class DeleteUserCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly DeleteUserCommandHandler _deleteUserCommandHandler;

        public DeleteUserCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _deleteUserCommandHandler = new DeleteUserCommandHandler(_mocker.GetMock<IUnitOfWork>().Object);
        }

        [Fact]
        public async Task InputDataIsOK_Executed_CallDelete()
        {
            //Arrange
            var deleteUserCommand = UserMock.DeleteUserCommandFaker.Generate();
            var user = UserMock.UserFaker.Generate();

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Users)
                .Returns(_mocker.GetMock<IUserRepository>().Object);

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Users.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            //Act
            await _deleteUserCommandHandler.Handle(deleteUserCommand, new CancellationToken());

            //Assert
            Assert.NotNull(user.DeletedAt);
        }
    }
}
