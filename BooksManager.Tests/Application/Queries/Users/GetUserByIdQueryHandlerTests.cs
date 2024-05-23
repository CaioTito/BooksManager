using BooksManager.Application.Queries.Users;
using BooksManager.Application.ViewModels;
using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Tests.Mocks;
using Moq;
using Moq.AutoMock;

namespace BooksManager.Tests.Application.Queries.Users
{
    public class GetUserByIdQueryHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly GetUserByIdQueryHandler _getUserByIdQueryHandler;

        public GetUserByIdQueryHandlerTests()
        {
            _mocker = new AutoMocker();
            _getUserByIdQueryHandler = new GetUserByIdQueryHandler(_mocker.GetMock<IUnitOfWork>().Object);
        }

        [Fact]
        public async Task InputDataIsOK_Executed_ReturnBook()
        {
            //Arrange
            var getUserByIdQuery = UserMock.GetUserByIdQueryFaker.Generate();
            var user = UserMock.UserFaker.Generate();

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Users)
                .Returns(_mocker.GetMock<IUserRepository>().Object);

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Users.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            //Act
            var userViewModel = await _getUserByIdQueryHandler.Handle(getUserByIdQuery, new CancellationToken());

            //Assert
            Assert.IsType<UserViewModel>(userViewModel);

            _mocker.GetMock<IUnitOfWork>().Verify(c => c.Users.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
