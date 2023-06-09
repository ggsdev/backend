using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Controllers;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.CompletionDTOS;
using PRIO.DTOS.ReservoirDTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.DTOS.WellDTOS;
using PRIO.Models.Clusters;
using PRIO.Models.Completions;
using PRIO.Models.Fields;
using PRIO.Models.Installations;
using PRIO.Models.Reservoirs;
using PRIO.Models.Users;
using PRIO.Models.Wells;
using PRIO.Models.Zones;
using PRIO.ViewModels.Completions;

namespace PRIO.TESTS.Cruds.Completions
{
    internal class CompletionControllerTests
    {
        private CompletionController _controller;
        private IMapper _mapper;
        private DataContext _context;
        private CreateCompletionViewModel _viewModel;
        private User _user;

        private Reservoir _reservoir1;
        private Reservoir _reservoir2;
        private Well _well1;
        private Well _well2;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(contextOptions);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Completion, CompletionDTO>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Reservoir, ReservoirDTO>();
                cfg.CreateMap<Well, WellDTO>();
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
            _context.SaveChanges();

            var (reservoir1, well1, reservoir2, well2) = SetupMockData();
            _reservoir1 = reservoir1;
            _reservoir2 = reservoir2;
            _well1 = well1;
            _well2 = well2;

            var httpContext = new DefaultHttpContext();
            httpContext.Items["Id"] = _user.Id;

            _controller = new CompletionController(_context, _mapper);
            _controller.ControllerContext.HttpContext = httpContext;
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Create_CompletionReturnsACreatedStatusWithDTO()
        {
            _viewModel = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = _well1.Id,
                CodCompletion = "Cod Teste COmpletion",
            };

            var response = await _controller.Create(_viewModel);
            var createdResult = (CreatedResult)response;

            Assert.IsInstanceOf<CreatedResult>(response);
            Assert.That(((CompletionDTO)createdResult.Value).Name, Is.EqualTo($"{_well1.Name}_{_reservoir1.Zone?.CodZone}"));
            Assert.That(createdResult.Location, Is.EqualTo($"completions/{((CompletionDTO)createdResult.Value).Id}"));
        }

        [Test]
        public async Task Create_CheckIfReservoirAndWellFieldsMatch()
        {
            _viewModel = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = _well2.Id,
                CodCompletion = "Different Fields",
            };

            var response = await _controller.Create(_viewModel);
            var conflictResult = (ConflictObjectResult)response;

