using Bogus;
using BooksManager.Application.Commands.Users;
using BooksManager.Application.Queries.Books;
using BooksManager.Application.Queries.Users;
using BooksManager.Application.ViewModels;
using BooksManager.Core.Entities;
using BooksManager.Core.Enums;

namespace BooksManager.Tests.Mocks
{
    public static class UserMock
    {
        public static Faker<User> UserFaker => new Faker<User>()
            .CustomInstantiator(f => (
                new User(
                    f.Person.FirstName,
                    f.Person.Email,
                    f.Random.Word(),
                    f.PickRandom<Roles>())
            ));

        public static Faker<CreateUserCommand> CreateUserCommandFaker =>
            new Faker<CreateUserCommand>()
                .RuleFor(x => x.Email, f => f.Person.Email)
                .RuleFor(x => x.Name, f => f.Person.FirstName)
                .RuleFor(x => x.Password, f => f.Random.Word())
                .RuleFor(x => x.Role, f => f.PickRandom<Roles>());

        public static Faker<LoginUserCommand> LoginUserCommandFaker =>
            new Faker<LoginUserCommand>()
                .RuleFor(x => x.Email, f => f.Person.Email)
                .RuleFor(x => x.Password, f => f.Random.Word());

        public static Faker<GetUserByIdQuery> GetUserByIdQueryFaker =>
            new Faker<GetUserByIdQuery>()
                .CustomInstantiator(f => (
                    new GetUserByIdQuery(f.Random.Guid())));

        public static Faker<DeleteUserCommand> DeleteUserCommandFaker =>
            new Faker<DeleteUserCommand>()
                .CustomInstantiator(f => (
                    new DeleteUserCommand(f.Random.Guid())));

        public static Faker<UserViewModel> UserViewModelFaker =>
            new Faker<UserViewModel>()
                .CustomInstantiator(f => (
                    new UserViewModel(
                        f.Random.Guid(),
                        f.Person.FirstName,
                        f.Person.Email,
                        f.PickRandom<Roles>())));
    }
}
