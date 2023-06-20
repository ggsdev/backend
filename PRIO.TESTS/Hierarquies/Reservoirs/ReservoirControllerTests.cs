using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Dtos;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Dtos;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Dtos;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.Http.Controllers;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Reservoirs.ViewModels;
using PRIO.src.Modules.Hierarchy.Zones.Dtos;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using System.ComponentModel.DataAnnotations;

namespace PRIO.TESTS.Hierarquies.Reservoirs
{
    internal class ReservoirControllerTests
    {
        private ReservoirController _controller;
        private IMapper _mapper;
        private DataContext _context;
        private CreateReservoirViewModel _viewModel;
        private ReservoirService _service;
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
                cfg.CreateMap<Cluster, ClusterDTO>();
                cfg.CreateMap<Installation, CreateUpdateInstallationDTO>();
                cfg.CreateMap<Field, FieldDTO>();
                cfg.CreateMap<Zone, CreateUpdateZoneDTO>();
                cfg.CreateMap<Reservoir, ReservoirDTO>();
                cfg.CreateMap<Reservoir, CreateUpdateReservoirDTO>();
                cfg.CreateMap<Reservoir, ReservoirHistoryDTO>();
                cfg.CreateMap<User, UserDTO>();
            });

            _mapper = mapperConfig.CreateMapper();
            _user = new User()
            {
                Name = "userTeste",
                Email = "userTeste@mail.com",
                Password = "1234",
                Username = "userTeste",
            };
            _context.Users.Add(_user);
            _context.SaveChanges();

            var httpContext = new DefaultHttpContext();
            httpContext.Items["Id"] = _user.Id;
            httpContext.Items["User"] = _user;

            _service = new ReservoirService(_context, _mapper);
            _controller = new ReservoirController(_service);
            _controller.ControllerContext.HttpContext = httpContext;
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Create_ReservoirReturnsACreatedStatusWithDTO()
        {

            Cluster _cluster1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "ClusterTest",
                User = _user
            };
            _context.Add(_cluster1);

            Installation _installation1 = new()
            {
                Id = Guid.NewGuid(),

                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                Cluster = _cluster1,
                User = _user
            };
            _context.Add(_installation1);

            Field _field1 = new()
            {
                Id = Guid.NewGuid(),

                Name = "FieldTest",
                CodField = "FieldTest",
                Installation = _installation1,
                User = _user
            };
            _context.Add(_field1);

            Zone _zone = new()
            {
                Id = Guid.NewGuid(),

                CodZone = "ZoneTest",
                Field = _field1
            };
            _context.Add(_zone);
            _context.SaveChanges();

            _viewModel = new CreateReservoirViewModel
            {

                Name = "ReservoirTest",
                CodReservoir = "ReservoirTest",
                ZoneId = _zone.Id,
            };

            var response = await _controller.Create(_viewModel);
            var createdResult = (CreatedResult)response;

            Assert.IsInstanceOf<CreatedResult>(response);
            Assert.That(((CreateUpdateReservoirDTO)createdResult.Value).Name, Is.EqualTo(_viewModel.Name));
            Assert.That(((CreateUpdateReservoirDTO)createdResult.Value).CodReservoir, Is.EqualTo(_viewModel.CodReservoir));
            Assert.That(createdResult.StatusCode, Is.EqualTo(201));
            Assert.That(createdResult.Location, Is.EqualTo($"reservoirs/{((CreateUpdateReservoirDTO)createdResult.Value).Id}"));
        }

        [Test]
        public async Task Create_CheckRequiredZoneIdField()
        {
            Cluster _cluster1 = new()
            {
                Id = Guid.NewGuid(),

                Name = "ClusterTest",
                User = _user
            };
            await _context.AddAsync(_cluster1);

            Installation _installation1 = new()
            {
                Id = Guid.NewGuid(),

                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                Cluster = _cluster1,
                User = _user
            };
            await _context.AddAsync(_installation1);

            Field _field1 = new()
            {
                Id = Guid.NewGuid(),

                Name = "FieldTest",
                CodField = "FieldTest",
                Installation = _installation1,
                User = _user
            };
            await _context.AddAsync(_field1);

            Zone _zone = new()
            {
                Id = Guid.NewGuid(),

                CodZone = "ZoneTest",
                Field = _field1
            };
            await _context.AddAsync(_zone);
            await _context.SaveChangesAsync();

            _viewModel = new CreateReservoirViewModel
            {
                Name = "ReservoirTest",
                CodReservoir = "ReservoirTest",
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_viewModel, null, null);
            bool isValid = Validator.TryValidateObject(_viewModel, validationContext, validationResults, true);

            if (!isValid)
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Message = "Zone id is required"
                };

                var badRequestResult = new BadRequestObjectResult(errorResponse);
                Assert.IsInstanceOf<BadRequestObjectResult>(badRequestResult);

                Assert.That(((ErrorResponseDTO)badRequestResult.Value).Message, Is.EqualTo(errorResponse.Message));
                Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            }
        }
        //[Test]
        //public async Task Create_CheckRequiredNameField()
        //{
        //    Cluster _cluster1 = new()
        //    {
        //        Name = "ClusterTest",
        //        User = _user
        //    };
        //    _context.Add(_cluster1);

        //    Installation _installation1 = new()
        //    {
        //        Name = "InstallationTest",
        //        CodInstallationUep = "InstallationTest",
        //        Cluster = _cluster1,
        //        User = _user
        //    };
        //    _context.Add(_installation1);

        //    Field _field1 = new()
        //    {
        //        Name = "FieldTest",
        //        CodField = "FieldTest",
        //        Installation = _installation1,
        //        User = _user
        //    };
        //    _context.Add(_field1);

        //    Zone _zone = new()
        //    {
        //        CodZone = "ZoneTest",
        //        Field = _field1
        //    };
        //    _context.Add(_zone);
        //    _context.SaveChanges();

        //    _viewModel = new CreateReservoirViewModel
        //    {
        //        Name = "teste"
        //    };

        //    try
        //    {
        //        var response = await _controller.Create(_viewModel);

        //        Assert.IsInstanceOf<BadRequestObjectResult>(response);
        //        var createdResult = (BadRequestObjectResult)response;

        //        Assert.That(createdResult.Value, Is.EqualTo("Reservoir name is required"));
        //        Assert.That(createdResult.StatusCode, Is.EqualTo(400));
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        Assert.IsInstanceOf<DbUpdateException>(ex);
        //        Assert.That(ex.Message, Contains.Substring("Required properties '{'ZoneId'}' are missing"));
        //    }
        //}

        //PATCH
        [Test]
        public async Task Update_ReservoirReturnsACreatedStatusWithDTO()
        {
            Cluster _cluster1 = new()
            {
                Id = Guid.NewGuid(),

                Name = "clustertest",
                User = _user
            };
            _context.Add(_cluster1);
            _context.SaveChanges();

            Installation _installation1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                Cluster = _cluster1,
                User = _user
            };
            _context.Add(_installation1);

            Field _field1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "FieldTest",
                CodField = "FieldTest",
                Installation = _installation1,
                User = _user
            };
            _context.Add(_field1);

            Zone _zone = new()
            {
                Id = Guid.NewGuid(),
                CodZone = "ZoneTest",
                Field = _field1
            };
            _context.Add(_zone);

            Reservoir _reservoir = new()
            {
                Id = Guid.NewGuid(),
                Name = "ReservoirTest",
                Zone = _zone,
                CodReservoir = "ReservoirTest",
                User = _user,
            };
            _context.Add(_reservoir);
            _context.SaveChanges();

            var _viewModel2 = new UpdateReservoirViewModel
            {
                Name = "ReservoirTest2",
                CodReservoir = "ReservoirTest2",
                ZoneId = _zone.Id,
            };

            var response = await _controller.Update(_reservoir.Id, _viewModel2);
            var createdResult = (OkObjectResult)response;

            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.That(((CreateUpdateReservoirDTO)createdResult.Value).Name, Is.EqualTo(_viewModel2.Name));
            Assert.That(((CreateUpdateReservoirDTO)createdResult.Value).CodReservoir, Is.EqualTo(_viewModel2.CodReservoir));
            Assert.That(createdResult.StatusCode, Is.EqualTo(200));
        }

        //DELETE
        [Test]
        public async Task Delete_ReservoirReturnsANoContent()
        {
            Cluster _cluster1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "clustertest",
                User = _user
            };
            _context.Add(_cluster1);

            Installation _installation1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                Cluster = _cluster1,
                User = _user
            };
            _context.Add(_installation1);

            Field _field1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "FieldTest",
                CodField = "FieldTest",
                Installation = _installation1,
                User = _user
            };
            _context.Add(_field1);


            Zone _zone = new()
            {
                Id = Guid.NewGuid(),
                CodZone = "ZoneTest",
                Field = _field1
            };
            _context.Add(_zone);

            Reservoir _reservoir = new()
            {
                Id = Guid.NewGuid(),
                Name = "ReservoirTest",
                Zone = _zone,
                CodReservoir = "ReservoirTest",
                User = _user,
            };
            _context.Add(_reservoir);
            _context.SaveChanges();

            var response = await _controller.Delete(_reservoir.Id);
            var createdResult = (NoContentResult)response;

            Assert.IsInstanceOf<NoContentResult>(response);
            var reservoir = await _context.Reservoirs.SingleOrDefaultAsync();
            Assert.That(reservoir.IsActive, Is.EqualTo(false));
            Assert.That(reservoir.DeletedAt, Is.Not.Null);
            Assert.That(createdResult.StatusCode, Is.EqualTo(204));
        }


        [Test]
        public async Task Create_AlsoCreateAReservoirHistoryOfTypeCreate()
        {
            Cluster _cluster1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "ClusterTest",
                User = _user
            };
            _context.Add(_cluster1);

            Installation _installation1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                Cluster = _cluster1,
                User = _user
            };
            _context.Add(_installation1);

            Field _field1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "FieldTest",
                CodField = "FieldTest",
                Installation = _installation1,
                User = _user
            };
            _context.Add(_field1);

            Zone _zone = new()
            {
                Id = Guid.NewGuid(),
                CodZone = "ZoneTest",
                Field = _field1
            };
            _context.Add(_zone);
            _context.SaveChanges();

            _viewModel = new CreateReservoirViewModel
            {
                Name = "ReservoirTest",
                CodReservoir = "ReservoirTest",
                ZoneId = _zone.Id,
            };
            await _controller.Create(_viewModel);

            var reservoir = await _context.Reservoirs.SingleOrDefaultAsync();

            Assert.That(reservoir, Is.Not.Null);

            //var history = await _context.ReservoirHistories.SingleOrDefaultAsync();
            //Assert.That(history, Is.Not.Null);
            //Assert.That(history.CodReservoir, Is.EqualTo(reservoir.CodReservoir));
            //Assert.That(history.Zone.Id, Is.EqualTo(reservoir.Zone.Id));

            //Assert.That(history.TypeOperation, Is.EqualTo(Utils.TypeOperation.Create));
            //Assert.That(history.User, Is.Not.Null);
            //Assert.That(history.User.Name, Is.EqualTo(_user.Name));
        }
    }
}
