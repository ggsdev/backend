using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Controllers;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.DTOS.WellDTOS;
using PRIO.Models.Clusters;
using PRIO.Models.Fields;
using PRIO.Models.Installations;
using PRIO.Models.Users;
using PRIO.Models.Wells;
using PRIO.ViewModels.Wells;

namespace PRIO.TESTS.Cruds.Wells
{
    [TestFixture]
    internal class WellControllerTests
    {
        private WellController _controller;
        private IMapper _mapper;
        private DataContext _context;
        private CreateWellViewModel _createViewModel;
        private UpdateWellViewModel _updateViewModel;
        private User _user;
        private Field _field1;
        private Field _field2;
        private Guid _invalidId = Guid.NewGuid();

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
                cfg.CreateMap<Well, WellDTO>();
                cfg.CreateMap<WellHistory, WellHistoryDTO>();
            });
            _mapper = mapperConfig.CreateMapper();
            _user = new User()
            {
                Name = "userTeste",
                Email = "userTeste@mail.com",
                Password = "1234",
                Username = "userTeste",
                Type = "admin"
            };
            _context.Users.Add(_user);

            var cluster = new Cluster
            {
                Name = "testeClus",
                UepCode = "213123",
                User = _user,
            };
            _context.Clusters.Add(cluster);

            var installation = new Installation()
            {
                Name = "testeInst",
                CodInstallation = "codmocked",
                User = _user,
                Cluster = cluster,
            };
            _context.Installations.Add(installation);

            _field1 = new Field()
            {
                Installation = installation,
                Name = "name",
                CodField = "cod",
                User = _user
            };

            _field2 = new Field()
            {
                Installation = installation,
                Name = "name",
                CodField = "casdod",
                User = _user
            };
            _context.Fields.Add(_field1);
            _context.Fields.Add(_field2);
            _context.SaveChanges();

            var httpContext = new DefaultHttpContext();
            httpContext.Items["Id"] = _user.Id;

            _controller = new WellController(_context, _mapper);
            _controller.ControllerContext.HttpContext = httpContext;
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Create_WellReturnsACreatedStatusWithDTO()
        {
            _createViewModel = new()
            {
                CodWellAnp = "21321",
                Name = "Name",
                FieldId = _field1.Id,
            };

            var response = await _controller.Create(_createViewModel);
            var createdResult = (CreatedResult)response;

            Assert.IsInstanceOf<CreatedResult>(response);
            Assert.That(((WellDTO)createdResult.Value).Name, Is.EqualTo(_createViewModel.Name));
            Assert.That(((WellDTO)createdResult.Value).CodWellAnp, Is.EqualTo(_createViewModel.CodWellAnp));
            Assert.That(createdResult.Location, Is.EqualTo($"wells/{((WellDTO)createdResult.Value).Id}"));
        }

        [Test]
        public async Task Create_WellShouldReturnConflictIfAlreadyExists()
        {
            _createViewModel = new()
            {
                CodWellAnp = "21321",
                Name = "Name",
                FieldId = _field1.Id,
            };

            await _controller.Create(_createViewModel);
            var response = await _controller.Create(_createViewModel);
            var conflictResult = (ConflictObjectResult)response;

            Assert.IsInstanceOf<ConflictObjectResult>(response);
            Assert.That(conflictResult.StatusCode, Is.EqualTo(409));
            Assert.That(((ErrorResponseDTO)conflictResult.Value!).Message, Is.EqualTo($"Well with code: {_createViewModel.CodWell} already exists, try another code."));
        }

        [Test]
        public async Task Create_WellShouldReturnNotFoundIfFieldDoesntExists()
        {
            _createViewModel = new()
            {
                CodWellAnp = "21321",
                Name = "Name",
                FieldId = _invalidId,
            };

            var response = await _controller.Create(_createViewModel);
            var notFoundResult = (NotFoundObjectResult)response;

            Assert.IsInstanceOf<NotFoundObjectResult>(response);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(((ErrorResponseDTO)notFoundResult.Value!).Message, Is.EqualTo("Field is not found"));
        }
    }
}