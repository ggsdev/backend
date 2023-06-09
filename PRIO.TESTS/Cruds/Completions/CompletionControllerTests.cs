﻿using AutoMapper;
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
using System.ComponentModel.DataAnnotations;

namespace PRIO.TESTS.Cruds.Completions
{
    [TestFixture]
    internal class CompletionControllerTests
    {
        private CompletionController _controller;
        private IMapper _mapper;
        private DataContext _context;
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
            _createViewModel = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = _well1.Id,
                CodCompletion = "Cod Teste COmpletion",
            };

            var response = await _controller.Create(_createViewModel);
            var createdResult = (CreatedResult)response;

            Assert.IsInstanceOf<CreatedResult>(response);
            Assert.That(((CompletionDTO)createdResult.Value).Name, Is.EqualTo($"{_well1.Name}_{_reservoir1.Zone?.CodZone}"));
            Assert.That(createdResult.Location, Is.EqualTo($"completions/{((CompletionDTO)createdResult.Value).Id}"));
        }

        [Test]
        public async Task Create_CheckIfReservoirAndWellFieldsMatch()
        {
            _createViewModel = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = _well2.Id,
                CodCompletion = "Different Fields",
            };

            var response = await _controller.Create(_createViewModel);
            var conflictResult = (ConflictObjectResult)response;

            Assert.IsInstanceOf<ConflictObjectResult>(response);
            Assert.That(conflictResult.StatusCode, Is.EqualTo(409));
            Assert.That(((ErrorResponseDTO)conflictResult.Value).Message, Is.EqualTo($"Reservoir: {_reservoir1.Name} and Well: {_well2.Name} doesn't belong to the same Field"));
        }

        [Test]
        public async Task Create_ShouldNotCreateWhenWellIsInvalid()
        {
            var wellInvalidId = Mock._invalidId;
            _createViewModel = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = wellInvalidId,
                CodCompletion = "Invalid well",
            };

            var response = await _controller.Create(_createViewModel);
            var notFoundResult = (NotFoundObjectResult)response;

            Assert.IsInstanceOf<NotFoundObjectResult>(response);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(((ErrorResponseDTO)notFoundResult.Value).Message, Is.EqualTo($"Well with id: {wellInvalidId} not found"));
        }

        [Test]
        public async Task Create_ShouldNotCreateWhenReservoirIsInvalid()
        {
            var reservoirInvalidId = Mock._invalidId;
            _createViewModel = new CreateCompletionViewModel
            {
                ReservoirId = Mock._invalidId,
                WellId = _well1.Id,
                CodCompletion = "Invalid reservoir",
            };

            var response = await _controller.Create(_createViewModel);
            var notFoundResult = (NotFoundObjectResult)response;

            Assert.IsInstanceOf<NotFoundObjectResult>(response);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(((ErrorResponseDTO)notFoundResult.Value).Message, Is.EqualTo($"Reservoir with id: {Mock._invalidId} not found"));
        }

        [Test]
        public async Task Create_ShouldNotCreateWhenCompletionNameAlreadyExists()
        {
            _createViewModel = new CreateCompletionViewModel
            {
                ReservoirId = _reservoir1.Id,
                WellId = _well1.Id,
                CodCompletion = "Invalid reservoir",
            };

            await _controller.Create(_createViewModel);

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
            Assert.That(((ErrorResponseDTO)conflictResult.Value!).Message, Is.EqualTo($"Completion with name: {_well1.Name}_{_reservoir1.Zone?.CodZone} already exists."));
        }

        [Test]
        public void Create_ShouldNotCreateCompletionIfBodyIsInvalid()
        {
            {
                _createViewModel = new CreateCompletionViewModel
                {
                    ReservoirId = _reservoir1.Id,
                    CodCompletion = "Invalid reservoir",
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
                CodCompletion = "mockCod",
            };

            await _controller.Create(_createViewModel);
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

        [Test]
        public async Task Update_ReturnsOkStatusWithDTO()
        {
            var completionToUpdate = SetUpUpdateMockData();
            _updateViewModel = new UpdateCompletionViewModel
            {
                ReservoirId = _reservoir2.Id,
                WellId = _well2.Id,
                CodCompletion = "updatedMock",
            };

            var response = await _controller.Update(completionToUpdate.Id, _updateViewModel);
            var updatedResult = (OkObjectResult)response;

            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.That(((CompletionDTO)updatedResult.Value).Name, Is.EqualTo($"{_well2.Name}_{_reservoir2.Zone?.CodZone}"));
            Assert.That(((CompletionDTO)updatedResult.Value).CodCompletion, Is.EqualTo(_updateViewModel.CodCompletion));
            Assert.That(((CompletionDTO)updatedResult.Value).Well, Is.Not.Null);
            Assert.That(((CompletionDTO)updatedResult.Value).Well.Name, Is.EqualTo(_well2.Name));
            Assert.That(((CompletionDTO)updatedResult.Value).Reservoir, Is.Not.Null);
            Assert.That(((CompletionDTO)updatedResult.Value).Reservoir.Name, Is.EqualTo(_reservoir2.Name));
            Assert.That(((CompletionDTO)updatedResult.Value).CodCompletion, Is.EqualTo(_updateViewModel.CodCompletion));
        }

        [Test]
        public async Task Update_ReturnsNotFoundWithInvalidCompletion()
        {

            _updateViewModel = new UpdateCompletionViewModel
            {
                ReservoirId = _reservoir2.Id,
                WellId = _well2.Id,
                CodCompletion = "updatedMock",
            };

            var response = await _controller.Update(Guid.NewGuid(), _updateViewModel);
            var notFoundResult = (NotFoundObjectResult)response;

            Assert.IsInstanceOf<NotFoundObjectResult>(response);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(((ErrorResponseDTO)notFoundResult.Value).Message, Is.EqualTo("Completion not found"));
        }

        [Test]
        public async Task Update_ReturnsNotFoundWithInvalidWell()
        {
            var completionToUpdate = SetUpUpdateMockData();

            _updateViewModel = new UpdateCompletionViewModel
            {
                WellId = Guid.NewGuid(),
                ReservoirId = _reservoir1.Id,
                CodCompletion = "updatedMock",
            };

            var response = await _controller.Update(completionToUpdate.Id, _updateViewModel);
            var notFoundResult = (NotFoundObjectResult)response;

            Assert.IsInstanceOf<NotFoundObjectResult>(response);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(((ErrorResponseDTO)notFoundResult.Value).Message, Is.EqualTo("Well not found"));
        }

        [Test]
        public async Task Update_ReturnsNotFoundWithInvalidReservoir()
        {
            var completionToUpdate = SetUpUpdateMockData();

            _updateViewModel = new UpdateCompletionViewModel
            {
                ReservoirId = Guid.NewGuid(),
                WellId = _well1.Id,
                CodCompletion = "updatedMock",
            };

            var response = await _controller.Update(completionToUpdate.Id, _updateViewModel);
            var notFoundResult = (NotFoundObjectResult)response;

            Assert.IsInstanceOf<NotFoundObjectResult>(response);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(((ErrorResponseDTO)notFoundResult.Value).Message, Is.EqualTo("Reservoir not found"));
        }

        [Test]
        public async Task Update_ShouldNotBeAbleToUpdateIfWellAndReservoirNotSameField()
        {
            var completionToUpdate = SetUpUpdateMockData();

            _updateViewModel = new UpdateCompletionViewModel
            {
                ReservoirId = _reservoir2.Id,
                WellId = _well1.Id,
                CodCompletion = "updatedMock",
            };

            var response = await _controller.Update(completionToUpdate.Id, _updateViewModel);
            var conflictResult = (ConflictObjectResult)response;

            Assert.IsInstanceOf<ConflictObjectResult>(response);
            Assert.That(conflictResult.StatusCode, Is.EqualTo(409));
            Assert.That(((ErrorResponseDTO)conflictResult.Value).Message, Is.EqualTo($"Well: {_well1.Name} and Reservoir: {_reservoir2.Name} doesn't belong to the same Field"));
        }

        [Test]
        public async Task Update_AlsoCreateAHistoryOfTypeUpdateAndPersistsInDatabase()
        {
            var completionToUpdate = SetUpUpdateMockData();
            _updateViewModel = new UpdateCompletionViewModel
            {
                ReservoirId = _reservoir2.Id,
                WellId = _well2.Id,
                CodCompletion = "updatedMock",
            };

            await _controller.Update(completionToUpdate.Id, _updateViewModel);
            var completion = await _context.Completions.SingleOrDefaultAsync();
            var history = await _context.CompletionHistories.SingleOrDefaultAsync();

            Assert.That(history, Is.Not.Null);
            Assert.That(completion, Is.Not.Null);
            Assert.That(history.CodCompletion, Is.EqualTo(completion.CodCompletion));
            Assert.That(history.TypeOperation, Is.EqualTo(Utils.TypeOperation.Update));
            Assert.That(history.User, Is.Not.Null);
            Assert.That(history.User.Name, Is.EqualTo(_user.Name));
            Assert.That(history.Name, Is.EqualTo(completion.Name));
            Assert.That(history.Well, Is.Not.Null);
            Assert.That(completion.Well, Is.Not.Null);
            Assert.That(history.Well.Name, Is.EqualTo(completion.Well.Name));
            Assert.That(history.Reservoir?.Name, Is.EqualTo(completion.Reservoir?.Name));
        }

        [Test]
        public async Task Delete_SuccesfullySoftDeletesACompletion()
        {
            var completionToDelete = new Completion
            {
                Name = "Test",
                CodCompletion = "Cod test",
                Well = _well1,
                Reservoir = _reservoir1,
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
            var completionToRestore = new Completion
            {
                Name = "Test",
                CodCompletion = "Cod test",
                Well = _well1,
                Reservoir = _reservoir1,
                IsActive = false,
            };

            _context.Add(completionToRestore);
            _context.SaveChanges();

            var response = await _controller.Restore(completionToRestore.Id);
            var completionInDatabase = await _context.Completions.SingleOrDefaultAsync();
            var historyInDatabase = await _context.CompletionHistories.SingleOrDefaultAsync();
            var okResult = (OkObjectResult)response;

            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(((CompletionDTO)okResult.Value), Is.Not.Null);

            Assert.That(completionInDatabase, Is.Not.Null);
            Assert.That(historyInDatabase, Is.Not.Null);
            Assert.That(completionInDatabase.IsActive, Is.True);
            Assert.That(historyInDatabase.IsActive, Is.EqualTo(completionInDatabase.IsActive));
            Assert.That(historyInDatabase.IsActiveOld, Is.False);
            Assert.That(historyInDatabase.TypeOperation, Is.EqualTo(Utils.TypeOperation.Restore));
            Assert.That(completionInDatabase.DeletedAt, Is.Null);
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
                Cluster = _cluster1,
                CodInstallation = "unique1"
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
                Cluster = _cluster2,
                CodInstallation = "unique2"
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

        private Completion SetUpUpdateMockData()
        {
            var _completionToUpdate = new Completion
            {
                Name = "Test",
                CodCompletion = "Cod test",
                Well = _well1,
                Reservoir = _reservoir1,
            };

            _context.Add(_completionToUpdate);
            _context.SaveChanges();

            return _completionToUpdate;
        }
    }
}
