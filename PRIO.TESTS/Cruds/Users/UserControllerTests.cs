using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Controllers;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.Models.UserControlAccessModels;

namespace PRIO.TESTS.Users
{
    [TestFixture]
    internal class UserControllerTests
    {
        private UserController _controller;
        private IMapper _mapper;
        private DataContext _context;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(contextOptions);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
            });

            _mapper = mapperConfig.CreateMapper();

            _controller = new UserController(_context, _mapper);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetUser_ExistingId_ReturnsOkResult()
        {
            var initialUserCount = _context.Users.Count();
            var existingUserId = Guid.NewGuid();
            var existingUser = new User { Id = existingUserId, Name = "John Doe", Email = "sada@mail.com", Password = "1234", Username = "asdsad" };
            await _context.Users.AddAsync(existingUser);
            await _context.SaveChangesAsync();

            var httpContext = new DefaultHttpContext();
            httpContext.Items["Id"] = existingUserId.ToString();
            _controller.ControllerContext.HttpContext = httpContext;

            var result = await _controller.GetById(existingUserId);
            var finalUserCount = _context.Users.Count();
            var okResult = (OkObjectResult)result;
            var userDto = (UserDTO)okResult.Value;

            Assert.Multiple(() =>
            {
                Assert.That(finalUserCount, Is.EqualTo(initialUserCount + 1));
                Assert.That(okResult.Value, Is.InstanceOf<UserDTO>());
                Assert.That(userDto.Id, Is.EqualTo(existingUser.Id));
                Assert.That(userDto.Name, Is.EqualTo(existingUser.Name));
            });
        }

        [Test]
        public async Task GetUser_NonExistingId_ReturnsNotFoundResult()
        {
            var nonExistingUserId = Guid.NewGuid();
            var httpContext = new DefaultHttpContext();
            httpContext.Items["Id"] = nonExistingUserId.ToString();

            _controller.ControllerContext.HttpContext = httpContext;
            var result = await _controller.GetById(nonExistingUserId);
            var notFoundResult = (NotFoundObjectResult)result;
            var errorResponse = (ErrorResponseDTO)notFoundResult.Value;

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.That(errorResponse?.Message, Is.EqualTo("User Not found."));

        }
    }
}