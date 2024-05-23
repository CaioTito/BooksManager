using Bogus;
using BooksManager.Application.Commands.Lendings;
using BooksManager.Application.Queries.Books;
using BooksManager.Core.Entities;

namespace BooksManager.Tests.Mocks
{
    public static class LendingMock
    {
        public static Faker<Lending> LendingFaker => new Faker<Lending>()
            .CustomInstantiator(f => (
                new Lending(
                    f.Random.Guid(),
                    f.Random.Guid(),
                    f.Date.Future())
            ));

        public static Faker<CreateLendingCommand> CreateLendingCommandFaker =>
            new Faker<CreateLendingCommand>()
                .RuleFor(x => x.BookId, f => f.Random.Guid())
                .RuleFor(x => x.ReturnDate, f => f.Date.Future());
    }
}
