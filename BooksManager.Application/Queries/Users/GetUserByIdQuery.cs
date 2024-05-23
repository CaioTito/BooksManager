using BooksManager.Application.ViewModels;
using MediatR;

namespace BooksManager.Application.Queries.Users
{
    public class GetUserByIdQuery(Guid id) : IRequest<UserViewModel>
    {
        public Guid Id { get; set; } = id;
    }
}
