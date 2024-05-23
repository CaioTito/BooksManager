namespace BooksManager.Core.Interfaces.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, int role, Guid id);
        string GeneratePasswordHash(string password);
    }
}
