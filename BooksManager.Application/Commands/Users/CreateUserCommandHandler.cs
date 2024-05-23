using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;
using MediatR;

namespace BooksManager.Application.Commands.Users
{
    public class CreateUserCommandHandler (IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Guid>
    {
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name, request.Email);

            await unitOfWork.Users.CreateAsync(user);
            await unitOfWork.SaveChangesAsync();

            return user.Id;
        }
    }
}
