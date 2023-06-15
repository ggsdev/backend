using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Controllers;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.HierarchyDTOS.ClusterDTOS;
using PRIO.DTOS.HierarchyDTOS.InstallationDTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.ViewModels.Installations;
using System.ComponentModel.DataAnnotations;

namespace PRIO.TESTS.Cruds.Installations
{
    internal class InstallationControllerTests
    {
        private InstallationController _controller;
        private IMapper _mapper;
        private DataContext _context;
        private CreateInstallationViewModel _viewModel;
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

            _controller = new InstallationController(_context, _mapper);
            _controller.ControllerContext.HttpContext = httpContext;
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        [Test]
        public async Task Create_InstallationReturnsACreatedStatusWithDTO()
        {

            Cluster _cluster1 = new()
            {
                Name = "ClusterTest",
                User = _user
            };
            _context.Add(_cluster1);
            _context.SaveChanges();

            _viewModel = new CreateInstallationViewModel
            {
                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                ClusterId = _cluster1.Id,
            };

            var response = await _controller.Create(_viewModel);
            var createdResult = (CreatedResult)response;

            Assert.IsInstanceOf<CreatedResult>(response);
            Assert.That(((CreateUpdateInstallationDTO)createdResult.Value).Name, Is.EqualTo(_viewModel.Name));
            Assert.That(createdResult.StatusCode, Is.EqualTo(201));
            Assert.That(createdResult.Location, Is.EqualTo($"installations/{((CreateUpdateInstallationDTO)createdResult.Value).Id}"));
        }
        //[Test]
        //public async Task Create_InstallationIsAlreadyExists()
        //{
        //    Cluster _cluster1 = new()
        //    {
        //        Name = "ClusterTest",
        //        User = _user
        //    };
        //    _context.Add(_cluster1);
        //    _context.SaveChanges();

        //    _viewModel = new CreateInstallationViewModel
        //    {
        //        Name = "InstallationTest",
        //        CodInstallationUep = "InstallationTest",
        //        ClusterId = _cluster1.Id,
        //    };
        //    await _controller.Create(_viewModel);
        //    var response = await _controller.Create(_viewModel);

        //    var createdResult = (ConflictObjectResult)response;


        //    Assert.IsInstanceOf<ConflictObjectResult>(response);
        //    Assert.That(((ErrorResponseDTO)createdResult.Value).Message, Is.EqualTo($"Installation with code: {_viewModel.CodInstallationUep} already exists, try another code."));
        //    Assert.That(createdResult.StatusCode, Is.EqualTo(409));
        //}

        [Test]
        public async Task Create_CheckRequiredClusterIdField()
        {
            _viewModel = new CreateInstallationViewModel
            {
                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_viewModel, null, null);
            bool isValid = Validator.TryValidateObject(_viewModel, validationContext, validationResults, true);

            if (!isValid)
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Message = "ClusterId is required."
                };

                var badRequestResult = new BadRequestObjectResult(errorResponse);
                Assert.IsInstanceOf<BadRequestObjectResult>(badRequestResult);

                Assert.That(((ErrorResponseDTO)badRequestResult.Value).Message, Is.EqualTo(errorResponse.Message));
                Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            }
        }

        [Test]
        public async Task Create_CheckRequiredNameField()
        {
            Cluster _cluster1 = new()
            {
                Name = "ClusterTest",
                User = _user
            };
            _context.Add(_cluster1);
            _context.SaveChanges();

            _viewModel = new CreateInstallationViewModel
            {
                CodInstallationUep = "12312",
                ClusterId = _cluster1.Id,
            };

            try
            {
                var response = await _controller.Create(_viewModel);

                Assert.IsInstanceOf<BadRequestObjectResult>(response);
                var createdResult = (BadRequestObjectResult)response;

                Assert.That(createdResult.Value, Is.EqualTo("Name is required."));
                Assert.That(createdResult.StatusCode, Is.EqualTo(400));
            }
            catch (DbUpdateException ex)
            {
                Assert.IsInstanceOf<DbUpdateException>(ex);
                Assert.That(ex.Message, Contains.Substring("Required properties '{'Name'}' are missing"));
            }
        }

        //PATCH
        [Test]
        public async Task Update_InstallationReturnsACreatedStatusWithDTO()
        {
            Cluster _cluster1 = new()
            {
                Name = "ClusterTest",
                User = _user
            };
            _context.Add(_cluster1);

            Installation _installation = new()
            {
                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                Cluster = _cluster1
            };
            _context.Add(_installation);
            _context.SaveChanges();

            var _viewModel2 = new UpdateInstallationViewModel
            {
                Name = "InstallationTest2",
                CodInstallationUep = "InstallationTest2",
                ClusterId = _cluster1.Id,
            };

            var response = await _controller.Update(_installation.Id, _viewModel2);
            var createdResult = (OkObjectResult)response;

            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.That(((CreateUpdateInstallationDTO)createdResult.Value).Name, Is.EqualTo(_viewModel2.Name));
            Assert.That(((CreateUpdateInstallationDTO)createdResult.Value).CodInstallationUep, Is.EqualTo(_viewModel2.CodInstallationUep));
            Assert.That(createdResult.StatusCode, Is.EqualTo(200));
        }

        //DELETE
        [Test]
        public async Task Delete_InstallationReturnsANoContent()
        {
            Cluster _cluster1 = new()
            {
                Name = "ClusterTest",
                User = _user
            };
            _context.Add(_cluster1);

            Installation _installation = new()
            {
                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                Cluster = _cluster1
            };
            _context.Add(_installation);
            _context.SaveChanges();

            var response = await _controller.Delete(_installation.Id);
            var createdResult = (NoContentResult)response;

            Assert.IsInstanceOf<NoContentResult>(response);
            var installation = await _context.Installations.SingleOrDefaultAsync();
            Assert.That(installation.IsActive, Is.EqualTo(false));
            Assert.That(installation.DeletedAt, Is.Not.Null);
            Assert.That(createdResult.StatusCode, Is.EqualTo(204));
        }


        [Test]
        public async Task Create_AlsoCreateAInstallationHistoryOfTypeCreate()
        {
            Cluster _cluster1 = new()
            {
                Name = "ClusterTest",
                User = _user
            };
            _context.Add(_cluster1);
            _context.SaveChanges();

            _viewModel = new CreateInstallationViewModel
            {
                Name = "InstallationTest",
                CodInstallationUep = "InstallationTest",
                ClusterId = _cluster1.Id,
            };

            await _controller.Create(_viewModel);
            var installation = await _context.Installations.SingleOrDefaultAsync();

            Assert.That(installation, Is.Not.Null);

            //var history = await _context.InstallationHistories.SingleOrDefaultAsync();
            //Assert.That(history, Is.Not.Null);
            //Assert.That(history.Name, Is.EqualTo(installation.Name));
            //Assert.That(history.Cluster.Id, Is.EqualTo(installation.Cluster.Id));
            //Assert.That(history.CodInstallationUep, Is.EqualTo(installation.CodInstallationUep));

            //Assert.That(history.TypeOperation, Is.EqualTo(Utils.TypeOperation.Create));
            //Assert.That(history.User, Is.Not.Null);
            //Assert.That(history.User.Name, Is.EqualTo(_user.Name));
            //Assert.That(history.Name, Is.EqualTo(installation.Name));
        }
    }
}
