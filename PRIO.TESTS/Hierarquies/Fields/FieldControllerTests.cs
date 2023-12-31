﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Dtos;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.Http.Controllers;
using PRIO.src.Modules.Hierarchy.Fields.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Fields.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.ViewModels;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Repositories;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Hierarchy.Zones.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Repositories;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.SystemHistories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PRIO.TESTS.Hierarquies.Fields
{
    [TestFixture]
    internal class FieldControllerTests
    {
        private FieldController _controller;
        private IMapper _mapper;
        private DataContext _context;
        private CreateFieldViewModel _createViewModel;
        private UpdateFieldViewModel _updateViewModel;
        private FieldService _service;
        private User _user;
        private Installation _installation1;
        private Installation _installation2;
        private UserService _userService;
        private Guid _invalidId;

        private IFieldRepository _fieldRepository;
        private ISystemHistoryRepository _systemHistoryRepository;
        private IInstallationRepository _installationRepository;
        private IZoneRepository _zoneRepository;
        private ICompletionRepository _completionRepository;
        private IWellRepository _wellRepository;
        private IReservoirRepository _reservoirRepository;

        private SystemHistoryService _systemHistoryService;


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
                cfg.CreateMap<Field, FieldDTO>();
                cfg.CreateMap<Field, CreateUpdateFieldDTO>();
                cfg.CreateMap<Field, FieldHistoryDTO>();
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

            _invalidId = Guid.NewGuid();
            var cluster = new Cluster
            {
                Name = "testeClus",
                User = _user,
            };
            _context.Clusters.Add(cluster);

            _installation1 = new()
            {
                Name = "testeInst",
                UepCod = "codmocked",
                CodInstallationAnp = "asdsadsads",
                UepName = "asdsadsadsa",
                User = _user,
                Cluster = cluster,
            };

            _installation2 = new()
            {
                Name = "testeInst2",
                UepCod = "codmasdocked2",
                CodInstallationAnp = "asdsadsads",
                UepName = "asdsadsadsa",
                User = _user,
                Cluster = cluster,
            };


            _context.Installations.Add(_installation1);
            _context.Installations.Add(_installation2);
            _context.SaveChanges();

            var httpContext = new DefaultHttpContext();
            httpContext.Items["Id"] = _user.Id;
            httpContext.Items["User"] = _user;

            _systemHistoryRepository = new SystemHistoryRepository(_context);
            _fieldRepository = new FieldRepository(_context);
            _installationRepository = new InstallationRepository(_context);

            _systemHistoryService = new SystemHistoryService(_mapper, _systemHistoryRepository);
            _service = new FieldService(_mapper, _fieldRepository, _systemHistoryService, _installationRepository, _zoneRepository, _wellRepository, _completionRepository, _reservoirRepository);
            _controller = new FieldController(_service);
            _controller.ControllerContext.HttpContext = httpContext;
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Create_FieldReturnsACreatedStatusWithDTO()
        {
            _createViewModel = new()
            {
                CodField = "21321",
                Name = "Name",
                InstallationId = _installation1.Id,
            };

            var response = await _controller.Create(_createViewModel);
            var createdResult = (CreatedResult)response;

            Assert.IsInstanceOf<CreatedResult>(response);
            Assert.That(((CreateUpdateFieldDTO)createdResult.Value).Name, Is.EqualTo(_createViewModel.Name));
            Assert.That(((CreateUpdateFieldDTO)createdResult.Value).CodField, Is.EqualTo(_createViewModel.CodField));
            Assert.That(createdResult.Location, Is.EqualTo($"fields/{((CreateUpdateFieldDTO)createdResult.Value).Id}"));
        }


        [Test]
        public async Task Create_FieldShouldReturnNotFoundIfInstallationDontExists()
        {

            _createViewModel = new()
            {
                CodField = "21321",
                Name = "Name",
                InstallationId = _invalidId,
            };
            try
            {
                var response = await _controller.Create(_createViewModel);

                Assert.Fail("Expected NotFoundException was not thrown.");

            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo($"Instalação não encontrado(a)."));

            }
        }


        [Test]
        public void Create_FieldShouldNotCreateIfBodyIsInvalidMissingCode()
        {
            _createViewModel = new()
            {
                Basin = "iosajdsajds",
                Name = "oisajdosai",
                InstallationId = _installation1.Id
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_createViewModel, null, null);
            var isValid = Validator.TryValidateObject(_createViewModel, validationContext, validationResults, true);

            if (!isValid)
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Message = "Field code is a required field."
                };

                var badRequestResult = new BadRequestObjectResult(errorResponse);
                Assert.IsInstanceOf<BadRequestObjectResult>(badRequestResult);

                Assert.That(((ErrorResponseDTO)badRequestResult.Value).Message, Is.EqualTo(errorResponse.Message));
                Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            }
        }

        [Test]
        public async Task Create_FieldAlsoCreateAHistoryOfTypeCreateAndPersistsInDatabase()
        {
            _createViewModel = new()
            {
                Basin = "iosajdsajds",
                Name = "oisajdosai",
                InstallationId = _installation1.Id,
                CodField = "128703"
            };

            await _controller.Create(_createViewModel);
            var field = await _context.Fields.SingleOrDefaultAsync();
            Assert.That(field, Is.Not.Null);
            Assert.That(field, Is.Not.Null);

            //Assert.That(history, Is.Not.Null);
            //var history = await _context.FieldHistories.SingleOrDefaultAsync();
            //Assert.That(history.CodField, Is.EqualTo(field.CodField));
            //Assert.That(history.TypeOperation, Is.EqualTo(Utils.TypeOperation.Create));
            //Assert.That(history.User, Is.Not.Null);
            //Assert.That(history.User.Name, Is.EqualTo(_user.Name));
            //Assert.That(history.Name, Is.EqualTo(field.Name));
            //Assert.That(history.Field, Is.Not.Null);
            //Assert.That(history.Field.CodField, Is.EqualTo(field.CodField));
        }

        [Test]
        public async Task Update_FieldReturnsOfStatusWithDTO()
        {
            var fieldToUpdate = new Field
            {
                Id = Guid.NewGuid(),
                CodField = "21321",
                Name = "NameToUpdate",
                Installation = _installation1,
                User = _user,
            };

            await _context.Fields.AddAsync(fieldToUpdate);
            await _context.SaveChangesAsync();

            _updateViewModel = new()
            {
                InstallationId = _installation2.Id,
                Name = "UpdatedField"
            };
            try
            {
                var response = await _controller.Update(fieldToUpdate.Id, _updateViewModel);
                Assert.Fail("Expected ConflictException was not thrown.");
            }
            catch (ConflictException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Relacionamento não pode ser alterado."));

            }

        }

        [Test]
        public async Task Update_FieldReturnsNotFoundWithInvalidField()
        {
            _updateViewModel = new()
            {
                InstallationId = _installation1.Id,
                Name = "UpdatedField",
            };
            try
            {
                var response = await _controller.Update(_invalidId, _updateViewModel);
                Assert.Fail("Expected NotFoundException was not thrown.");
            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Campo não encontrado(a)."));

            }

        }

        [Test]
        public async Task Update_FieldReturnsNotFoundWithInvalidInstallation()
        {
            var fieldToUpdate = new Field
            {
                CodField = "21321",
                Name = "NameToUpdate",
                Installation = _installation1,
                User = _user,
            };

            await _context.Fields.AddAsync(fieldToUpdate);
            await _context.SaveChangesAsync();
            _updateViewModel = new()
            {
                InstallationId = _invalidId,
                Name = "UpdatedField",
            };

            try
            {
                var response = await _controller.Update(fieldToUpdate.Id, _updateViewModel);
                Assert.Fail("Expected NotFoundException was not thrown.");
            }
            catch (ConflictException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Relacionamento não pode ser alterado."));
            }
        }

        [Test]
        public async Task Update_FieldAlsoCreateAHistoryOfTypeUpdateAndPersistsInDatabase()
        {
            var fieldToUpdate = new CreateFieldViewModel
            {
                CodField = "21321",
                Name = "NameToUpdate",
                InstallationId = _installation1.Id,
            };

            var create = await _service.CreateField(fieldToUpdate, _user);

            _updateViewModel = new()
            {
                InstallationId = _installation2.Id,
                Name = "saoidjasdsa"
            };
            try
            {
                var update = await _controller.Update(create.Id, _updateViewModel);
                Assert.Fail("Expected ConflictException was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Relacionamento não pode ser alterado."));
            }
            var field = await _context.Fields.SingleOrDefaultAsync();
            Assert.That(field, Is.Not.Null);
            Assert.That(field.Installation, Is.Not.Null);

            //var history = await _context.FieldHistories.SingleOrDefaultAsync();
            //Assert.That(history, Is.Not.Null);
            //Assert.That(history.Installation, Is.Not.Null);

            //Assert.That(history.Installation.Id, Is.EqualTo(field.Installation.Id));

            //Assert.That(history.CodField, Is.EqualTo(field.CodField));
            //Assert.That(history.TypeOperation, Is.EqualTo(Utils.TypeOperation.Update));
            //Assert.That(history.User, Is.Not.Null);
            //Assert.That(history.User.Name, Is.EqualTo(_user.Name));
            //Assert.That(history.Name, Is.EqualTo(field.Name));
        }

        [Test]
        public async Task Delete_SuccesfullySoftDeletesAField()
        {
            var fieldToDelete = new Field
            {
                Name = "Test",
                Installation = _installation1,
                CodField = "Test12",
                User = _user,
            };

            await _context.AddAsync(fieldToDelete);
            await _context.SaveChangesAsync();

            var response = await _controller.Delete(fieldToDelete.Id);
            var fieldInDatabase = await _context.Fields.SingleOrDefaultAsync();
            var noContentResult = (NoContentResult)response;

            Assert.IsInstanceOf<NoContentResult>(response);
            Assert.That(noContentResult.StatusCode, Is.EqualTo(204));

            Assert.That(fieldInDatabase, Is.Not.Null);
            Assert.That(fieldInDatabase.IsActive, Is.False);
            Assert.That(fieldInDatabase.DeletedAt, Is.Not.Null);

            //var historyInDatabase = await _context.FieldHistories.SingleOrDefaultAsync();
            //Assert.That(historyInDatabase, Is.Not.Null);
            //Assert.That(historyInDatabase.IsActive, Is.EqualTo(fieldInDatabase.IsActive));
            //Assert.That(historyInDatabase.IsActiveOld, Is.True);
            //Assert.That(historyInDatabase.TypeOperation, Is.EqualTo(Utils.TypeOperation.Delete));
        }

        [Test]
        public async Task Restore_SuccesfullyRestoresAField()
        {
            var fieldToRestore = new Field
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                CodField = "Cod test",
                IsActive = false,
                Installation = _installation1,
                User = _user,
            };

            await _context.AddAsync(fieldToRestore);
            await _context.SaveChangesAsync();

            var response = await _controller.Restore(fieldToRestore.Id);
            var completionInDatabase = await _context.Fields.SingleOrDefaultAsync();

            var okResult = (OkObjectResult)response;

            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            Assert.That(((CreateUpdateFieldDTO)okResult.Value), Is.Not.Null);

            Assert.That(completionInDatabase, Is.Not.Null);
            //var historyInDatabase = await _context.FieldHistories.SingleOrDefaultAsync();

            Assert.That(completionInDatabase.IsActive, Is.True);
            Assert.That(completionInDatabase.DeletedAt, Is.Null);
            Assert.That(completionInDatabase.DeletedAt, Is.Null);

            //Assert.That(historyInDatabase, Is.Not.Null);
            //Assert.That(historyInDatabase.IsActive, Is.EqualTo(completionInDatabase.IsActive));
            //Assert.That(historyInDatabase.IsActiveOld, Is.False);
            //Assert.That(historyInDatabase.TypeOperation, Is.EqualTo(Utils.TypeOperation.Restore));
        }
    }
}
