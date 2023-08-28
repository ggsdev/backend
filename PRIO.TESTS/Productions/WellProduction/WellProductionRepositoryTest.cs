using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.WellProductions.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.TESTS.Productions.WellProduction
{
    [TestFixture]
    internal class WellProductionRepositoryTest
    {
        private DataContext _context;
        private IWellProductionRepository _wellProductionRepository;
        private User _user;
        private Installation _installation;
        private MeasuringPoint _measuringPoint;
        private MeasurementHistory _measurementHistory;
        private IProductionRepository _productionRepository;
        private Production _production;
        private FileType _fileType;
        private Field _field;

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

            _field = new Field()
            {
                Installation = _installation,
                Name = "namdse",
                CodField = "co123d",
                User = _user
            };

            _context.Fields.Add(_field);

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

            _wellProductionRepository = new WellProductionRepository(_context);

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
        public async Task GetAllFieldsProductionsByProductionId_ReturnsAListOfFieldProduction()
        {
            var fieldProduction = new FieldProduction
            {
                Id = Guid.NewGuid(),
                GasProductionInField = 1200,
                WaterProductionInField = 1110,
                OilProductionInField = 120,
                ProductionId = _production.Id,
                FieldId = _field.Id,
            };

            _context.FieldsProductions.Add(fieldProduction);
            _context.SaveChanges();

            await _productionRepository.GetAllFieldProductionByProduction(_production.Id);

            var fieldProductions = _context.FieldsProductions.Local.ToList();

            Assert.That(fieldProductions.Count, Is.GreaterThan(0));
            Assert.That(fieldProductions[0].Id, Is.EqualTo(fieldProduction.Id));
            Assert.That(fieldProductions[0].FieldId, Is.EqualTo(_field.Id));
            Assert.That(fieldProductions[0].ProductionId, Is.EqualTo(_production.Id));

        }

        //[Test]
        //public async Task GetCompletionProduction_ReturnsACompletionProduction()
        //{

        //    var completionProduction = new CompletionProduction
        //    {
        //        Id = Guid.NewGuid(),
        //        ProductionId = _production.Id,

        //    };

        //    _context.FieldsProductions.Add(completionProduction);

        //    var fieldProduction = new FieldProduction
        //    {
        //        Id = Guid.NewGuid(),
        //        GasProductionInField = 1200,
        //        WaterProductionInField = 1110,
        //        OilProductionInField = 120,
        //        ProductionId = _production.Id,
        //        FieldId = _field.Id,
        //    };

        //    _context.FieldsProductions.Add(fieldProduction);
        //    _context.SaveChanges();

        //    await _productionRepository.GetAllFieldProductionByProduction(_production.Id);

        //    var fieldProductions = _context.FieldsProductions.Local.ToList();

        //    Assert.That(fieldProductions.Count, Is.GreaterThan(0));
        //    Assert.That(fieldProductions[0].Id, Is.EqualTo(fieldProduction.Id));
        //    Assert.That(fieldProductions[0].FieldId, Is.EqualTo(_field.Id));
        //    Assert.That(fieldProductions[0].ProductionId, Is.EqualTo(_production.Id));

        //}
    }

}
