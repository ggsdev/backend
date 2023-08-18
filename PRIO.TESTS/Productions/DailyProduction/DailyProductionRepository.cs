using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.TESTS.Productions.DailyProduction
{
    [TestFixture]
    internal class DailyProductionRepository
    {
        private DataContext _context;
        private IMeasurementRepository _measurementRepository;
        private User _user;
        private Installation _installation;
        private MeasuringPoint _measuringPoint;
        private MeasurementHistory _measurementHistory;
        private IProductionRepository _productionRepository;
        private Production _production;
        private FileType _fileType;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(contextOptions);

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
                Name = "testeClus",
                User = _user,
            };
            _context.Clusters.Add(cluster);

            _installation = new Installation()
            {
                Name = "testeInst",
                UepCod = "codmocked",
                CodInstallationAnp = "asdsadsads",
                UepName = "asdsadsadsa",
                User = _user,
                Cluster = cluster,
            };

            _productionRepository = new ProductionRepository(_context);

            _context.Installations.Add(_installation);

            _measuringPoint = new MeasuringPoint()
            {
                DinamicLocalMeasuringPoint = "Tramo a",
                Installation = _installation,
                Id = Guid.NewGuid(),
                TagPointMeasuring = "FIT-3502-B",

            };

            _context.MeasuringPoints.Add(_measuringPoint);

            _production = new Production()
            {
                Id = Guid.NewGuid(),
                Installation = _installation,
                CalculatedImportedAt = DateTime.UtcNow,
                CalculatedImportedBy = _user,
                MeasuredAt = DateTime.UtcNow,
                TotalProduction = 0m,
                StatusProduction = false
            };

            _fileType = new FileType()
            {
                Name = "001",
                Acronym = "PGL",
                Id = Guid.NewGuid(),
            };

            _context.FileTypes.Add(_fileType);

            _measurementHistory = new MeasurementHistory()
            {
                Id = Guid.NewGuid(),
                FileAcronym = _fileType.Acronym,
                FileName = "assadasdas.xml",
                FileType = _fileType.Name,
                FileContent = "asdasdasdasdabase64",
                ImportedAt = DateTime.UtcNow,
                ImportedBy = _user,
                MeasuredAt = DateTime.UtcNow,
                TypeOperation = "Import"

            };

            _context.MeasurementHistories.Add(_measurementHistory);

            _context.SaveChanges();

            _measurementRepository = new MeasurementRepository(_context);

            var httpContext = new DefaultHttpContext();
            httpContext.Items["Id"] = _user.Id;
            httpContext.Items["User"] = _user;

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddProduction()
        {
            var production = new Production
            {
                Id = Guid.NewGuid(),
                Installation = _installation,
                CalculatedImportedAt = DateTime.UtcNow,
                CalculatedImportedBy = _user,
                MeasuredAt = DateTime.UtcNow,
                StatusProduction = false,
                TotalProduction = 0m,
            };

            await _productionRepository.AddProduction(production);

            var productionInDatabase = _context.Productions.Local.Any();

            Assert.That(productionInDatabase, Is.True);
        }

        [Test]
        public async Task SaveProduction()
        {
            var production = new Production
            {
                Id = Guid.NewGuid(),
                Installation = _installation,
                CalculatedImportedAt = DateTime.UtcNow,
                CalculatedImportedBy = _user,
                MeasuredAt = DateTime.UtcNow,
                StatusProduction = false,
                TotalProduction = 0m,
            };

            await _productionRepository.AddProduction(production);
            await _productionRepository.SaveChangesAsync();

            var productionInDatabase = await _context.Productions.AnyAsync();

            Assert.That(productionInDatabase, Is.True);
        }

        [Test]
        public async Task GetProductionGasByDate_ExistingDate_ReturnsProduction()
        {
            // Arrange
            DateTime date = new DateTime(2023, 8, 17);

            // Create a sample production with the desired date
            var production = new Production
            {
                Id = Guid.NewGuid(),
                Installation = _installation,
                MeasuredAt = date,
            };

            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productionRepository.GetProductionGasByDate(date);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.MeasuredAt.Year, Is.EqualTo(date.Year));
            Assert.That(result.MeasuredAt.Month, Is.EqualTo(date.Month));
            Assert.That(result.MeasuredAt.Day, Is.EqualTo(date.Day));
        }

        [Test]
        public async Task GetProductionGasByDate_NonExistingDate_ReturnsNull()
        {
            // Arrange
            DateTime existingDate = new DateTime(2023, 8, 17);
            DateTime dateToCheck = new DateTime(2023, 8, 18);

            // Create a sample production with a different date
            var production = new Production
            {
                Id = Guid.NewGuid(),
                Installation = _installation,
                MeasuredAt = existingDate,
            };

            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productionRepository.GetProductionGasByDate(dateToCheck);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task AnyByDate_ExistingDate_ReturnsTrue()
        {
            // Arrange
            DateTime date = new DateTime(2023, 8, 17);

            // Create a sample production with the desired date
            var production = new Production
            {
                Id = Guid.NewGuid(),
                Installation = _installation,
                MeasuredAt = date
            };
            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            // Act
            bool result = await _productionRepository.AnyByDate(date);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task AnyByDate_NonExistingDate_ReturnsFalse()
        {
            // Arrange
            DateTime existingDate = new DateTime(2023, 8, 17);
            DateTime dateToCheck = new DateTime(2023, 8, 18);

            // Create a sample production with a different date
            var production = new Production
            {
                Id = Guid.NewGuid(),
                Installation = _installation,
                MeasuredAt = existingDate
            };
            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            // Act
            bool result = await _productionRepository.AnyByDate(dateToCheck);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetExistingByDate_ExistingDate_ReturnsProductionWithAllIncludedEntities()
        {
            // Arrange
            DateTime date = new DateTime(2023, 8, 17);

            var production = new Production
            {
                Id = Guid.NewGuid(),
                Installation = _installation,
                MeasuredAt = date,
                GasLinear = new GasLinear(),
                GasDiferencial = new GasDiferencial(),
                Gas = new Gas(),
                Oil = new Oil(),
                FieldsFR = new List<FieldFR>
                {
                new FieldFR
                {
                    Field = new Field()
                    {
                        Name = "RandomName",
                        CodField = "RandomCod",
                        State = "RandomState",
                        Basin = "RandomBasin",
                        Location = "RandomLocation",
                    }
                }
            },
                Measurements = new List<Measurement>
            {
                new Measurement
                {
                     Id = Guid.NewGuid(),
                        User = _user,
                        Installation = _installation,
                        MeasuringPoint = _measuringPoint,
                        FileType = _fileType,
                        MeasurementHistory = _measurementHistory,
                }
            }
            };

            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productionRepository.GetExistingByDate(date);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Installation);
            Assert.IsNotNull(result.GasLinear);
            Assert.IsNotNull(result.GasDiferencial);
            Assert.IsNotNull(result.Gas);
            Assert.IsNotNull(result.Oil);
            Assert.IsNotNull(result.FieldsFR);
            Assert.IsNotNull(result.Measurements);
        }

        [Test]
        public async Task GetExistingByDate_NonExistingDate_ReturnsNull()
        {
            // Arrange
            DateTime existingDate = new DateTime(2023, 8, 17);
            DateTime dateToCheck = new DateTime(2023, 8, 18);

            // Create a sample production with the existing date
            var production = new Production
            {
                Id = Guid.NewGuid(),
                Installation = _installation,
                MeasuredAt = existingDate
            };
            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productionRepository.GetExistingByDate(dateToCheck);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetById_ExistingId_ReturnsProductionWithIncludedEntities()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Create a sample production with associated entities
            var production = new Production
            {
                Id = id,
                Installation = _installation,
                Measurements = new List<Measurement>
            {
                new Measurement
                {
                    MeasurementHistory = new MeasurementHistory()
                }
            }
            };
            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productionRepository.GetById(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Measurements);
            // Assert other included entities and their properties as needed
        }

        [Test]
        public async Task GetById_NonExistingId_ReturnsNull()
        {
            // Arrange
            Guid existingId = Guid.NewGuid();
            Guid idToCheck = Guid.NewGuid();

            // Create a sample production with the existing Id
            var production = new Production
            {
                Id = existingId,
                Installation = _installation
            };
            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productionRepository.GetById(idToCheck);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllProductions_ReturnsListOfProductionsWithIncludedEntities()
        {
            // Arrange
            var production = new Production
            {
                Id = Guid.NewGuid(),
                Installation = _installation,
                MeasuredAt = DateTime.UtcNow,
                GasLinear = new GasLinear(),
                GasDiferencial = new GasDiferencial(),
                Gas = new Gas(),
                Oil = new Oil(),
                FieldsFR = new List<FieldFR>
            {
                new FieldFR
                {
                    Field = new Field()
                    {
                        Name = "RandomName",
                        CodField = "RandomCod",
                        State = "RandomState",
                        Basin = "RandomBasin",
                        Location = "RandomLocation",
                    }
                }
            },
                Measurements = new List<Measurement>
            {
                new Measurement
                {
                    Id = Guid.NewGuid(),
                    User = _user,
                    Installation = _installation,
                    MeasuringPoint = _measuringPoint,
                    FileType = _fileType,
                    MeasurementHistory = _measurementHistory,
                }
            }
            };

            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            // Act
            List<Production> result = await _productionRepository.GetAllProductions();

            // Assert
            Assert.AreEqual(1, result.Count);

            Production retrievedProduction = result[0];
            Assert.IsNotNull(retrievedProduction);
            Assert.IsNotNull(retrievedProduction.Installation);
            Assert.IsNotNull(retrievedProduction.GasLinear);
            Assert.IsNotNull(retrievedProduction.GasDiferencial);
            Assert.IsNotNull(retrievedProduction.Gas);
            Assert.IsNotNull(retrievedProduction.Oil);

            Assert.AreEqual(1, retrievedProduction.FieldsFR.Count);
            FieldFR retrievedFieldFR = retrievedProduction.FieldsFR[0];
            Assert.IsNotNull(retrievedFieldFR.Field);
            Assert.AreEqual("RandomName", retrievedFieldFR.Field.Name);
            Assert.AreEqual("RandomCod", retrievedFieldFR.Field.CodField);
            Assert.AreEqual("RandomState", retrievedFieldFR.Field.State);
            Assert.AreEqual("RandomBasin", retrievedFieldFR.Field.Basin);
            Assert.AreEqual("RandomLocation", retrievedFieldFR.Field.Location);

            Assert.AreEqual(1, retrievedProduction.Measurements.Count);
            Measurement retrievedMeasurement = retrievedProduction.Measurements[0];
            Assert.IsNotNull(retrievedMeasurement.MeasuringPoint);
            Assert.IsNotNull(retrievedMeasurement.MeasurementHistory);

            Assert.AreEqual(_installation, retrievedProduction.Installation);
            Assert.AreEqual(DateTime.UtcNow.Date, retrievedProduction.MeasuredAt.Date);
            // ... assert other properties and included entities
        }

        [Test]
        public async Task AddOrUpdateProduction_AddNewProduction()
        {
            var production = new Production
            {
                Id = Guid.NewGuid(),
                Installation = _installation,
                MeasuredAt = DateTime.UtcNow.Date, // Use only the date component
                StatusProduction = true,
                TotalProduction = 100.0m,
            };

            // Act
            await _productionRepository.AddOrUpdateProduction(production);
            await _context.SaveChangesAsync();
            // Assert
            var addedProduction = await _context.Productions.SingleOrDefaultAsync();

            Assert.IsNotNull(addedProduction);
            Assert.That(addedProduction.Id, Is.EqualTo(production.Id));
            Assert.That(addedProduction.StatusProduction, Is.EqualTo(production.StatusProduction));
            Assert.That(addedProduction.TotalProduction, Is.EqualTo(production.TotalProduction));
        }

        [Test]
        public async Task AddOrUpdateProduction_UpdateExistingProduction()
        {
            // Arrange
            var existingProduction = new Production
            {
                Id = Guid.NewGuid(),
                Installation = _installation,
                MeasuredAt = DateTime.UtcNow,
                StatusProduction = false,
                TotalProduction = 50.0m
            };

            _context.Productions.Add(existingProduction);
            await _context.SaveChangesAsync();

            var updatedProduction = new Production
            {
                Id = existingProduction.Id,
                Installation = _installation,
                MeasuredAt = existingProduction.MeasuredAt,
                StatusProduction = true,
                TotalProduction = 75.0m
            };

            // Act
            await _productionRepository.AddOrUpdateProduction(updatedProduction);

            // Assert
            var updatedProductionInDb = await _context.Productions.SingleOrDefaultAsync();
            Assert.IsNotNull(updatedProductionInDb);
            Assert.That(updatedProductionInDb.Id, Is.EqualTo(existingProduction.Id));
            Assert.That(updatedProductionInDb.StatusProduction, Is.EqualTo(updatedProduction.StatusProduction));
            Assert.That(updatedProductionInDb.TotalProduction, Is.EqualTo(updatedProduction.TotalProduction));
        }
    }


}
