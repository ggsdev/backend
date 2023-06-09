using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Controllers;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.ClusterDTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.Models.Clusters;
using PRIO.Models.Users;
using PRIO.ViewModels.Clusters;

namespace PRIO.TESTS.Cruds.Clusters
{
    internal class ClusterControllerTests
    {
        private ClusterController _controller;
        private IMapper _mapper;
        private DataContext _context;
        private CreateClusterViewModel _viewModel;
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
                cfg.CreateMap<User, UserDTO>();
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

            _controller = new ClusterController(_context, _mapper);
            _controller.ControllerContext.HttpContext = httpContext;
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Create_ClusterReturnsACreatedStatusWithDTO()
        {
            _viewModel = new CreateClusterViewModel
            {
                Name = "ClusterTest",
                CodCluster = "12312",
                UepCode = "12332",
            };

            var response = await _controller.Create(_viewModel);
            var createdResult = (CreatedResult)response;

            Assert.IsInstanceOf<CreatedResult>(response);
            Assert.That(((ClusterDTO)createdResult.Value).Name, Is.EqualTo(_viewModel.Name));
            Assert.That(createdResult.StatusCode, Is.EqualTo(201));
            Assert.That(createdResult.Location, Is.EqualTo($"clusters/{((ClusterDTO)createdResult.Value).Id}"));
        }

        [Test]
        public async Task Create_ClusterIsAlreadyExists()
        {
            _viewModel = new CreateClusterViewModel
            {
                Name = "ClusterTest",
                CodCluster = "12312",
                UepCode = "12332",
            };
            await _controller.Create(_viewModel);
            var response = await _controller.Create(_viewModel);

            var createdResult = (ConflictObjectResult)response;


            Assert.IsInstanceOf<ConflictObjectResult>(response);
            Assert.That(((ErrorResponseDTO)createdResult.Value).Message, Is.EqualTo($"Cluster with code: {_viewModel.CodCluster} already exists."));
            Assert.That(createdResult.StatusCode, Is.EqualTo(409));

        }

        [Test]
        public async Task Create_CheckRequiredField()
        {
            _viewModel = new CreateClusterViewModel
            {
                CodCluster = "12312",
                UepCode = "12332",
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
        [Test]
        public async Task Create_AlsoCreateAClusterHistoryOfTypeCreate()
        {
            _viewModel = new CreateClusterViewModel
            {
                Name = "ClusterTest",
                CodCluster = "12312",
                UepCode = "12332",
            };

            await _controller.Create(_viewModel);
            var cluster = await _context.Clusters.SingleOrDefaultAsync();
            var history = await _context.ClustersHistories.SingleOrDefaultAsync();

            Assert.That(history, Is.Not.Null);
            Assert.That(cluster, Is.Not.Null);
            Assert.That(history.CodCluster, Is.EqualTo(cluster.CodCluster));
            Assert.That(history.TypeOperation, Is.EqualTo(Utils.TypeOperation.Create));
            Assert.That(history.User, Is.Not.Null);
            Assert.That(history.User.Name, Is.EqualTo(_user.Name));
            Assert.That(history.Name, Is.EqualTo(cluster.Name));
        }
    }
}
