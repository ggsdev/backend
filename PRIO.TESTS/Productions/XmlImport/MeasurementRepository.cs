using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.TESTS.Productions.XmlImport
{
    [TestFixture]
    internal class MeasurementRepositoryTest
    {
        private DataContext _context;
        private IMeasurementRepository _measurementRepository;
        private User _user;
        private Installation _installation;
        private MeasuringPoint _measuringPoint;
        private MeasurementHistory _measurementHistory;
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

            _measurementRepository = new src.Modules.Measuring.Measurements.Infra.EF.Repositories.MeasurementRepository(_context);

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
        public async Task AddMeasurementAsync()
        {
            var measurement = new Measurement
            {
                Id = Guid.NewGuid(),
                User = _user,
                Installation = _installation,
                MeasuringPoint = _measuringPoint,
                Production = _production,
                FileType = _fileType,
                MeasurementHistory = _measurementHistory,
            };

            await _measurementRepository.AddAsync(measurement);

            var measurementInContext = _context.Measurements.Local.SingleOrDefault(m => m.Id == measurement.Id);
            Assert.That(measurementInContext, Is.Not.Null);
        }

        [Test]
        public async Task SaveMeasurementAsync()
        {
            var measurement = new Measurement
            {
                Id = Guid.NewGuid(),
                User = _user,
                Installation = _installation,
                MeasuringPoint = _measuringPoint,
                Production = _production,
                FileType = _fileType,
                MeasurementHistory = _measurementHistory,
            };

            await _context.Measurements.AddAsync(measurement);
            await _measurementRepository.SaveChangesAsync();

            var measurementInContext = _context.Measurements.Any(m => m.Id == measurement.Id);
            Assert.That(measurementInContext, Is.True);
        }

        [Test]
        public async Task GetAnyByDate_ExistingDateForFileType001_ReturnsTrue()
        {
            // Arrange
            DateTime date = new DateTime(2023, 8, 17);
            string fileType = "001";

            // Create a sample measurement with the desired date for fileType "001"
            var measurement = new Measurement
            {
                Id = Guid.NewGuid(),
                User = _user,
                Installation = _installation,
                MeasuringPoint = _measuringPoint,
                Production = _production,
                FileType = _fileType,
                MeasurementHistory = _measurementHistory,
                DHA_INICIO_PERIODO_MEDICAO_001 = date
            };
            await _context.Measurements.AddAsync(measurement);
            await _context.SaveChangesAsync();

            // Act
            bool result = await _measurementRepository.GetAnyByDate(date, fileType);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetAnyByDate_NonExistingDateForFileType001_ReturnsFalse()
        {
            // Arrange
            DateTime date = new DateTime(2023, 8, 18);
            string fileType = "001";

            var measurement = new Measurement
            {
                Id = Guid.NewGuid(),
                User = _user,
                Installation = _installation,
                MeasuringPoint = _measuringPoint,
                Production = _production,
                FileType = _fileType,
                MeasurementHistory = _measurementHistory,
                DHA_INICIO_PERIODO_MEDICAO_001 = date.AddDays(-1)
            };
            await _context.Measurements.AddAsync(measurement);
            await _context.SaveChangesAsync();

            // Act
            bool result = await _measurementRepository.GetAnyByDate(date, fileType);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetAnyAsync_ExistingId_ReturnsTrue()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Create a sample measurement with the desired Id
            var measurement = new Measurement
            {
                Id = id,
                User = _user,
                Installation = _installation,
                MeasuringPoint = _measuringPoint,
                Production = _production,
                FileType = _fileType,
                MeasurementHistory = _measurementHistory,
            };
            await _context.Measurements.AddAsync(measurement);
            await _context.SaveChangesAsync();

            // Act
            bool result = await _measurementRepository.GetAnyAsync(id);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetAnyAsync_NonExistingId_ReturnsFalse()
        {
            // Arrange
            Guid existingId = Guid.NewGuid();
            Guid idToCheck = Guid.NewGuid();

            // Create a sample measurement with a different Id
            var measurement = new Measurement
            {
                Id = existingId,
                User = _user,
                Installation = _installation,
                MeasuringPoint = _measuringPoint,
                Production = _production,
                FileType = _fileType,
                MeasurementHistory = _measurementHistory,
            };
            await _context.Measurements.AddAsync(measurement);
            await _context.SaveChangesAsync();

            // Act
            bool result = await _measurementRepository.GetAnyAsync(idToCheck);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
