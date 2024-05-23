using Bogus;
using BooksManager.Application.Commands.Books;
using BooksManager.Application.Queries.Books;
using BooksManager.Core.Entities;

namespace BooksManager.Tests.Mocks
{
    public static class BookMock
    {
        public static Faker<Book> BookFaker => new Faker<Book>()
            .CustomInstantiator(f => (
                new Book(
                    f.Random.Word(),
                    f.Random.Word(),
                    f.Random.Word(),
                    f.Random.Int())
            ));

        public static Faker<CreateBookCommand> CreateBookCommandFaker =>
            new Faker<CreateBookCommand>()
                .RuleFor(x => x.Author, f => f.Random.Word())
                .RuleFor(x => x.Isbn, f => f.Random.Word())
                .RuleFor(x => x.Title, f => f.Random.Word())
                .RuleFor(x => x.YearOfPublication, f => f.Random.Int());

        public static Faker<GetBookByIdQuery> GetBookByIdQueryFaker =>
            new Faker<GetBookByIdQuery>()
                .RuleFor(x => x.Id, f => f.Random.Guid());
    }
}