            Assert.IsInstanceOf<ConflictObjectResult>(response);
            Assert.That(conflictResult.StatusCode, Is.EqualTo(409));
            Assert.That(((ErrorResponseDTO)conflictResult.Value).Message, Is.EqualTo($"Reservoir: {_reservoir1.Name} and Well: {_well2.Name} doesn't belong to the same Field"));
        }

        [Test]
        public async Task Create_ShouldNotCreateWhenWellIsInvalid()
        {
            var wellInvalidId = Mock._invalidId;
            _viewModel = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = wellInvalidId,
                CodCompletion = "Invalid well",
            };

            var response = await _controller.Create(_viewModel);
            var notFoundResult = (NotFoundObjectResult)response;

            Assert.IsInstanceOf<NotFoundObjectResult>(response);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(((ErrorResponseDTO)notFoundResult.Value).Message, Is.EqualTo($"Well with id: {wellInvalidId} not found"));
        }

        [Test]
        public async Task Create_ShouldNotCreateWhenReservoirIsInvalid()
        {
            var reservoirInvalidId = Mock._invalidId;
            _viewModel = new CreateCompletionViewModel
            {
                ReservoirId = Mock._invalidId,
                WellId = _well1.Id,
                CodCompletion = "Invalid reservoir",
            };

            var response = await _controller.Create(_viewModel);
            var notFoundResult = (NotFoundObjectResult)response;

            Assert.IsInstanceOf<NotFoundObjectResult>(response);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(((ErrorResponseDTO)notFoundResult.Value).Message, Is.EqualTo($"Reservoir with id: {Mock._invalidId} not found"));
        }

        [Test]
        public async Task Create_ShouldNotCreateWhenCompletionNameAlreadyExists()
        {
            _viewModel = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = _well1.Id,
                CodCompletion = "Invalid reservoir",
            };

            await _controller.Create(_viewModel);

            var viewModelDuplicated = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = _well1.Id,
                CodCompletion = "Already exists",
            };

            var response = await _controller.Create(viewModelDuplicated);
            var conflictResult = (ConflictObjectResult)response;

            Assert.IsInstanceOf<ConflictObjectResult>(response);
            Assert.That(conflictResult.StatusCode, Is.EqualTo(409));
            Assert.That(((ErrorResponseDTO)conflictResult.Value).Message, Is.EqualTo($"Completion with name: {_well1.Name}_{_reservoir1.Zone?.CodZone} already exists."));
        }

        [Test]
        public async Task Create_AlsoCreateAHistoryOfTypeCreate()
        {
            _viewModel = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = _well1.Id,
                CodCompletion = "mockCod",
            };

            await _controller.Create(_viewModel);
            var completion = await _context.Completions.SingleOrDefaultAsync();
            var history = await _context.CompletionHistories.SingleOrDefaultAsync();

            Assert.That(history, Is.Not.Null);
            Assert.That(completion, Is.Not.Null);
            Assert.That(history.CodCompletion, Is.EqualTo(completion.CodCompletion));
            Assert.That(history.TypeOperation, Is.EqualTo(Utils.TypeOperation.Create));
            Assert.That(history.User, Is.Not.Null);
            Assert.That(history.User.Name, Is.EqualTo(_user.Name));
            Assert.That(history.Name, Is.EqualTo(completion.Name));
        }

        private (Reservoir reservoir1, Well well1, Reservoir reservoir2, Well well2) SetupMockData()
        {
            Cluster _cluster1 = new()
            {
                UepCode = "12332",
                Name = "ClusterTest",
                User = _user
            };

            Installation _installation1 = new()
            {
                Name = "InstallationTest",
                User = _user,
                Cluster = _cluster1
            };

            Field _field1 = new()
            {
                Name = "FieldTest",
                Installation = _installation1,
                User = _user,
                CodField = "32132"
            };

            Zone _zone1 = new()
            {
                Field = _field1,
                User = _user,
                CodZone = "933213",
            };

            Reservoir _reservoir1 = new()
            {
                Zone = _zone1,
                User = _user,
                Name = "ReservoirTest"
            };

            Well _well1 = new()
            {
                Field = _field1,
                User = _user,
                Name = "ReservoirTest",
                CodWellAnp = "123213111"
            };

            Cluster _cluster2 = new()
            {
                UepCode = "22332",
                Name = "ClusterTest2",
                User = _user
            };

            Installation _installation2 = new()
            {
                Name = "InstallationTest2",
                User = _user,
                Cluster = _cluster2
            };

            Field _field2 = new()
            {
                Name = "FieldTest2",
                Installation = _installation2,
                User = _user,
                CodField = "32232"
            };

            Zone _zone2 = new()
            {
                Field = _field2,
                User = _user,
                CodZone = "933223",
            };

            Reservoir _reservoir2 = new()
            {
                Zone = _zone2,
                User = _user,
                Name = "ReservoirTest2"
            };

            Well _well2 = new()
            {
                Field = _field2,
                User = _user,
                Name = "ReservoirTest2",
                CodWellAnp = "223213111"
            };

            _context.Add(_cluster1);
            _context.Add(_cluster2);

            _context.Add(_installation1);
            _context.Add(_installation2);

            _context.Add(_field1);
            _context.Add(_field2);

            _context.Add(_zone1);
            _context.Add(_zone2);

            _context.Add(_reservoir1);
            _context.Add(_reservoir2);

            _context.Add(_well1);
            _context.Add(_well2);

            _context.SaveChanges();

            return (_reservoir1, _well1, _reservoir2, _well2);
        }
    }
}
