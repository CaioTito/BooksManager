namespace BooksManager.Core.Interfaces.Services
{
    public interface IEmailService
    {
        void SendEmail(string email, string subject, string message);
    }
}
