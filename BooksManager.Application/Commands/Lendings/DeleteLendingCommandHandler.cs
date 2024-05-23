using BooksManager.Core.Interfaces.Repositories;
using MediatR;

namespace BooksManager.Application.Commands.Lendings
{
    public class DeleteLendingCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteLendingCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteLendingCommand request, CancellationToken cancellationToken)
        {
            var lending = await unitOfWork.Lendings.GetByIdAsync(request.Id);

            lending.Delete(DateTime.Now);
            await unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
