using BooksManager.Core.Interfaces.Repositories;
using BooksManager.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Text.Json;
using BooksManager.Core.Entities;
using System.Text.Json.Serialization;
using BooksManager.Infraestructure.Extensions;

namespace BooksManager.Infraestructure.Jobs
{
    [DisallowConcurrentExecution]
    public class EmailBackgroundJob(
        ILogger<EmailBackgroundJob> logger,
        IUnitOfWork unitOfWork)
        : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            logger.LogInformation("Job de verificação de empréstimos iniciada");

            var lendingsNearExpiration = unitOfWork.Lendings.CheckLendingReturnDate();

            if (lendingsNearExpiration.Count != 0)
            {
                foreach (var lending in lendingsNearExpiration)
                {
                    var book = await unitOfWork.Books.GetByIdAsync(lending.BookId);
                    var user = await unitOfWork.Users.GetByIdAsync(lending.UserId);
                    var content = EmailExtensions.EmailTemplate(user.Name, book.Title, lending.ReturnDate.ToShortDateString());

                    unitOfWork.Email.SendEmail(user.Email,
                        $"Empréstimos próximos ao vencimento - {DateTime.Now.ToString("dd/MM/yyyy")}",
                        content);
                }
                
            }
        }
    }
}
