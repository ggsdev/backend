using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.Models;
using PRIO.Services;
using PRIO.ViewModels;

namespace PRIO.TESTS
{
    [TestFixture]
    public class UserServicesTests
    {
        private DataContext _context;
        private UserServices _userServices;

        [SetUp]
        public void SetUp()
        {
            // Set up an in-memory database for testing
            var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "TestDb")
                    .Options;

            _context = new DataContext(options);

            // Seed some test data
            var users = new List<User>
    {
        new User
        {
            Id = new Guid("11111111-1111-1111-1111-111111111111"),
            Name = "John Doe",
            Email = "johndoe@example.com",
            Password = "password",
            Username = "johndoe"
        },
        new User
        {
            Id = new Guid("22222222-2222-2222-2222-222222222222"),
            Name = "Jane Smith",
            Email = "janesmith@example.com",
            Password = "password",
            Username = "janesmith"
        }
    };
            _context.Users.AddRange(users);
            _context.SaveChanges();

            _userServices = new UserServices(_context);
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose the in-memory database after each test
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        #region Get user by ID
        [Test]
        public async Task GetUserByIdAsync_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userId = new Guid("11111111-1111-1111-1111-111111111111");

            // Act
            var result = await _userServices.GetUserByIdAsync(userId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<User>());
            Assert.That(result.Id, Is.EqualTo(userId));
        }

        [Test]
        public async Task GetUserByIdAsync_ReturnsNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = new Guid("33333333-3333-3333-3333-333333333333");

            // Act
            var result = await _userServices.GetUserByIdAsync(userId);

            // Assert
            Assert.That(result, Is.Null);
        }

        #endregion

        [Test]
        public async Task CreateUserAsync_ReturnsUserDTO_WithValidInput()
        {
            // Arrange
            var body = new CreateUserViewModel
            {
                Name = "John Doe",
                Email = "johndoe@example.com",
                Username = "johndoe",
                Password = "password"
            };

            // Act
            var result = await _userServices.CreateUserAsync(body);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(body.Name));
            Assert.That(result.Email, Is.EqualTo(body.Email));
            Assert.That(result.Username, Is.EqualTo(body.Username));
            Assert.That(result.IsActive.HasValue, Is.True);
            Assert.That(result.CreatedAt.Date, Is.EqualTo(DateTime.UtcNow.ToLocalTime().Date));
            Assert.That(result.UpdatedAt, Is.Null);
        }
    }
}