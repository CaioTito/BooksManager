using BooksManager.Core.Entities;
using BooksManager.Core.Enums;
using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Core.Interfaces.Services;
using BooksManager.Infraestructure.Jobs;
using Microsoft.Extensions.Logging;
using Moq;

namespace BooksManager.Tests.Infraestructure.Jobs
{
    public class EmailBackgroundJobTests
    {
        [Fact]
        public async Task Execute_ShouldSendEmails_WhenLendingsNearExpiration()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<EmailBackgroundJob>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var lendings = new List<Lending>
            {
                new Lending(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddDays(1))
            };

            unitOfWorkMock.Setup(u => u.Lendings.CheckLendingReturnDate()).Returns(lendings);
            unitOfWorkMock.Setup(u => u.Books.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Book("Test Book", "Author", "123123123", 2024));
            unitOfWorkMock.Setup(u => u.Users.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new User("Test User", "test@example.com", "test", Roles.Administrator));

            var emailServiceMock = new Mock<IEmailService>();
            unitOfWorkMock.Setup(u => u.Email).Returns(emailServiceMock.Object);

            var job = new EmailBackgroundJob(loggerMock.Object, unitOfWorkMock.Object);

            // Act
            await job.Execute(null);

            // Assert
            emailServiceMock.Verify(e => e.SendEmail("test@example.com",
                $"Empréstimos próximos ao vencimento - {DateTime.Now.ToString("dd/MM/yyyy")}",
                It.IsAny<string>()), Times.Once);

            loggerMock.Verify(
                l => l.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Job de verificação de empréstimos iniciada")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldNotSendEmails_WhenNoLendingsNearExpiration()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<EmailBackgroundJob>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var lendings = new List<Lending>();

            unitOfWorkMock.Setup(u => u.Lendings.CheckLendingReturnDate()).Returns(lendings);

            var job = new EmailBackgroundJob(loggerMock.Object, unitOfWorkMock.Object);

            // Act
            await job.Execute(null);

            // Assert
            unitOfWorkMock.Verify(u => u.Books.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            unitOfWorkMock.Verify(u => u.Users.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            unitOfWorkMock.Verify(u => u.Email.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);

            loggerMock.Verify(
                l => l.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Job de verificação de empréstimos iniciada")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }
    }
}
