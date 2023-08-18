using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.FileImport.XML.FileContent._001;
using PRIO.src.Modules.FileImport.XML.FileContent._002;
using PRIO.src.Modules.FileImport.XML.FileContent._003;
using PRIO.src.Modules.FileImport.XML.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Repositories;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.Measurements.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.Productions.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.SystemHistories.Infra.EF.Repositories;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.SystemHistories.Interfaces;

namespace PRIO.TESTS.Productions.XmlImport
{
    [TestFixture]
    internal class XmlImportServiceTest
    {
        private XMLImportService _service;
        private FieldFRService _fieldFRService;
        private MeasurementService _measurementService;
        private SystemHistoryService _systemHistoryService;
        private ISystemHistoryRepository _systemHistoryRepository;
        private DataContext _context;
        private User _user;
        private Installation _installation;
        private MeasuringPoint _measuringPoint;
        private IProductionRepository _productionRepository;
        private Production _production;
        private FileType _fileType;
        private IGasVolumeCalculationRepository _gasRepository;
        private IFieldRepository _fieldRepository;
        private IOilVolumeCalculationRepository _oilRepository;
        private IMeasurementHistoryRepository _measurementHistoryRepository;
        private IMeasurementRepository _measurementRepository;
        private IMeasuringPointRepository _measuringPointRepository;
        private IInstallationRepository _installationRepository;
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
                cfg.CreateMap<_001PMO, Measurement>();
                cfg.CreateMap<Measurement, _001DTO>();
                cfg.CreateMap<_001DTO, Measurement>();

                cfg.CreateMap<_002PMGL, Measurement>();
                cfg.CreateMap<Measurement, _002DTO>();
                cfg.CreateMap<_002DTO, Measurement>();
                cfg.CreateMap<Client002DTO, Measurement>();
                cfg.CreateMap<Client001DTO, Measurement>();
                cfg.CreateMap<Client003DTO, Measurement>();
                cfg.CreateMap<Client039DTO, Measurement>();
                cfg.CreateMap<MeasurementHistory, MeasurementHistoryDto>()
                    .ForMember(dest => dest.ImportId, opt => opt.MapFrom(src =>
                    src.Id));

                cfg.CreateMap<_003PMGD, Measurement>();
                cfg.CreateMap<Measurement, _003DTO>();
                cfg.CreateMap<_003DTO, Measurement>();
                cfg.CreateMap<Measurement, Client003DTO>();
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
                StatusProduction = false
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
            _measurementRepository = new MeasurementRepository(_context);
            _oilRepository = new OilVolumeCalculationRepository(_context);
            _measurementHistoryRepository = new MeasurementHistoryRepository(_context);
            _measuringPointRepository = new MeasuringPointRepository(_context);
            _fieldRepository = new FieldRepository(_context);
            _systemHistoryRepository = new SystemHistoryRepository(_context);
            _systemHistoryService = new SystemHistoryService(_mapper, _systemHistoryRepository);

            _measurementService = new MeasurementService(_measurementHistoryRepository, _measurementRepository, _mapper);

            _fieldFRService = new FieldFRService(_mapper, _installationRepository, _fieldRepository, _productionRepository);

            _service = new XMLImportService(_mapper, _installationRepository, _measurementRepository, _measurementService, _gasRepository, _measuringPointRepository, _oilRepository, _measurementHistoryRepository, _productionRepository, _fieldRepository, _fieldFRService);

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
        public async Task Validate001File_SuccesfullyReturns()
        {



        }
    }
}
