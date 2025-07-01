using AmazeCare.DbContexts;
using AmazeCare.Models;
using AmazeCare.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceTests
{
    [TestFixture]
    public class AuthServiceTests
    {
        private IAuthService _authService;
        private IConfiguration _config;
        private ApplicationDbContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("AmazeCareTestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Users.Add(new User
            {
                Username = "testuser",
                Password = "password",
                Role = "Patient"
            });
            _context.SaveChanges();

            var inMemorySettings = new Dictionary<string, string> {
            {"Jwt:Key", "ThisIsASecretKeyForJwt"},
            {"Jwt:Issuer", "AmazeCareAPI"},
            {"Jwt:Audience", "AmazeCareUsers"}
        };

            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _authService = new AuthService(_context, _config);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task AuthenticateUserAsync_ValidCredentials_ReturnsUser()
        {
            var user = await _authService.AuthenticateUserAsync("testuser", "password");
            Assert.IsNotNull(user);
            Assert.AreEqual("testuser", user.Username);
        }

        [Test]
        public async Task AuthenticateUserAsync_InvalidCredentials_ReturnsNull()
        {
            var user = await _authService.AuthenticateUserAsync("wronguser", "wrongpass");
            Assert.IsNull(user);
        }

        [Test]
        public void GenerateToken_ValidUser_ReturnsToken()
        {
            var user = new User { Username = "testuser", Role = "Patient" };
            var token = _authService.GenerateToken(user);
            Assert.IsNotNull(token);
            Assert.IsInstanceOf<string>(token);
        }
    }
}
