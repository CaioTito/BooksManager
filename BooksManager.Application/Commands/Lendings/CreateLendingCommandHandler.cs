using BooksManager.Core.Entities;
using BooksManager.Core.Interfaces.Repositories;
using MediatR;

namespace BooksManager.Application.Commands.Lendings
{
    public class CreateLendingCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateLendingCommand, Guid>
    {
        public async Task<Guid> Handle(CreateLendingCommand request, CancellationToken cancellationToken)
        {
            var lending = new Lending(request.UserId, request.BookId, request.ReturnDate);

            await unitOfWork.Lendings.CreateAsync(lending);
            await unitOfWork.SaveChangesAsync();

            return lending.Id;
        }
    }
}
