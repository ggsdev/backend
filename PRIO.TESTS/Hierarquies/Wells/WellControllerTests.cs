using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Controllers;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.HierarchyDTOS.WellDTOS;
using PRIO.DTOS.HistoryDTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.Exceptions;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.Services.HierarchyServices;
using PRIO.ViewModels.HierarchyViewModels.Wells;
using System.ComponentModel.DataAnnotations;

namespace PRIO.TESTS.Hierarquies.Wells
{
    [TestFixture]
    internal class WellControllerTests
    {
        private WellController _controller;
        private IMapper _mapper;
        private DataContext _context;
        private User _user;
        private Field _field1;
        private Field _field2;
        private CreateWellViewModel _createViewModel;
        private WellService _service;
        private Guid _invalidId = Guid.NewGuid();

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
                cfg.CreateMap<Well, WellDTO>();
                cfg.CreateMap<Well, CreateUpdateWellDTO>();
                cfg.CreateMap<Well, WellHistoryDTO>();
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

            var cluster = new Cluster
            {
                Name = "2123asd13",
                User = _user,
            };
            _context.Clusters.Add(cluster);

            var installation = new Installation()
            {
                Name = "testeInst",
                CodInstallationUep = "codmocked",
                User = _user,
                Cluster = cluster,
            };
            _context.Installations.Add(installation);

            _field1 = new Field()
            {
                Installation = installation,
                Name = "namdse",
                CodField = "co123d",
                User = _user
            };
            _context.Fields.Add(_field1);

            _field2 = new Field()
            {
                Installation = installation,
                Name = "namse",
                CodField = "cas123dod",
                User = _user

            };
            _context.Fields.Add(_field2);


            _createViewModel = new()
            {
                Name = "1233a3",
                WellOperatorName = "1233aa3",
                CodWellAnp = "1233aa3",
                CategoryAnp = "1233a3",
                CategoryReclassificationAnp = "1233a3",
                CategoryOperator = "1233a3",
                StatusOperator = true,
                Type = "1233a3",
                WaterDepth = 322.52m,
                TopOfPerforated = 2.5m,
                BaseOfPerforated = 2.5m,
                ArtificialLift = "1233a3",
                Latitude4C = "22:03:34,054",
                Longitude4C = "22:03:34,054",
                LatitudeDD = "-22,0594594444",
                LongitudeDD = "39,8311675000",
                DatumHorizontal = "1233a3",
                TypeBaseCoordinate = "1233a3",
                CoordX = "-39,7706275000",
                CoordY = "-22,1108369444",
                FieldId = _field2.Id,
                CodWell = "sadsada"
            };
            _context.SaveChanges();

            var httpContext = new DefaultHttpContext();
            httpContext.Items["Id"] = _user.Id;
            httpContext.Items["User"] = _user;

            _service = new WellService(_context, _mapper);
            _controller = new WellController(_service);
            _controller.ControllerContext.HttpContext = httpContext;
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Create_WellReturnsACreatedStatusWithDTO()
        {
            var response = await _controller.Create(_createViewModel);
            var createdResult = (CreatedResult)response;

            Assert.IsInstanceOf<CreatedResult>(response);
            Assert.That(((CreateUpdateWellDTO)createdResult.Value).Name, Is.EqualTo(_createViewModel.Name));
            Assert.That(((CreateUpdateWellDTO)createdResult.Value).CodWellAnp, Is.EqualTo(_createViewModel.CodWellAnp));
            Assert.That(createdResult.Location, Is.EqualTo($"wells/{((CreateUpdateWellDTO)createdResult.Value).Id}"));
        }

