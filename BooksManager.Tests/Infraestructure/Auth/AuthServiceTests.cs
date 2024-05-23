using BooksManager.Core.Enums;
using BooksManager.Infraestructure.Auth;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BooksManager.Tests.Infraestructure.Auth
{
    public class AuthServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();

            _configurationMock.SetupGet(c => c["Jwt:Issuer"]).Returns("testIssuer");
            _configurationMock.SetupGet(c => c["Jwt:Audience"]).Returns("testAudience");
            _configurationMock.SetupGet(c => c["Jwt:Key"]).Returns("testKey123456789012345678901234567890");

            _authService = new AuthService(_configurationMock.Object);
        }

        [Fact]
        public void GenerateJwtToken_ShouldReturnToken()
        {
            // Arrange
            var email = "test@example.com";
            var role = 1;
            var id = Guid.NewGuid();

            // Act
            var token = _authService.GenerateJwtToken(email, role, id);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            Assert.Equal("testIssuer", jwtToken.Issuer);
            Assert.Equal("testAudience", jwtToken.Audiences.First());
            Assert.Equal(email, jwtToken.Claims.First(c => c.Type == ClaimTypes.Email).Value);

            var expectedRoleName = Enum.GetName(typeof(Roles), role);
            Assert.Equal(expectedRoleName, jwtToken.Claims.First(c => c.Type == ClaimTypes.Role).Value);

            Assert.Equal(id.ToString(), jwtToken.Claims.First(c => c.Type == "UserId").Value);
        }

        [Fact]
        public void GeneratePasswordHash_ShouldReturnHashedPassword()
        {
            // Arrange
            var password = "TestPassword123";

            // Act
            var hashedPassword = _authService.GeneratePasswordHash(password);

            // Assert
            using SHA256 sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            var expectedHash = builder.ToString();

            Assert.Equal(expectedHash, hashedPassword);
        }
    }
}
