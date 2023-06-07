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

        private async Task<(Reservoir reservoir1, Well well1, Reservoir reservoir2, Well well2)> SetupMockData()
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

            await _context.AddAsync(_cluster1);
            await _context.AddAsync(_cluster2);

            await _context.AddAsync(_installation1);
            await _context.AddAsync(_installation2);

            await _context.AddAsync(_field1);
            await _context.AddAsync(_field2);

            await _context.AddAsync(_zone1);
            await _context.AddAsync(_zone2);

            await _context.AddAsync(_reservoir1);
            await _context.AddAsync(_reservoir2);

            await _context.AddAsync(_well1);
            await _context.AddAsync(_well2);

            await _context.SaveChangesAsync();

            return (_reservoir1, _well1, _reservoir2, _well2);
        }

        [Test]
        public async Task Create_CompletionReturnsACreatedStatusWithDTO()
        {
            var mockedData = await SetupMockData();

            _viewModel = new CreateCompletionViewModel
            {
                ReservoirId = mockedData.reservoir1.Id,
                WellId = mockedData.well1.Id,
                CodCompletion = "Cod Teste COmpletion",
            };

            var response = await _controller.Create(_viewModel);
            var createdResult = (CreatedResult)response;

            Assert.IsInstanceOf<CreatedResult>(response);
            Assert.That(((CompletionDTO)createdResult.Value).Name, Is.EqualTo($"{mockedData.well1.Name}_{mockedData.reservoir1.Zone?.CodZone}"));
            Assert.That(createdResult.StatusCode, Is.EqualTo(201));
            Assert.That(createdResult.Location, Is.EqualTo($"completions/{((CompletionDTO)createdResult.Value).Id}"));
        }

        [Test]
        public async Task Create_CheckIfReservoirAndWellFieldsMatch()
        {
            var mockedData = await SetupMockData();

            _viewModel = new CreateCompletionViewModel
            {
                ReservoirId = mockedData.reservoir1.Id,
                WellId = mockedData.well2.Id,
                CodCompletion = "Different Fields",
            };

            var response = await _controller.Create(_viewModel);
            var createdResult = (ConflictObjectResult)response;

            Assert.IsInstanceOf<ConflictObjectResult>(response);
            Assert.That(createdResult.StatusCode, Is.EqualTo(409));
            Assert.That(((ErrorResponseDTO)createdResult.Value).Message, Is.EqualTo($"Reservoir: {mockedData.reservoir1.Name} and Well: {mockedData.well2.Name} doesn't belong to the same Field"));
        }

        //    [Test]
        //    public async Task Create_CheckIfReservoirAndWellFieldsMatch()
        //    {
        //        await _context.Clusters.AddAsync(Mock._cluster1);
        //        await _context.Installations.AddAsync(Mock._installation1);
        //        await _context.Fields.AddAsync(Mock._field1);
        //        await _context.Zones.AddAsync(Mock._zone1);
        //        await _context.Reservoirs.AddAsync(Mock._reservoir1);
        //        await _context.Wells.AddAsync(Mock._well1);

        //        await _context.Clusters.AddAsync(Mock._cluster2);
        //        await _context.Installations.AddAsync(Mock._installation2);
        //        await _context.Fields.AddAsync(Mock._field2);
        //        await _context.Zones.AddAsync(Mock._zone2);
        //        await _context.Reservoirs.AddAsync(Mock._reservoir2);
        //        await _context.Wells.AddAsync(Mock._well2);

        //        await _context.SaveChangesAsync();

        //        _viewModel = new CreateCompletionViewModel
        //        {
        //            ReservoirId = Mock._reservoir1.Id,
        //            WellId = Mock._well2.Id,
        //            CodCompletion = "Different Fields",
        //        };

        //        var response = await _controller.Create(_viewModel);
        //        var createdResult = (ConflictObjectResult)response;

        //        Assert.IsInstanceOf<ConflictObjectResult>(response);
        //        Assert.That(createdResult.StatusCode, Is.EqualTo(409));
        //        Assert.That(((ErrorResponseDTO)createdResult.Value).Message, Is.EqualTo($"Reservoir: {Mock._reservoir1.Name} and Well: {Mock._well2.Name} doesn't belong to the same Field"));
        //    }
        //}
    }
}