        [Test]
        public async Task Create_WellShouldReturnNotFoundIfFieldDoesntExists()
        {
            _createViewModel = new()
            {
                Name = "1233a3",
                WellOperatorName = "1233aa3",
                CodWellAnp = "1233aa3",
                CategoryAnp = "1233a3",
                CategoryReclassificationAnp = "1233a3",
                CategoryOperator = "1233a3",
                StatusOperator = true,
                Type = "1233a3",
                WaterDepth = 322.52m,
                TopOfPerforated = 2.5m,
                BaseOfPerforated = 2.5m,
                ArtificialLift = "1233a3",
                Latitude4C = "22:03:34,054",
                Longitude4C = "22:03:34,054",
                LatitudeDD = "-22,0594594444",
                LongitudeDD = "39,8311675000",
                DatumHorizontal = "1233a3",
                TypeBaseCoordinate = "1233a3",
                CoordX = "-39,7706275000",
                CoordY = "-22,1108369444",
                CodWell = "sadsada",
                FieldId = _invalidId
            };
            try
            {
                var response = await _controller.Create(_createViewModel);

                Assert.Fail("Expected NotFoundException was not thrown.");
            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Field not found"));

            }
        }

        [Test]
        public void Create_ShouldNotCreateWellIfBodyIsInvalid()
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_createViewModel, null, null);
            var isValid = Validator.TryValidateObject(_createViewModel, validationContext, validationResults, true);

