using BooksManager.Application.Commands.Lendings;
using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Tests.Mocks;
using Moq;
using Moq.AutoMock;

namespace BooksManager.Tests.Application.Commands.Lendings
{
    public class DeleteLendingCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly DeleteLendingCommandHandler _deleteLendingCommandHandler;

        public DeleteLendingCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _deleteLendingCommandHandler = new DeleteLendingCommandHandler(_mocker.GetMock<IUnitOfWork>().Object);
        }

        [Fact]
        public async Task InputDataIsOK_Executed_CallDelete()
        {
            //Arrange
            var deleteLendingCommand = LendingMock.DeleteLendingCommandFaker.Generate();
            var lending = LendingMock.LendingFaker.Generate();

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Lendings)
                .Returns(_mocker.GetMock<ILendingRepository>().Object);

            _mocker.GetMock<IUnitOfWork>().Setup(uow => uow.Lendings.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(lending);

            //Act
            await _deleteLendingCommandHandler.Handle(deleteLendingCommand, new CancellationToken());

            //Assert
            Assert.NotNull(lending.DeletedAt);
        }
    }
}
