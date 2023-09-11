using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Repositories;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Repositories;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Dtos;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.Productions.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Utils;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.TESTS.Productions.DailyProduction
{
    [TestFixture]
    internal class DailyProductionServiceTest
    {
        private ProductionService _service;

        private DataContext _context;
        private User _user;
        private Installation _installation;
        private MeasuringPoint _measuringPoint;
        private IProductionRepository _productionRepository;
        private Production _production;
        private FileType _fileType;
        private IGasVolumeCalculationRepository _gasRepository;
        private IOilVolumeCalculationRepository _oilRepository;
        private IMeasurementHistoryRepository _measurementHistoryRepository;
        private IInstallationRepository _installationRepository;
        private IWellRepository _wellRepository;
        private IFieldRepository _fieldRepository;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(contextOptions);
            var mapperConfig = new MapperConfiguration(cfg =>
            {

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
                StatusProduction = "aberto"
            };

            _fileType = new FileType()
            {
                Name = "001",
                Acronym = "PGL",
                Id = Guid.NewGuid(),
            };

            _context.FileTypes.Add(_fileType);

            _context.SaveChanges();

            _productionRepository = new ProductionRepository(_context);
            _gasRepository = new GasVolumeCalculationRepository(_context);
            _installationRepository = new InstallationRepository(_context);
            _oilRepository = new OilVolumeCalculationRepository(_context);
            _wellRepository = new WellRepository(_context);
            _measurementHistoryRepository = new MeasurementHistoryRepository(_context);
            _fieldRepository = new FieldRepository(_context);

            _service = new ProductionService(_productionRepository, _mapper, _gasRepository, _installationRepository, _oilRepository, _measurementHistoryRepository, _fieldRepository, _wellRepository);

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
        public async Task GetProductionByInvalidDate_ReturnsNotFoundException()
        {
            var invalidDate = DateTime.UtcNow.Date.AddDays(-5);

            try
            {
                var result = await _service.GetByDate(invalidDate);
                Assert.Fail("Expected NotFoundException was not thrown.");
            }
            catch (NotFoundException)
            {

                // Expected exception, do nothing
            }
        }

        [Test]
        public async Task GetProductionByDate_ReturnsProduction()
        {
            var productionId = Guid.NewGuid();
            var productionDate = DateTime.UtcNow.Date;

            var production = new Production
            {
                Id = productionId,
                Installation = _installation,
                MeasuredAt = productionDate,
                StatusProduction = "fechado",
                CalculatedImportedBy = _user,
                CalculatedImportedAt = DateTime.UtcNow,
                TotalProduction = 20m,

            };

            _context.Productions.Add(production);
            await _context.SaveChangesAsync();


            var result = await _service.GetByDate(productionDate);


            var productionInDb = await _context.Productions
                .FirstOrDefaultAsync(x => x.Id == productionId);

            Assert.That(result, Is.Not.Null);

            Assert.That(_context.Productions.Count() == 1);

            Assert.That(productionInDb, Is.Not.Null);
            Assert.That(productionInDb.Installation.Id, Is.EqualTo(production.Installation.Id));
            Assert.That(productionInDb.MeasuredAt, Is.EqualTo(production.MeasuredAt));
            Assert.That(productionInDb.StatusProduction, Is.EqualTo(production.StatusProduction));
            Assert.That(productionInDb.Id, Is.EqualTo(production.Id));
            Assert.That(productionInDb.TotalProduction, Is.EqualTo(production.TotalProduction));
            Assert.That(productionInDb.CalculatedImportedBy.Name, Is.EqualTo(production.CalculatedImportedBy.Name));
        }

        [Test]
        public async Task GetProductionByDate_ReturnsWithGasAndOilWhenHasIt()
        {
            var productionId = Guid.NewGuid();
            var productionDate = DateTime.UtcNow.Date;

            var gasLinear = new GasLinear
            {
                Id = Guid.NewGuid(),
                TotalGas = 2m,
                StatusGas = true,
                BurntGas = 2m
            };

            var gasDiferencial = new GasDiferencial
            {
                Id = Guid.NewGuid(),
                TotalGas = 2m,
                StatusGas = true,
                FuelGas = 3m
            };

            var gas = new Gas
            {
                Id = Guid.NewGuid(),
                GasDiferencial = gasDiferencial,
                GasLinear = gasLinear,

            };

            var oil = new Oil
            {
                Id = Guid.NewGuid(),
                TotalOil = 15m
            };

            var production = new Production
            {
                Id = productionId,
                Installation = _installation,
                MeasuredAt = productionDate,
                StatusProduction = "fechado",
                CalculatedImportedBy = _user,
                CalculatedImportedAt = DateTime.UtcNow,
                TotalProduction = 20m,
                Gas = gas,
                GasLinear = gasLinear,
                GasDiferencial = gasDiferencial,
                Oil = oil,
            };

            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            var result = await _service.GetByDate(productionDate);

            var productionInDb = await _context.Productions
                .Include(x => x.Gas)
                .Include(x => x.GasLinear)
                .Include(x => x.GasDiferencial)
                .Include(x => x.Oil)
                .FirstOrDefaultAsync(x => x.Id == productionId);

            Assert.That(result, Is.Not.Null);

            Assert.That(_context.Productions.Count() == 1);

            Assert.That(productionInDb, Is.Not.Null);
            Assert.That(productionInDb.Gas.Id, Is.EqualTo(production.Gas.Id));
            Assert.That(productionInDb.Oil.Id, Is.EqualTo(production.Oil.Id));
            Assert.That(productionInDb.GasLinear.Id, Is.EqualTo(production.GasLinear.Id));
            Assert.That(productionInDb.GasDiferencial.Id, Is.EqualTo(production.GasDiferencial.Id));
        }

        [Test]
        public async Task GetAllProductions_ReturnsListOfProductionsDto()
        {
            var gasDiferencial = new GasDiferencial { TotalGas = 50.0m };
            var gasLinear = new GasLinear { TotalGas = 100.0m };
            var oil = new Oil { TotalOil = 25.0m };

            var production = new Production
            {
                Id = Guid.NewGuid(),
                Installation = _installation,
                MeasuredAt = DateTime.UtcNow,
                StatusProduction = "fechado",
                GasDiferencial = gasDiferencial,
                GasLinear = gasLinear,
                Oil = oil,
            };

            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAllProductions();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));

            GetAllProductionsDto retrievedDto = result[0];
            Assert.That(retrievedDto.Id, Is.EqualTo(production.Id));

            decimal expectedGasTotalBBL = Math.Round((gasDiferencial.TotalGas * ProductionUtils.m3ToBBLConversionMultiplier) + (gasLinear.TotalGas * ProductionUtils.m3ToBBLConversionMultiplier), 2);
            decimal expectedGasTotalM3 = Math.Round(gasDiferencial.TotalGas + gasLinear.TotalGas, 2);

            decimal expectedOilTotalBBL = Math.Round(oil.TotalOil * ProductionUtils.m3ToBBLConversionMultiplier, 2);
            decimal expectedOilTotalM3 = Math.Round(oil.TotalOil, 2);

            Assert.That(retrievedDto.Gas.TotalGasSCF, Is.EqualTo(expectedGasTotalBBL));
            Assert.That(retrievedDto.Gas.TotalGasM3, Is.EqualTo(expectedGasTotalM3));

            Assert.That(retrievedDto.Oil.TotalOilBBL, Is.EqualTo(expectedOilTotalBBL));
            Assert.That(retrievedDto.Oil.TotalOilM3, Is.EqualTo(expectedOilTotalM3));
        }

        [Test]
        public async Task DownloadAllProductionFiles_ValidProductionId_ReturnsListOfFilesWithBase64()
        {
            var productionId = Guid.NewGuid();
            var productionDate = DateTime.UtcNow.Date; // Set to the current date without the time

            var fileHistory = new MeasurementHistory
            {
                Id = Guid.NewGuid(),
                FileName = "test.xml",
                FileType = "001",
                ImportedAt = productionDate,
                FileContent = "base64content",
                MeasuredAt = productionDate,
            };

            _context.MeasurementHistories.Add(fileHistory);

            var production = new Production
            {
                Id = productionId,
                Installation = _installation,
                MeasuredAt = productionDate
            };

            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.DownloadAllProductionFiles(productionId);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));

            ProductionFilesDtoWithBase64 retrievedDto = result[0];
            Assert.That(retrievedDto.FileId, Is.EqualTo(fileHistory.Id));
            Assert.That(retrievedDto.FileName, Is.EqualTo(fileHistory.FileName));
            Assert.That(retrievedDto.FileType, Is.EqualTo(fileHistory.FileType));
            Assert.That(retrievedDto.ImportedAt, Is.EqualTo(fileHistory.ImportedAt));
            Assert.That(retrievedDto.Base64, Is.EqualTo(fileHistory.FileContent));
        }

        [Test]
        public async Task DownloadAllProductionFiles_InvalidProductionId_ReturnsNotFound()
        {
            var invalidId = Guid.NewGuid();
            // Act
            try
            {
                var result = await _service.DownloadAllProductionFiles(invalidId);
                Assert.Fail("Expected NotFoundException was not thrown.");

            }
            catch (NotFoundException)
            {

                // Expected exception, do nothing
            }
        }
    }
}
