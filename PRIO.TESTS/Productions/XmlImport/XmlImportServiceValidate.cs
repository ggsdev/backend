using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.FileImport.XML.FileContent._001;
using PRIO.src.Modules.FileImport.XML.FileContent._002;
using PRIO.src.Modules.FileImport.XML.FileContent._003;
using PRIO.src.Modules.FileImport.XML.Infra.Http.Services;
using PRIO.src.Modules.FileImport.XML.Infra.Utils;
using PRIO.src.Modules.FileImport.XML.ViewModels;
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
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.Productions.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.SystemHistories.Infra.EF.Repositories;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.SystemHistories.Interfaces;
using PRIO.TESTS.Productions.XmlImport.FileMocks;

namespace PRIO.TESTS.Productions.XmlImport
{
    [TestFixture]
    internal class XmlImportServiceTest
    {
        private DataContext _context;
        private IMapper _mapper;

        private XMLImportService _service;
        private FieldFRService _fieldFRService;
        private MeasurementService _measurementService;
        private SystemHistoryService _systemHistoryService;

        private ISystemHistoryRepository _systemHistoryRepository;
        private IProductionRepository _productionRepository;
        private IGasVolumeCalculationRepository _gasRepository;
        private IFieldRepository _fieldRepository;
        private IOilVolumeCalculationRepository _oilRepository;
        private IMeasurementHistoryRepository _measurementHistoryRepository;
        private IMeasurementRepository _measurementRepository;
        private IMeasuringPointRepository _measuringPointRepository;
        private IInstallationRepository _installationRepository;

        private User _user;
        private Installation _bravoInstallation;
        private Section _tramoASection;
        private Section _tramoBSection;
        private OilVolumeCalculation _oilVolumeCalculation;
        private MeasuringPoint _measuringPoint;
        private MeasuringPoint _measuringPoint2;
        private Production _production;
        private FileType _fileType;

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
                cfg.CreateMap<Measurement, Client001DTO>();
                cfg.CreateMap<_001DTO, Measurement>();

                cfg.CreateMap<User, UserDTO>();

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

            var bravoCluster = new Cluster
            {
                Name = "Bravo",
                User = _user,
            };

            _oilVolumeCalculation = new OilVolumeCalculation
            {
                Id = Guid.NewGuid(),
                Sections = new(),

            };

            _bravoInstallation = new Installation()
            {
                Name = "FPSO Bravo",
                UepCod = "10905",
                CodInstallationAnp = "10905",
                UepName = "FPSO Bravo",
                User = _user,
                Cluster = bravoCluster,
                Id = Guid.NewGuid(),
                IsProcessingUnit = true,
                OilVolumeCalculation = _oilVolumeCalculation
            };

            _measuringPoint = new MeasuringPoint()
            {
                DinamicLocalMeasuringPoint = "Tramo A",
                Installation = _bravoInstallation,
                Id = Guid.NewGuid(),
                TagPointMeasuring = "FT-1198B-01",
            };

            _measuringPoint2 = new MeasuringPoint()
            {
                DinamicLocalMeasuringPoint = "Tramo B",
                Installation = _bravoInstallation,
                Id = Guid.NewGuid(),
                TagPointMeasuring = "FT-1198C-01",
            };

            _tramoASection = new Section
            {
                Id = Guid.NewGuid(),
                MeasuringPoint = _measuringPoint,
                StaticLocalMeasuringPoint = "Tramo A",
                OilVolumeCalculation = _oilVolumeCalculation,
                IsApplicable = true,
            };

            _tramoBSection = new Section
            {
                Id = Guid.NewGuid(),
                MeasuringPoint = _measuringPoint2,
                StaticLocalMeasuringPoint = "Tramo B",
                OilVolumeCalculation = _oilVolumeCalculation,
                IsApplicable = true,
            };

            _context.Clusters.Add(bravoCluster);

            _measuringPoint.Section = _tramoASection;
            _measuringPoint2.Section = _tramoBSection;

            _oilVolumeCalculation.Sections.Add(_tramoASection);
            _oilVolumeCalculation.Sections.Add(_tramoBSection);


            _context.Installations.Add(_bravoInstallation);

            _context.MeasuringPoints.Add(_measuringPoint);
            _context.MeasuringPoints.Add(_measuringPoint2);

            _context.OilVolumeCalculations.Add(_oilVolumeCalculation);

            _context.Sections.Add(_tramoASection);
            _context.Sections.Add(_tramoBSection);

            //_production = new Production()
            //{
            //    Id = Guid.NewGuid(),
            //    Installation = _bravoInstallation,
            //    CalculatedImportedAt = DateTime.UtcNow,
            //    CalculatedImportedBy = _user,
            //    MeasuredAt = DateTime.UtcNow,
            //    TotalProduction = 0m,
            //    StatusProduction = false
            //};

            //_fileType = new FileType()
            //{
            //    Name = "001",
            //    Acronym = "PGL",
            //    Id = Guid.NewGuid(),
            //};

            //_context.FileTypes.Add(_fileType);

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
        public async Task ValidateFile_NonXmlFileReturnsError()
        {
            var fileContent001 = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo001,
                FileName = "bravo001.xsd",
                FileType = XmlUtils.File001
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent001
                }
            };
            try
            {
                var result = await _service.Validate(requestViewModel, _user);
                Assert.Fail("Expected BadRequestException not thrown");
            }

