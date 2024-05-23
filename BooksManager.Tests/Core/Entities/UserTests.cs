using BooksManager.Core.Entities;
using BooksManager.Core.Enums;

namespace BooksManager.Tests.Core.Entities
{
    public class UserTests
    {
        [Fact]
        public void User_EmptyConstructor_ShouldInitializeWithDefaultValues()
        {
            // Act
            var user = new User();

            // Assert
            Assert.NotNull(user);
            Assert.Equal(string.Empty, user.Name);
            Assert.Equal(string.Empty, user.Email);
            Assert.Equal(string.Empty, user.Password);
            Assert.Equal(default(Roles), user.Role);
            Assert.NotNull(user.Lendings);
        }
    }
}
