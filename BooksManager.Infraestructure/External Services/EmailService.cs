using System.Net;
using System.Net.Mail;
using BooksManager.Core.Interfaces.Services;

namespace BooksManager.Infraestructure.External_Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("caiotiito@gmail.com", "dykx swzl yrss doxq")
            };

            using (var mensagem = new MailMessage("caiotiito@gmail.com", email)
                   {
                       Subject = subject,
                       Body = message,
                       IsBodyHtml = true
                   })

            client.Send(mensagem);
        }
    }
}
