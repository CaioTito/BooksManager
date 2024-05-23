using BooksManager.Application.ViewModels;
using BooksManager.Core.Interfaces.Repositories;
using MediatR;

namespace BooksManager.Application.Queries.Users
{
    public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserByIdQuery, UserViewModel>
    {
        public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(request.Id);

            return new UserViewModel(user.Id, user.Name, user.Email, user.Role);
        }
    }
}