            catch (BadRequestException ex)
            {
                Assert.That(ex.Message, Is.Not.Null);
                Assert.That(ex.Message, Is.EqualTo($"Formato arquivo inválido, deve ter a extensão xml. Importação falhou arquivo com nome: {fileContent001.FileName}"));
            }
        }

        [Test]
        public async Task ValidateFile_NonValidBase64ReturnsError()
        {
            var fileContent001 = new FileContent
            {
                ContentBase64 = "invalidbase64",
                FileName = "bravo001.xml",
                FileType = XmlUtils.File001
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent001
                }
            };
            try
            {
                var result = await _service.Validate(requestViewModel, _user);
                Assert.Fail("Expected BadRequestException not thrown");
            }

            catch (BadRequestException ex)
            {
                Assert.That(ex.Message, Is.Not.Null);
                Assert.That(ex.Message, Is.EqualTo("Não é um base64 válido"));
            }
        }

        [Test]
        public async Task ValidateFile_NonValidFileTypeReturnsError()
        {
            var fileContent001 = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo001,
                FileName = "bravo001.xml",
                FileType = "004"
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent001
                }
            };
            try
            {
                var result = await _service.Validate(requestViewModel, _user);
                Assert.Fail("Expected BadRequestException not thrown");
            }

            catch (BadRequestException ex)
            {
                Assert.That(ex.Message, Is.Not.Null);
                Assert.That(ex.Message, Is.EqualTo($"Deve pertencer a uma das categorias: 001, 002 e 003. Importação falhou, arquivo com nome: {fileContent001.FileName}"));
            }
        }

        [Test]
        public async Task Validate001File_NonValidXmlStructureMissingProductionTag()
        {
            var fileContent001 = new FileContent
            {
                ContentBase64 = BravoMocks.BravoInvalidXsd001,
                FileName = "bravo001.xml",
                FileType = XmlUtils.File001
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent001
                }
            };
            try
            {
                var result = await _service.Validate(requestViewModel, _user);
                Assert.Fail("Expected BadRequestException not thrown");
            }

            catch (BadRequestException ex)
            {
                Assert.That(ex.Message, Is.Not.Null);
                Assert.That(ex.Message, Is.EqualTo("The element 'DADOS_BASICOS' has incomplete content. List of possible elements expected: 'LISTA_INSTRUMENTO_TEMPERATURA, LISTA_ANALISADOR_DENSIDADE, LISTA_ANALISADOR_BSW, LISTA_PRODUCAO'."));
            }
        }

        [Test]
        public async Task Validate001File_NonValidXmlStructureMissingDadosBasicosElement()
        {
            var fileContent001 = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo001WithoutDadosBasicosElement,
                FileName = "bravo001.xml",
                FileType = XmlUtils.File001
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent001
                }
            };
            try
            {
                var result = await _service.Validate(requestViewModel, _user);
                Assert.Fail("Expected BadRequestException not thrown");
            }

            catch (BadRequestException ex)
            {
                Assert.That(ex.Message, Is.Not.Null);
                Assert.That(ex.Message, Is.EqualTo("The element 'LISTA_DADOS_BASICOS' has incomplete content. List of possible elements expected: 'DADOS_BASICOS'."));
            }
        }

        [Test]
        public async Task Validate001File_ReturnsAListOfErrorsWhenNotProperlyRegisteringHierarchy()
        {
            var fileContent001 = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo001InstallationInvalidAndMeasuringPointInvalid,
                FileName = "bravo001.xml",
                FileType = XmlUtils.File001
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent001
                }
            };
            try
            {
                var result = await _service.Validate(requestViewModel, _user);
                Assert.Fail("Expected BadRequestException not thrown");
            }

            catch (BadRequestException ex)
            {
                Assert.That(ex.Message, Is.Not.Null);
                Assert.That(ex.Message, Is.EqualTo($"Algum(s) erro(s) ocorreram durante a validação do arquivo de nome: {fileContent001.FileName}"));
                Assert.That(ex.Errors, Is.Not.Null);
                Assert.That(ex.Errors.Count, Is.GreaterThan(0));
            }
        }
        [Test]
        public async Task Validate001File_ReturnsAProperResponseWithOneMeasurement()
        {
            var fileContent001 = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo001OneMeasurement,
                FileName = "bravo001.xml",
                FileType = XmlUtils.File001
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent001
                }
            };

            var result = await _service.Validate(requestViewModel, _user);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.InstallationId, Is.EqualTo(_bravoInstallation.Id));
            Assert.That(result.UepCode, Is.EqualTo(_bravoInstallation.UepCod));
            Assert.That(result.UepName, Is.EqualTo(_bravoInstallation.UepName));
            Assert.That(result._001File.Count, Is.EqualTo(1));
            Assert.That(result._001File[0].Measurements.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Validate001File_ReturnsAProperResponseWithMoreThanOneMeasurement()
        {
            var fileContent001 = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo001,
                FileName = "bravo001.xml",
                FileType = XmlUtils.File001
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent001
                }
            };

            var result = await _service.Validate(requestViewModel, _user);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.InstallationId, Is.EqualTo(_bravoInstallation.Id));
            Assert.That(result.UepCode, Is.EqualTo(_bravoInstallation.UepCod));
            Assert.That(result.UepName, Is.EqualTo(_bravoInstallation.UepName));
            Assert.That(result._001File.Count, Is.EqualTo(1));
            Assert.That(result._001File[0].Measurements.Count, Is.EqualTo(2));
        }
    }
}
