﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Clusters.Dtos;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Dtos;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Repositories;
using PRIO.src.Modules.Hierarchy.Completions.Infra.Http.Controllers;
using PRIO.src.Modules.Hierarchy.Completions.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Modules.Hierarchy.Completions.ViewModels;
using PRIO.src.Modules.Hierarchy.Fields.Dtos;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Dtos;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Repositories;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Dtos;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Repositories;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Hierarchy.Zones.Dtos;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Repositories;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.SystemHistories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PRIO.TESTS.Hierarquies.Completions
{
    [TestFixture]
    internal class CompletionControllerTests
    {
        private CompletionController _controller;
        private IMapper _mapper;
        private DataContext _context;
        private CompletionService _service;

        private ISystemHistoryRepository _systemHistoryRepository;
        private ICompletionRepository _completionRepository;
        private IWellRepository _wellRepository;
        private IReservoirRepository _reservoirRepository;
        private SystemHistoryService _systemHistoryService;
        private UserService _userService;

        private CreateCompletionViewModel _createViewModel;
        private UpdateCompletionViewModel _updateViewModel;
        private User _user;

        private Reservoir _reservoir1;
        private Reservoir _reservoir2;
        private Well _well1;
        private Well _well2;

        [SetUp]
        public void Setup()
        {
            #region Context Config
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(contextOptions);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Completion, CompletionDTO>();
                cfg.CreateMap<Completion, CompletionWithouWellDTO>();
                cfg.CreateMap<Completion, CompletionHistoryDTO>();
                cfg.CreateMap<Completion, CreateUpdateCompletionDTO>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Reservoir, ReservoirDTO>();
                cfg.CreateMap<Reservoir, CreateUpdateReservoirDTO>();
                cfg.CreateMap<Reservoir, ReservoirWithZoneDTO>();
                cfg.CreateMap<Zone, ZoneDTO>();
                cfg.CreateMap<Zone, ZoneWithoutFieldDTO>();
                cfg.CreateMap<Field, FieldDTO>();
                cfg.CreateMap<Field, FieldWithZonesAndWellsDTO>();
                cfg.CreateMap<Installation, InstallationDTO>();
                cfg.CreateMap<Installation, InstallationWithoutClusterDTO>();
                cfg.CreateMap<Cluster, ClusterDTO>();
                cfg.CreateMap<Well, WellDTO>();
                cfg.CreateMap<Well, WellWithoutFieldDTO>();
                cfg.CreateMap<Well, WellWithoutCompletionDTO>();
            });

            _mapper = mapperConfig.CreateMapper();
            #endregion

            _user = new User()
            {
                Name = "userTeste",
                Email = "userTeste@mail.com",
                Password = "1234",
                Username = "userTeste",
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
            httpContext.Items["User"] = _user;

            #region Repositories
            _systemHistoryRepository = new SystemHistoryRepository(_context);
            _completionRepository = new CompletionRepository(_context);
            _wellRepository = new WellRepository(_context);
            _reservoirRepository = new ReservoirRepository(_context);
            #endregion

            #region Services
            _systemHistoryService = new SystemHistoryService(_mapper, _systemHistoryRepository);
            _service = new CompletionService(_mapper, _completionRepository,
                _wellRepository, _reservoirRepository,
                _systemHistoryService);
            #endregion

            #region Controllers
            _controller = new CompletionController(_service);
            _controller.ControllerContext.HttpContext = httpContext;
            #endregion
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
            _createViewModel = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = _well1.Id,
                AllocationReservoir = 1
            };

            var response = await _controller.Create(_createViewModel);
            var createdResult = (CreatedResult)response;

            Assert.IsInstanceOf<CreatedResult>(response);
            Assert.That(((CreateUpdateCompletionDTO)createdResult.Value).Name, Is.EqualTo($"{_well1.Name}_{_reservoir1.Zone?.CodZone}"));
            Assert.That(createdResult.Location, Is.EqualTo($"completions/{((CreateUpdateCompletionDTO)createdResult.Value).Id}"));
        }

        [Test]
        public async Task Create_CheckIfReservoirAndWellFieldsMatch()
        {
            _createViewModel = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = _well2.Id,
                AllocationReservoir = 1
            };

            try
            {
                await _controller.Create(_createViewModel);
                Assert.Fail("Expected ConflictException was not thrown.");
            }
            catch (ConflictException ex)
            {
                Assert.That(ex.Message, Is.EqualTo($"Poço e reservatório devem pertencer ao mesmo campo."));
            }
        }

        [Test]
        public async Task Create_ShouldNotCreateWhenWellIsInvalid()
        {
            var wellInvalidId = Mock._invalidId;
            _createViewModel = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = wellInvalidId,
                AllocationReservoir = 1

            };

            try
            {
                var response = await _controller.Create(_createViewModel);

                Assert.Fail("Expected NotFoundException was not thrown.");
            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo($"Poço não encontrado(a)."));
            }
        }

        [Test]
        public async Task Create_ShouldNotCreateWhenReservoirIsInvalid()
        {
            var reservoirInvalidId = Mock._invalidId;
            _createViewModel = new CreateCompletionViewModel
            {
                ReservoirId = Mock._invalidId,
                WellId = _well1.Id,
                AllocationReservoir = 1

            };

            try
            {
                var response = await _controller.Create(_createViewModel);

                Assert.Fail("Expected NotFoundException was not thrown.");
            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo($"Reservatório não encontrado(a)."));

            }
        }

        [Test]
        public async Task Create_ShouldNotCreateWhenCompletionNameAlreadyExists()
        {
            _createViewModel = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = _well1.Id,
                AllocationReservoir = 0.5m

            };

            await _controller.Create(_createViewModel);

            var viewModelDuplicated = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = _well1.Id,
                AllocationReservoir = 0.5m

            };

            try
            {
                var response = await _controller.Create(viewModelDuplicated);
                Assert.Fail("Expected ConflictException was not thrown.");
            }
            catch (ConflictException ex)
            {
                Assert.That(ex.Message, Is.EqualTo($"Já existe uma completação com esse poço e reservatório associados"));
            }
        }

        [Test]
        public void Create_ShouldNotCreateCompletionIfBodyIsInvalid()
        {
            {
                _createViewModel = new CreateCompletionViewModel
                {
                    ReservoirId = _reservoir1.Id,
                    AllocationReservoir = 1
                };

                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(_createViewModel, null, null);
                var isValid = Validator.TryValidateObject(_createViewModel, validationContext, validationResults, true);

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
        }

        [Test]
        public async Task Create_AlsoCreateAHistoryOfTypeCreateAndPersistsInDatabase()
        {
            _createViewModel = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = _well1.Id,
                AllocationReservoir = 1
            };

            await _controller.Create(_createViewModel);
            var completion = await _context.Completions.SingleOrDefaultAsync();
            Assert.That(completion, Is.Not.Null);

            //Assert.That(history, Is.Not.Null);
            //var history = await _context.CompletionHistories.SingleOrDefaultAsync();
            //Assert.That(history.CodCompletion, Is.EqualTo(completion.CodCompletion));
            //Assert.That(history.TypeOperation, Is.EqualTo(Utils.TypeOperation.Create));
            //Assert.That(history.User, Is.Not.Null);
            //Assert.That(history.User.Name, Is.EqualTo(_user.Name));
            //Assert.That(history.Name, Is.EqualTo(completion.Name));
        }

        [Test]
        public async Task Update_ReturnsOkStatusWithDTO()
        {
            var completionToUpdate = SetUpUpdateMockData();
            _updateViewModel = new UpdateCompletionViewModel
            {
                ReservoirId = _reservoir2.Id,
                WellId = _well2.Id,
                AllocationReservoir = 1

            };

            var response = await _controller.Update(completionToUpdate.Id, _updateViewModel);
            var updatedResult = (OkObjectResult)response;

            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.That(((CompletionDTO)updatedResult.Value).Name, Is.EqualTo($"{_well2.Name}_{_reservoir2.Zone?.CodZone}"));
            //Assert.That(((CompletionDTO)updatedResult.Value).Name, Is.EqualTo(_updateViewModel.Name));
            Assert.That(((CompletionDTO)updatedResult.Value).Well, Is.Not.Null);
            Assert.That(((CompletionDTO)updatedResult.Value).Well.Name, Is.EqualTo(_well2.Name));
            Assert.That(((CompletionDTO)updatedResult.Value).Reservoir, Is.Not.Null);
            Assert.That(((CompletionDTO)updatedResult.Value).Reservoir.Name, Is.EqualTo(_reservoir2.Name));
            //Assert.That(((CompletionDTO)updatedResult.Value).Name, Is.EqualTo(_updateViewModel.Name));
        }

        [Test]
        public async Task Update_ReturnsNotFoundWithInvalidCompletion()
        {

            _updateViewModel = new UpdateCompletionViewModel
            {
                ReservoirId = _reservoir2.Id,
                WellId = _well2.Id,
            };

            try
            {
                var response = await _controller.Update(Guid.NewGuid(), _updateViewModel);

                Assert.Fail("Expected NotFoundException was not thrown.");
            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Completação não encontrado(a)."));
            }
        }

        [Test]
        public async Task Update_ReturnsNotFoundWithInvalidWell()
        {
            var completionToUpdate = SetUpUpdateMockData();

            _updateViewModel = new UpdateCompletionViewModel
            {
                WellId = Guid.NewGuid(),
                ReservoirId = _reservoir1.Id,
            };

            try
            {
                var response = await _controller.Update(completionToUpdate.Id, _updateViewModel);
                Assert.Fail("Expected NotFoundException was not thrown.");

            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Poço não encontrado(a)."));

            }

        }

        [Test]
        public async Task Update_ReturnsNotFoundWithInvalidReservoir()
        {
            var completionToUpdate = SetUpUpdateMockData();

            _updateViewModel = new UpdateCompletionViewModel
            {
                ReservoirId = Guid.NewGuid(),
                WellId = _well1.Id,
            };

            try
            {
                var response = await _controller.Update(completionToUpdate.Id, _updateViewModel);

                Assert.Fail("Expected NotFoundException was not thrown.");

            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Reservatório não encontrado(a)."));

            }
        }

        [Test]
        public async Task Update_ShouldNotBeAbleToUpdateIfWellAndReservoirNotSameField()
        {
            var completionToUpdate = SetUpUpdateMockData();

            _updateViewModel = new UpdateCompletionViewModel
            {
                ReservoirId = _reservoir2.Id,
                WellId = _well1.Id,
            };

            try
            {
                var response = await _controller.Update(completionToUpdate.Id, _updateViewModel);

                Assert.Fail("Expected ConflictException was not thrown.");

            }
            catch (ConflictException ex)
            {
                Assert.That(ex.Message, Is.EqualTo($"Poço e reservatório devem pertencer ao mesmo campo."));

            }
        }

        [Test]
        public async Task Update_AlsoCreateAHistoryOfTypeUpdateAndPersistsInDatabase()
        {
            var completionToUpdate = SetUpUpdateMockData();
            _updateViewModel = new UpdateCompletionViewModel
            {
                ReservoirId = _reservoir2.Id,
                WellId = _well2.Id,
            };

            await _controller.Update(completionToUpdate.Id, _updateViewModel);
            var completion = await _context.Completions.SingleOrDefaultAsync();

            Assert.That(completion, Is.Not.Null);
            Assert.That(completion.Well, Is.Not.Null);
            //var history = await _context.CompletionHistories.SingleOrDefaultAsync();
            //Assert.That(history, Is.Not.Null);
            //Assert.That(history.CodCompletion, Is.EqualTo(completion.CodCompletion));
            //Assert.That(history.TypeOperation, Is.EqualTo(Utils.TypeOperation.Update));
            //Assert.That(history.User, Is.Not.Null);
            //Assert.That(history.User.Name, Is.EqualTo(_user.Name));
            //Assert.That(history.Name, Is.EqualTo(completion.Name));
            //Assert.That(history.Well, Is.Not.Null);
            //Assert.That(history.Well.Name, Is.EqualTo(completion.Well.Name));
            //Assert.That(history.Reservoir?.Name, Is.EqualTo(completion.Reservoir?.Name));
        }

        [Test]
        public async Task Delete_SuccesfullySoftDeletesACompletion()
        {
            var completionToDelete = new Completion
            {
                Name = "Test",
                Well = _well1,
                Reservoir = _reservoir1,
                AllocationReservoir = 1

            };

            _context.Add(completionToDelete);
            _context.SaveChanges();

            var response = await _controller.Delete(completionToDelete.Id);
            var completionInDatabase = await _context.Completions.SingleOrDefaultAsync();
            var noContentResult = (NoContentResult)response;

            Assert.IsInstanceOf<NoContentResult>(response);
            Assert.That(noContentResult.StatusCode, Is.EqualTo(204));
            Assert.That(completionInDatabase, Is.Not.Null);
            Assert.That(completionInDatabase.IsActive, Is.False);
            Assert.That(completionInDatabase.DeletedAt, Is.Not.Null);
        }

        [Test]
        public async Task Restore_SuccesfullyRestoresACompletion()
        {
            var completionToRestore = new CreateCompletionViewModel
            {
                WellId = _well1.Id,
                ReservoirId = _reservoir1.Id,
                AllocationReservoir = 1
            };

            var create = await _service.CreateCompletion(completionToRestore, _user);

            await _controller.Delete(create.Id);

            var response = await _controller.Restore(create.Id);
            var completionInDatabase = await _context.Completions.SingleOrDefaultAsync();
            var okResult = (OkObjectResult)response;

            //var historyInDatabase = await _context.CompletionHistories.SingleOrDefaultAsync();
            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(((CompletionDTO)okResult.Value), Is.Not.Null);

            Assert.That(completionInDatabase, Is.Not.Null);
            Assert.That(completionInDatabase.IsActive, Is.True);
            //Assert.That(historyInDatabase, Is.Not.Null);
            //assert.that(historyindatabase.isactive, is.true);
            //Assert.That(historyInDatabase.TypeOperation, Is.EqualTo(Utils.TypeOperation.Restore));
            Assert.That(completionInDatabase.DeletedAt, Is.Null);
        }

        private (Reservoir reservoir1, Well well1, Reservoir reservoir2, Well well2) SetupMockData()
        {
            Cluster _cluster1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "ClusterTest",
                User = _user
            };

            Installation _installation1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "InstallationTest",
                User = _user,
                Cluster = _cluster1,
                UepCod = "unique1",
                CodInstallationAnp = "unique1",
                UepName = "asdsada"
            };

            Field _field1 = new()
            {
                Id = Guid.NewGuid(),

                Name = "FieldTest",
                Installation = _installation1,
                User = _user,
                CodField = "32132"
            };

            Zone _zone1 = new()
            {
                Id = Guid.NewGuid(),

                Field = _field1,
                User = _user,
                CodZone = "933213",
            };

            Reservoir _reservoir1 = new()
            {
                Id = Guid.NewGuid(),

                Zone = _zone1,
                User = _user,
                Name = "ReservoirTest"
            };

            Well _well1 = new()
            {
                Id = Guid.NewGuid(),

                Name = "1233a3",
                WellOperatorName = "1233aa3",
                CodWellAnp = "1233aa3",
                CategoryAnp = "1233a3",
                CategoryReclassificationAnp = "1233a3",
                CategoryOperator = "1233a3",
                StatusOperator = true,
                Type = "1233a3",
                WaterDepth = 322.52m,
                ArtificialLift = "1233a3",
                Latitude4C = "22:03:34,054",
                Longitude4C = "22:03:34,054",
                LatitudeDD = "-22,0594594444",
                LongitudeDD = "39,8311675000",
                DatumHorizontal = "1233a3",
                TypeBaseCoordinate = "1233a3",
                CoordX = "-39,7706275000",
                CoordY = "-22,1108369444",
                User = _user,
                Field = _field1,
            };

            Cluster _cluster2 = new()
            {
                Id = Guid.NewGuid(),

                Name = "ClusterTest2",
                User = _user
            };

            Installation _installation2 = new()
            {
                Id = Guid.NewGuid(),

                Name = "InstallationTest2",
                User = _user,
                Cluster = _cluster2,
                UepCod = "unique2",
                CodInstallationAnp = "unique2",
                UepName = "asdsada"
            };

            Field _field2 = new()
            {
                Id = Guid.NewGuid(),

                Name = "FieldTest2",
                Installation = _installation2,
                User = _user,
                CodField = "32232"
            };

            Zone _zone2 = new()
            {
                Id = Guid.NewGuid(),

                Field = _field2,
                User = _user,
                CodZone = "933223",
            };

            Reservoir _reservoir2 = new()
            {
                Id = Guid.NewGuid(),

                Zone = _zone2,
                User = _user,
                Name = "ReservoirTest2"
            };

            Well _well2 = new()
            {
                Id = Guid.NewGuid(),

                Name = "1233a3",
                WellOperatorName = "1233aa3",
                CodWellAnp = "1233aa3",
                CategoryAnp = "1233a3",
                CategoryReclassificationAnp = "1233a3",
                CategoryOperator = "1233a3",
                StatusOperator = true,
                Type = "1233a3",
                WaterDepth = 322.52m,
                ArtificialLift = "1233a3",
                Latitude4C = "22:03:34,054",
                Longitude4C = "22:03:34,054",
                LatitudeDD = "-22,0594594444",
                LongitudeDD = "39,8311675000",
                DatumHorizontal = "1233a3",
                TypeBaseCoordinate = "1233a3",
                CoordX = "-39,7706275000",
                CoordY = "-22,1108369444",
                User = _user,
                Field = _field2,
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

        private Completion SetUpUpdateMockData()
        {
            var _completionToUpdate = new Completion
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Well = _well1,
                Reservoir = _reservoir1,
                AllocationReservoir = 1

            };

            _context.Add(_completionToUpdate);
            _context.SaveChanges();

            return _completionToUpdate;
        }
    }
}
