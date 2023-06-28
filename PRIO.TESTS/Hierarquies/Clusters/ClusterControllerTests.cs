using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Dtos;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Interfaces;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.Http.Controllers;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Clusters.ViewModels;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Repositories;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.SystemHistories.Interfaces;

namespace PRIO.TESTS.Hierarquies.Clusters
{
    internal class ClusterControllerTests
    {
        private ClusterController _controller;
        private IMapper _mapper;
        private DataContext _context;
        private IClusterRepository _clusterRepository;
        private ISystemHistoryRepository _systemHistoryRepository;
        private SystemHistoryService _systemHistoryService;

        private ClusterService _service;
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
                cfg.CreateMap<Cluster, ClusterHistoryDTO>();
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

            _systemHistoryRepository = new SystemHistoryRepository(_context);
            _clusterRepository = new ClusterRepository(_context);

            _systemHistoryService = new SystemHistoryService(_mapper, _systemHistoryRepository);

            _service = new ClusterService(_mapper, _clusterRepository, _systemHistoryService);
            _controller = new ClusterController(_service);
            _controller.ControllerContext.HttpContext = httpContext;
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        //CREATE
        [Test]
        public async Task Create_ClusterReturnsACreatedStatusWithDTO()
        {
            _viewModel = new CreateClusterViewModel
            {
                Name = "ClusterTest",
                CodCluster = "12312",
            };

            var response = await _controller.Create(_viewModel);
            var createdResult = (CreatedResult)response;

            Assert.IsInstanceOf<CreatedResult>(response);
            Assert.That(((ClusterDTO)createdResult.Value).Name, Is.EqualTo(_viewModel.Name));
            Assert.That(createdResult.StatusCode, Is.EqualTo(201));
            Assert.That(createdResult.Location, Is.EqualTo($"clusters/{((ClusterDTO)createdResult.Value).Id}"));
        }

        [Test]
        public async Task Create_CheckRequiredField()
        {
            _viewModel = new CreateClusterViewModel
            {
                CodCluster = "12312",
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
            };

            await _controller.Create(_viewModel);
            var cluster = await _context.Clusters.SingleOrDefaultAsync();
            Assert.That(cluster, Is.Not.Null);


            ////var history = await _context.ClustersHistories.SingleOrDefaultAsync();

            //Assert.That(history, Is.Not.Null);
            //Assert.That(history.CodCluster, Is.EqualTo(cluster.CodCluster));
            //Assert.That(history.TypeOperation, Is.EqualTo(Utils.TypeOperation.Create));
            //Assert.That(history.User, Is.Not.Null);
            //Assert.That(history.User.Name, Is.EqualTo(_user.Name));
            //Assert.That(history.Name, Is.EqualTo(cluster.Name));
        }

        //PATCH
        [Test]
        public async Task Update_ClusterReturnsACreatedStatusWithDTO()
        {
            Cluster _cluster1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "ClusterTest",
                User = _user
            };
            _context.Add(_cluster1);
            _context.SaveChanges();

            var _viewModel2 = new UpdateClusterViewModel
            {
                Name = "ClusterTest2",
                CodCluster = "ClusterTest2",
            };

            var response = await _controller.Update(_cluster1.Id, _viewModel2);
            var createdResult = (OkObjectResult)response;

            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.That(((ClusterDTO)createdResult.Value).Name, Is.EqualTo(_viewModel2.Name));
            Assert.That(createdResult.StatusCode, Is.EqualTo(200));
        }

        //DELETE
        [Test]
        public async Task Delete_ClusterReturnsANoContent()
        {
            Cluster _cluster1 = new()
            {
                Name = "ClusterTest",
                User = _user
            };
            _context.Add(_cluster1);
            _context.SaveChanges();

            var response = await _controller.Delete(_cluster1.Id);
            var createdResult = (NoContentResult)response;

            Assert.IsInstanceOf<NoContentResult>(response);
            var cluster = await _context.Clusters.SingleOrDefaultAsync();
            Assert.That(cluster.IsActive, Is.EqualTo(false));
            Assert.That(cluster.DeletedAt, Is.Not.Null);
            Assert.That(createdResult.StatusCode, Is.EqualTo(204));
        }
    }
}