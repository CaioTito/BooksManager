using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BooksManager.Application.Commands.Lendings
{
    public class CreateLendingCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateLendingCommand, Guid>
    {
        public async Task<Guid> Handle(CreateLendingCommand request, CancellationToken cancellationToken)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId") ?? throw new Exception("Error in UserId identification");
            Guid.TryParse(userIdClaim.Value, out var userId);

            var lending = new Lending(userId, request.BookId, request.ReturnDate);

            await unitOfWork.Lendings.CreateAsync(lending);
            await unitOfWork.SaveChangesAsync();

            return lending.Id;
        }
    }
}
