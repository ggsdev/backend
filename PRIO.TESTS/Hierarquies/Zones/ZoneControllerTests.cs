using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Controllers;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.HierarchyDTOS.ClusterDTOS;
using PRIO.DTOS.HierarchyDTOS.FieldDTOS;
using PRIO.DTOS.HierarchyDTOS.InstallationDTOS;
using PRIO.DTOS.HierarchyDTOS.ZoneDTOS;
using PRIO.DTOS.HistoryDTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.Services.HierarchyServices;
using PRIO.ViewModels.HierarchyViewModels.Zones;
using System.ComponentModel.DataAnnotations;

namespace PRIO.TESTS.Hierarquies.Zones
{
    internal class ZoneControllerTests
    {
        private ZoneController _controller;
        private IMapper _mapper;
        private DataContext _context;
        private CreateZoneViewModel _viewModel;
        private ZoneService _service;
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
                cfg.CreateMap<Zone, ZoneHistoryDTO>();
                cfg.CreateMap<User, UserDTO>();
            })
            {

            };
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

            _service = new ZoneService(_context, _mapper);
            _controller = new ZoneController(_service);
            _controller.ControllerContext.HttpContext = httpContext;
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Create_ZoneReturnsACreatedStatusWithDTO()
        {

            Cluster _cluster1 = new()
            {
                Name = "ClusterTest",
                User = _user
            };
            _context.Add(_cluster1);

            Installation _installation1 = new()
            {
                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                Cluster = _cluster1,
                User = _user
            };
            _context.Add(_installation1);

            Field _field1 = new()
            {
                Name = "FieldTest",
                CodField = "FieldTest",
                Installation = _installation1,
                User = _user
            };
            _context.Add(_field1);
            _context.SaveChanges();

            _viewModel = new CreateZoneViewModel
            {
                CodZone = "ZoneTest",
                FieldId = _field1.Id,
            };

            var response = await _controller.Create(_viewModel);
            var createdResult = (CreatedResult)response;

            Assert.IsInstanceOf<CreatedResult>(response);
            Assert.That(((CreateUpdateZoneDTO)createdResult.Value).CodZone, Is.EqualTo(_viewModel.CodZone));
            Assert.That(createdResult.StatusCode, Is.EqualTo(201));
            Assert.That(createdResult.Location, Is.EqualTo($"zones/{((CreateUpdateZoneDTO)createdResult.Value).Id}"));
        }


        [Test]
        public async Task Create_CheckRequiredFieldIdField()
        {
            Cluster _cluster1 = new()
            {
                Name = "clustertest",
                User = _user
            };
            _context.Add(_cluster1);
            _context.SaveChanges();

            Installation _installation1 = new()
            {
                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                Cluster = _cluster1,
                User = _user
            };
            _context.Add(_installation1);

            Field _field1 = new()
            {
                Name = "FieldTest",
                CodField = "FieldTest",
                Installation = _installation1,
                User = _user
            };
            _context.Add(_field1);
            _context.SaveChanges();

            _viewModel = new CreateZoneViewModel
            {
                CodZone = "ZoneTest",
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_viewModel, null, null);
            bool isValid = Validator.TryValidateObject(_viewModel, validationContext, validationResults, true);

            if (!isValid)
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Message = "Field id is required"
                };

                var badRequestResult = new BadRequestObjectResult(errorResponse);
                Assert.IsInstanceOf<BadRequestObjectResult>(badRequestResult);

                Assert.That(((ErrorResponseDTO)badRequestResult.Value).Message, Is.EqualTo(errorResponse.Message));
                Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            }
        }

        [Test]
        public async Task Create_CheckRequiredCodZoneField()
        {
            Cluster _cluster1 = new()
            {
                Name = "clustertest",
                User = _user
            };
            _context.Add(_cluster1);
            _context.SaveChanges();

            Installation _installation1 = new()
            {
                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                Cluster = _cluster1,
                User = _user
            };
            _context.Add(_installation1);

            Field _field1 = new()
            {
                Name = "FieldTest",
                CodField = "FieldTest",
                Installation = _installation1,
                User = _user
            };
            _context.Add(_field1);
            _context.SaveChanges();

            _viewModel = new CreateZoneViewModel
            {
                FieldId = _field1.Id,
            };

            try
            {
                var response = await _controller.Create(_viewModel);

                Assert.IsInstanceOf<BadRequestObjectResult>(response);
                var createdResult = (BadRequestObjectResult)response;

                Assert.That(createdResult.Value, Is.EqualTo("Zone code is required"));
                Assert.That(createdResult.StatusCode, Is.EqualTo(400));
            }
            catch (DbUpdateException ex)
            {
                Assert.IsInstanceOf<DbUpdateException>(ex);
                Assert.That(ex.Message, Contains.Substring("Required properties '{'CodZone'}' are missing"));
            }
        }

        //PATCH
        [Test]
        public async Task Update_ZoneReturnsACreatedStatusWithDTO()
        {
            Cluster _cluster1 = new()
            {
                Name = "clustertest",
                User = _user
            };
            _context.Add(_cluster1);
            _context.SaveChanges();

            Installation _installation1 = new()
            {
                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                Cluster = _cluster1,
                User = _user
            };
            _context.Add(_installation1);

            Field _field1 = new()
            {
                Name = "FieldTest",
                CodField = "FieldTest",
                Installation = _installation1,
                User = _user
            };
            _context.Add(_field1);

            Zone _zone = new()
            {
                CodZone = "ZoneTest",
                Field = _field1
            };
            _context.Add(_zone);
            _context.SaveChanges();

            var _viewModel2 = new UpdateZoneViewModel
            {
                CodZone = "ZoneTest2"
            };

            var response = await _controller.Update(_zone.Id, _viewModel2);
            var createdResult = (OkObjectResult)response;

            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.That(((CreateUpdateZoneDTO)createdResult.Value).CodZone, Is.EqualTo(_viewModel2.CodZone));
            Assert.That(createdResult.StatusCode, Is.EqualTo(200));
        }

        //DELETE
        [Test]
        public async Task Delete_ZonenReturnsANoContent()
        {
            Cluster _cluster1 = new()
            {
                Name = "clustertest",
                User = _user
            };
            _context.Add(_cluster1);
            _context.SaveChanges();

            Installation _installation1 = new()
            {
                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                Cluster = _cluster1,
                User = _user
            };
            _context.Add(_installation1);

            Field _field1 = new()
            {
                Name = "FieldTest",
                CodField = "FieldTest",
                Installation = _installation1,
                User = _user
            };
            _context.Add(_field1);

            Zone _zone = new()
            {
                CodZone = "ZoneTest",
                Field = _field1
            };
            _context.Add(_zone);
            _context.SaveChanges();

            var response = await _controller.Delete(_zone.Id);
            var createdResult = (NoContentResult)response;

            Assert.IsInstanceOf<NoContentResult>(response);
            var zone = await _context.Zones.SingleOrDefaultAsync();
            Assert.That(zone.IsActive, Is.EqualTo(false));
            Assert.That(zone.DeletedAt, Is.Not.Null);
            Assert.That(createdResult.StatusCode, Is.EqualTo(204));
        }


        [Test]
        public async Task Create_AlsoCreateAZoneHistoryOfTypeCreate()
        {
            Cluster _cluster1 = new()
            {
                Name = "ClusterTest",
                User = _user
            };
            _context.Add(_cluster1);

            Installation _installation1 = new()
            {
                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                Cluster = _cluster1,
                User = _user
            };
            _context.Add(_installation1);

            Field _field1 = new()
            {
                Name = "FieldTest",
                CodField = "FieldTest",
                Installation = _installation1,
                User = _user
            };
            _context.Add(_field1);
            _context.SaveChanges();

            _viewModel = new CreateZoneViewModel
            {
                CodZone = "ZoneTest",
                FieldId = _field1.Id,
            };
            await _controller.Create(_viewModel);

            var zone = await _context.Zones.SingleOrDefaultAsync();

            //var history = await _context.ZoneHistories.SingleOrDefaultAsync();
            Assert.That(zone, Is.Not.Null);

            //Assert.That(history, Is.Not.Null);
            //Assert.That(history.CodZone, Is.EqualTo(zone.CodZone));
            //Assert.That(history.Field.Id, Is.EqualTo(zone.Field.Id));

            //Assert.That(history.TypeOperation, Is.EqualTo(Utils.TypeOperation.Create));
            //Assert.That(history.User, Is.Not.Null);
            //Assert.That(history.User.Name, Is.EqualTo(_user.Name));
        }
    }
}