            if (!isValid)
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Message = "ReservoirId is required."
                };

                var badRequestResult = new BadRequestObjectResult(errorResponse);
                Assert.IsInstanceOf<BadRequestObjectResult>(badRequestResult);

                Assert.That(((ErrorResponseDTO)badRequestResult.Value).Message, Is.EqualTo(errorResponse.Message));
                Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            }

        }

        [Test]
        public async Task Create_AlsoCreateAHistoryOfTypeCreateAndPersistsInDatabase()
        {

            await _controller.Create(_createViewModel);
            var well = await _context.Wells.SingleOrDefaultAsync();


            Assert.That(well, Is.Not.Null);
            Assert.That(well.User, Is.Not.Null);
            Assert.That(well.Name, Is.EqualTo(_createViewModel.Name));
            Assert.That(well.CodWellAnp, Is.EqualTo(_createViewModel.CodWellAnp));
            Assert.That(well.WellOperatorName, Is.EqualTo(_createViewModel.WellOperatorName));
            Assert.That(well.CategoryAnp, Is.EqualTo(_createViewModel.CategoryAnp));
            Assert.That(well.CategoryReclassificationAnp, Is.EqualTo(_createViewModel.CategoryReclassificationAnp));
            Assert.That(well.CategoryOperator, Is.EqualTo(_createViewModel.CategoryOperator));
            Assert.That(well.StatusOperator, Is.EqualTo(_createViewModel.StatusOperator));
            Assert.That(well.Type, Is.EqualTo(_createViewModel.Type));
            Assert.That(well.WaterDepth, Is.EqualTo(_createViewModel.WaterDepth));
            Assert.That(well.TopOfPerforated, Is.EqualTo(_createViewModel.TopOfPerforated));
            Assert.That(well.BaseOfPerforated, Is.EqualTo(_createViewModel.BaseOfPerforated));
            Assert.That(well.ArtificialLift, Is.EqualTo(_createViewModel.ArtificialLift));
            Assert.That(well.Latitude4C, Is.EqualTo(_createViewModel.Latitude4C));
            Assert.That(well.Longitude4C, Is.EqualTo(_createViewModel.Longitude4C));
            Assert.That(well.LatitudeDD, Is.EqualTo(_createViewModel.LatitudeDD));
            Assert.That(well.LongitudeDD, Is.EqualTo(_createViewModel.LongitudeDD));
            Assert.That(well.DatumHorizontal, Is.EqualTo(_createViewModel.DatumHorizontal));
            Assert.That(well.TypeBaseCoordinate, Is.EqualTo(_createViewModel.TypeBaseCoordinate));
            Assert.That(well.CoordX, Is.EqualTo(_createViewModel.CoordX));
            Assert.That(well.CoordY, Is.EqualTo(_createViewModel.CoordY));

            //var history = await _context.WellHistories.SingleOrDefaultAsync();
            //Assert.That(history, Is.Not.Null);
            //Assert.That(history.User, Is.Not.Null);
            //Assert.That(history.Name, Is.EqualTo(well.Name));
            //Assert.That(history.CodWellAnp, Is.EqualTo(well.CodWellAnp));
            //Assert.That(well.WellOperatorName, Is.EqualTo(history.WellOperatorName));
            //Assert.That(well.CategoryAnp, Is.EqualTo(history.CategoryAnp));
            //Assert.That(well.CategoryReclassificationAnp, Is.EqualTo(history.CategoryReclassificationAnp));
            //Assert.That(well.CategoryOperator, Is.EqualTo(history.CategoryOperator));
            //Assert.That(well.StatusOperator, Is.EqualTo(history.StatusOperator));
            //Assert.That(well.Type, Is.EqualTo(history.Type));
            //Assert.That(well.WaterDepth, Is.EqualTo(history.WaterDepth));
            //Assert.That(well.TopOfPerforated, Is.EqualTo(history.TopOfPerforated));
            //Assert.That(well.BaseOfPerforated, Is.EqualTo(history.BaseOfPerforated));
            //Assert.That(well.ArtificialLift, Is.EqualTo(history.ArtificialLift));
            //Assert.That(well.Latitude4C, Is.EqualTo(history.Latitude4C));
            //Assert.That(well.Longitude4C, Is.EqualTo(history.Longitude4C));
            //Assert.That(well.LatitudeDD, Is.EqualTo(history.LatitudeDD));
            //Assert.That(well.LongitudeDD, Is.EqualTo(history.LongitudeDD));
            //Assert.That(well.DatumHorizontal, Is.EqualTo(history.DatumHorizontal));
            //Assert.That(well.TypeBaseCoordinate, Is.EqualTo(history.TypeBaseCoordinate));
            //Assert.That(well.CoordX, Is.EqualTo(history.CoordX));
            //Assert.That(well.CoordY, Is.EqualTo(history.CoordY));
            //Assert.That(history.TypeOperation, Is.EqualTo(Utils.TypeOperation.Create));

        }

        [Test]
        public async Task Update_ReturnsOkStatusWithDTO()
        {
            var updateViewModel = new UpdateWellViewModel()
            {
                Name = "sdasa",
                CodWellAnp = "2312saa",
                FieldId = _field2.Id
            };

            var wellToUpdate = new Well()
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
                TopOfPerforated = 2.5m,
                BaseOfPerforated = 2.5m,
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
            await _context.AddAsync(wellToUpdate);
            await _context.SaveChangesAsync();

            var response = await _controller.Update(wellToUpdate.Id, updateViewModel);
            var updatedResult = (OkObjectResult)response;

            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.That(((CreateUpdateWellDTO)updatedResult.Value).Name, Is.EqualTo(wellToUpdate.Name));
            Assert.That(((CreateUpdateWellDTO)updatedResult.Value).CodWellAnp, Is.EqualTo(wellToUpdate.CodWellAnp));
        }

        [Test]
        public async Task Update_ReturnsNotFoundWithInvalidWellId()
        {

            var updateViewModel = new UpdateWellViewModel()
            {
                Name = "sadsadsa"
            };
            try
            {
                var response = await _controller.Update(_invalidId, updateViewModel);

                Assert.Fail("Expected NotFoundException was not thrown.");
            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Well not found"));

            }
        }

        [Test]
        public async Task Update_ReturnsNotFoundWithInvalidFieldId()
        {
            var wellToUpdate = new Well()
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
                TopOfPerforated = 2.5m,
                BaseOfPerforated = 2.5m,
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
            await _context.AddAsync(wellToUpdate);
            await _context.SaveChangesAsync();

            var updateViewModel = new UpdateWellViewModel()
            {
                FieldId = _invalidId,
            };
            try
            {
                var response = await _controller.Update(wellToUpdate.Id, updateViewModel);

                Assert.Fail("Expected NotFoundException was not thrown.");
            }
            catch (NotFoundException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Field not found"));

            }
        }

        [Test]
        public async Task Update_AlsoCreateAHistoryOfTypeUpdateAndPersistsInDatabase()
        {
            var wellToUpdate = new Well()
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
                TopOfPerforated = 2.5m,
                BaseOfPerforated = 2.5m,
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
            await _context.AddAsync(wellToUpdate);
            await _context.SaveChangesAsync();
            var updateViewModel = new UpdateWellViewModel()
            {
                Name = "sadsadsa",
                FieldId = _field2.Id
            };
            var beforeUpdate = new UpdateWellViewModel
            {
                Name = wellToUpdate.Name,
                FieldId = wellToUpdate.Field?.Id,
            };

            await _controller.Update(wellToUpdate.Id, updateViewModel);

            var well = await _context.Wells.SingleOrDefaultAsync();

            Assert.That(well, Is.Not.Null);
            Assert.That(well.User, Is.Not.Null);
            Assert.That(well.Field, Is.Not.Null);
            Assert.That(well.Field.Id, Is.EqualTo(updateViewModel.FieldId));
            Assert.That(well.Name, Is.EqualTo(updateViewModel.Name));

            //var history = await _context.WellHistories.SingleOrDefaultAsync();
            //Assert.That(history, Is.Not.Null);
            //Assert.That(history.Well, Is.Not.Null);
            //Assert.That(history.Field, Is.Not.Null);
            //Assert.That(history.User, Is.Not.Null);

            //Assert.That(history.FieldOld, Is.EqualTo(beforeUpdate.FieldId));
            //Assert.That(history.Name, Is.EqualTo(well.Name));
            //Assert.That(history.Field.Name, Is.EqualTo(well.Field.Name));
            //Assert.That(history.NameOld, Is.EqualTo(beforeUpdate.Name));
            //Assert.That(history.TypeOperation, Is.EqualTo(Utils.TypeOperation.Update));
        }

        [Test]
        public async Task Delete_SuccesfullySoftDeletesAWell()
        {
            var wellToUpdate = new Well()
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
                TopOfPerforated = 2.5m,
                BaseOfPerforated = 2.5m,
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
                IsActive = true,
            };
            await _context.AddAsync(wellToUpdate);
            await _context.SaveChangesAsync();

            var response = await _controller.Delete(wellToUpdate.Id);

            var wellInDatabase = await _context.Wells.SingleOrDefaultAsync();

            Assert.IsInstanceOf<NoContentResult>(response);
            Assert.That(wellInDatabase, Is.Not.Null);
            Assert.That(wellInDatabase.IsActive, Is.False);
            Assert.That(wellInDatabase.DeletedAt, Is.Not.Null);
            //var historyInDatabase = await _context.WellHistories.SingleOrDefaultAsync();
            //Assert.That(historyInDatabase, Is.Not.Null);
            //Assert.That(historyInDatabase.IsActiveOld, Is.True);
            //Assert.That(historyInDatabase.TypeOperation, Is.EqualTo(Utils.TypeOperation.Delete));
        }

        [Test]
        public async Task Restore_SuccesfullyRestoresAWell()
        {
            var wellToUpdate = new Well()
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
                TopOfPerforated = 2.5m,
                BaseOfPerforated = 2.5m,
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
                IsActive = false,
            };
            await _context.AddAsync(wellToUpdate);
            await _context.SaveChangesAsync();

            var response = await _controller.Restore(wellToUpdate.Id);
            var wellInDatabase = await _context.Wells.SingleOrDefaultAsync();
            var okResult = (OkObjectResult)response;

            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.That(((CreateUpdateWellDTO)okResult.Value), Is.Not.Null);
            Assert.That(wellInDatabase, Is.Not.Null);
            Assert.That(wellInDatabase.IsActive, Is.True);
            Assert.That(wellInDatabase.DeletedAt, Is.Null);

            //var historyInDatabase = await _context.WellHistories.SingleOrDefaultAsync();
            //Assert.That(historyInDatabase, Is.Not.Null);
            //Assert.That(historyInDatabase.IsActive, Is.True);
            //Assert.That(historyInDatabase.IsActiveOld, Is.False);
            //Assert.That(historyInDatabase.TypeOperation, Is.EqualTo(Utils.TypeOperation.Restore));
        }
    }
}