using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Controllers;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.HierarchyDTOS.FieldDTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.ViewModels.Fields;
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
        private User _user;
        private Installation _installation1;
        private Installation _installation2;
        private Guid _invalidId;

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
                CodInstallationUep = "codmocked",
                User = _user,
                Cluster = cluster,
            };

            _installation2 = new()
            {
                Name = "testeInst2",
                CodInstallationUep = "codmasdocked2",
                User = _user,
                Cluster = cluster,
            };


            _context.Installations.Add(_installation1);
            _context.Installations.Add(_installation2);
            _context.SaveChanges();

            var httpContext = new DefaultHttpContext();
            httpContext.Items["Id"] = _user.Id;
            httpContext.Items["User"] = _user;

            _controller = new FieldController(_context, _mapper);
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
            Assert.That(((FieldDTO)createdResult.Value).Name, Is.EqualTo(_createViewModel.Name));
            Assert.That(((FieldDTO)createdResult.Value).CodField, Is.EqualTo(_createViewModel.CodField));
            Assert.That(createdResult.Location, Is.EqualTo($"fields/{((FieldDTO)createdResult.Value).Id}"));
        }

        [Test]
        public async Task Create_FieldShouldReturnConflictIfAlreadyExists()
        {
            var field = new Field
            {
                CodField = "21321",
                Name = "Name",
                Installation = _installation1,
            };

            await _context.AddAsync(field);
            await _context.SaveChangesAsync();

            _createViewModel = new()
            {
                CodField = "21321",
                Name = "Name",
                InstallationId = _installation1.Id,
            };

            var response = await _controller.Create(_createViewModel);
            var conflictResult = (ConflictObjectResult)response;

            Assert.IsInstanceOf<ConflictObjectResult>(response);
            Assert.That(conflictResult.StatusCode, Is.EqualTo(409));
            Assert.That(((ErrorResponseDTO)conflictResult.Value!).Message, Is.EqualTo($"Field with code: {_createViewModel.CodField} already exists."));
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

            var response = await _controller.Create(_createViewModel);
            var notFoundResult = (NotFoundObjectResult)response;

            Assert.IsInstanceOf<NotFoundObjectResult>(response);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(((ErrorResponseDTO)notFoundResult.Value).Message, Is.EqualTo($"Installation not found"));
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

            var response = await _controller.Update(fieldToUpdate.Id, _updateViewModel);
            var updatedResult = (OkObjectResult)response;

            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.That(updatedResult.StatusCode, Is.EqualTo(200));
            Assert.That(((FieldDTO)updatedResult.Value).Name, Is.EqualTo(_updateViewModel.Name));

        }

        [Test]
        public async Task Update_FieldReturnsNotFoundWithInvalidField()
        {
            _updateViewModel = new()
            {
                InstallationId = _installation1.Id,
                Name = "UpdatedField",
            };

            var response = await _controller.Update(_invalidId, _updateViewModel);
            var notFoundResult = (NotFoundObjectResult)response;
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(((ErrorResponseDTO)notFoundResult.Value).Message, Is.EqualTo("Field not found"));

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

            var response = await _controller.Update(fieldToUpdate.Id, _updateViewModel);
            var notFoundResult = (NotFoundObjectResult)response;
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(((ErrorResponseDTO)notFoundResult.Value).Message, Is.EqualTo("Installation not found"));
        }

        [Test]
        public async Task Update_FieldAlsoCreateAHistoryOfTypeUpdateAndPersistsInDatabase()
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
                InstallationId = _installation2.Id,
                Name = "saoidjasdsa"
            };

            await _controller.Update(fieldToUpdate.Id, _updateViewModel);
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

            Assert.That(((FieldDTO)okResult.Value), Is.Not.Null);

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
