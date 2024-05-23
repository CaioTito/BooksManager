using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;
using MediatR;

namespace BooksManager.Application.Commands.Users
{
    public class CreateUserCommandHandler (IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Guid>
    {
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var hashPassword = unitOfWork.Auth.GeneratePasswordHash(request.Password);
            
            var user = new User(request.Name, request.Email, hashPassword, request.Role);

            await unitOfWork.Users.CreateAsync(user);
            await unitOfWork.SaveChangesAsync();

            return user.Id;
        }
    }
}
