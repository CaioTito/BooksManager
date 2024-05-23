using BooksManager.Core.Interfaces.Repositories;
using MediatR;

namespace BooksManager.Application.Commands.Users
{
    public class LoginUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<LoginUserCommand, string>
    {
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = unitOfWork.Auth.GeneratePasswordHash(request.Password);

            var user = await unitOfWork.Users.GetByPasswordAndEmailAsync(request.Email, passwordHash);

            if (user == null)
            {
                return null;
            }

            return unitOfWork.Auth.GenerateJwtToken(user.Email, (int)user.Role, user.Id);
        }
    }
}
