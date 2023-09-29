using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.Infra.Http.Services;
using PRIO.src.Modules.FileImport.XML.Measuring.Dtos;
using PRIO.src.Modules.FileImport.XML.Measuring.FileContent._001;
using PRIO.src.Modules.FileImport.XML.Measuring.FileContent._002;
using PRIO.src.Modules.FileImport.XML.Measuring.FileContent._003;
using PRIO.src.Modules.FileImport.XML.Measuring.Infra.Utils;
using PRIO.src.Modules.FileImport.XML.Measuring.ViewModels;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Repositories;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;
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
        private Production _production;
        private FileType _fileType;

        #region Oil
        private Section _tramoASection;
        private Section _tramoBSection;
        private OilVolumeCalculation _oilVolumeCalculation;
        private MeasuringPoint _measuringPoint;
        private MeasuringPoint _measuringPoint2;
        #endregion

        #region Gas
        private HPFlare _hpFlare;
        private LPFlare _lpFlare;
        private LowPressureGas _lowPressure;
        private GasVolumeCalculation _gasVolumeCalculation;

        private MeasuringPoint _measuringPoint3;
        private MeasuringPoint _measuringPoint4;

        private MeasuringPoint _measuringPoint5;
        #endregion


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
                cfg.CreateMap<Measurement, Client002DTO>();
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
            _gasVolumeCalculation = new GasVolumeCalculation
            {
                Id = Guid.NewGuid(),
                HPFlares = new(),
                LPFlares = new()

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
                OilVolumeCalculation = _oilVolumeCalculation,
                GasVolumeCalculation = _gasVolumeCalculation,
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

            _measuringPoint3 = new MeasuringPoint()
            {
                DinamicLocalMeasuringPoint = "HP Flare",
                Installation = _bravoInstallation,
                Id = Guid.NewGuid(),
                TagPointMeasuring = "FT-3010-01",
            };

            _measuringPoint4 = new MeasuringPoint()
            {
                DinamicLocalMeasuringPoint = "LPFlare",
                Installation = _bravoInstallation,
                Id = Guid.NewGuid(),
                TagPointMeasuring = "FT-3020-01",
            };

            _measuringPoint5 = new MeasuringPoint()
            {
                DinamicLocalMeasuringPoint = "GasCombustivel",
                Installation = _bravoInstallation,
                Id = Guid.NewGuid(),
                TagPointMeasuring = "FIT-3115-03",
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

            _hpFlare = new HPFlare
            {
                Id = Guid.NewGuid(),
                MeasuringPoint = _measuringPoint3,
                StaticLocalMeasuringPoint = "HP Flare",
                GasVolumeCalculation = _gasVolumeCalculation,
                IsApplicable = true,
            };

            _lpFlare = new LPFlare
            {
                Id = Guid.NewGuid(),
                MeasuringPoint = _measuringPoint4,
                StaticLocalMeasuringPoint = "LP Flare",
                GasVolumeCalculation = _gasVolumeCalculation,
                IsApplicable = true,
            };

            _lowPressure = new LowPressureGas
            {
                Id = Guid.NewGuid(),
                MeasuringPoint = _measuringPoint5,
                StaticLocalMeasuringPoint = "Low Pressure",
                GasVolumeCalculation = _gasVolumeCalculation,
                IsApplicable = true,
            };

            _context.Clusters.Add(bravoCluster);

            _measuringPoint.Section = _tramoASection;
            _measuringPoint2.Section = _tramoBSection;

            _measuringPoint3.HPFlare = _hpFlare;
            _measuringPoint4.LPFlare = _lpFlare;
            _measuringPoint5.LowPressureGas = _lowPressure;

            _oilVolumeCalculation.Sections.Add(_tramoASection);
            _oilVolumeCalculation.Sections.Add(_tramoBSection);

            _gasVolumeCalculation.HPFlares.Add(_hpFlare);
            _gasVolumeCalculation.LPFlares.Add(_lpFlare);
            _gasVolumeCalculation.LowPressureGases.Add(_lowPressure);

            _context.Installations.Add(_bravoInstallation);

            _context.MeasuringPoints.Add(_measuringPoint);
            _context.MeasuringPoints.Add(_measuringPoint2);

            _context.MeasuringPoints.Add(_measuringPoint3);
            _context.MeasuringPoints.Add(_measuringPoint4);
            _context.MeasuringPoints.Add(_measuringPoint5);

            _context.OilVolumeCalculations.Add(_oilVolumeCalculation);

            _context.GasVolumeCalculations.Add(_gasVolumeCalculation);

            _context.Sections.Add(_tramoASection);
            _context.Sections.Add(_tramoBSection);

            _context.HPFlares.Add(_hpFlare);
            _context.LPFlares.Add(_lpFlare);
            _context.LowPressureGases.Add(_lowPressure);

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
            var fileContent = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo001,
                FileName = "bravo001.xsd",
                FileType = XmlUtils.File001
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent
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
                Assert.That(ex.Message, Is.EqualTo($"Formato arquivo inválido, deve ter a extensão xml. Importação falhou arquivo com nome: {fileContent.FileName}"));
            }
        }

        [Test]
        public async Task ValidateFile_NonValidBase64ReturnsError()
        {
            var fileContent = new FileContent
            {
                ContentBase64 = "invalidbase64",
                FileName = "bravo001.xml",
                FileType = XmlUtils.File001
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent
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
            var fileContent = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo001,
                FileName = "bravo001.xml",
                FileType = "004"
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent
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
                Assert.That(ex.Message, Is.EqualTo($"Deve pertencer a uma das categorias: 001, 002 e 003. Importação falhou, arquivo com nome: {fileContent.FileName}"));
            }
        }

        [Test]
        public async Task Validate001File_NonValidXmlStructureMissingProductionTag()
        {
            var fileContent = new FileContent
            {
                ContentBase64 = BravoMocks.BravoInvalidXsd001,
                FileName = "bravo001.xml",
                FileType = XmlUtils.File001
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent
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
            var fileContent = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo001WithoutDadosBasicosElement,
                FileName = "bravo001.xml",
                FileType = XmlUtils.File001
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent
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
            var fileContent = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo001InstallationInvalidAndMeasuringPointInvalid,
                FileName = "bravo001.xml",
                FileType = XmlUtils.File001
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent
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
                Assert.That(ex.Message, Is.EqualTo($"Algum(s) erro(s) ocorreram durante a validação do arquivo de nome: {fileContent.FileName}"));
                Assert.That(ex.Errors, Is.Not.Null);
                Assert.That(ex.Errors.Count, Is.GreaterThan(0));
            }
        }

        [Test]
        public async Task Validate001File_ReturnsAProperResponseWithOneMeasurement()
        {
            var fileContent = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo001OneMeasurement,
                FileName = "bravo001.xml",
                FileType = XmlUtils.File001
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent
                }
            };

            var result = await _service.Validate(requestViewModel, _user);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.InstallationId, Is.EqualTo(_bravoInstallation.Id));
            Assert.That(result.UepCode, Is.EqualTo(_bravoInstallation.UepCod));
            Assert.That(result.UepName, Is.EqualTo(_bravoInstallation.UepName));
            Assert.That(result._001File.Count, Is.EqualTo(1));
            Assert.That(result._001File[0].Measurements.Count, Is.EqualTo(1));
            Assert.That(result._001File[0].Measurements[0].COD_TAG_PONTO_MEDICAO_001, Is.EqualTo(_measuringPoint.TagPointMeasuring));
            Assert.That(result._001File[0].Measurements[0].COD_INSTALACAO_001, Is.EqualTo(_bravoInstallation.UepCod));
        }

        [Test]
        public async Task Validate001File_ReturnsAProperResponseWithMoreThanOneMeasurement()
        {
            var fileContent = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo001,
                FileName = "bravo001.xml",
                FileType = XmlUtils.File001
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent
                }
            };

            var result = await _service.Validate(requestViewModel, _user);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.InstallationId, Is.EqualTo(_bravoInstallation.Id));
            Assert.That(result.UepCode, Is.EqualTo(_bravoInstallation.UepCod));
            Assert.That(result.UepName, Is.EqualTo(_bravoInstallation.UepName));
            Assert.That(result._001File.Count, Is.EqualTo(1));
            Assert.That(result._001File[0].Measurements.Count, Is.EqualTo(2));

            Assert.That(result._001File[0].Measurements[0].COD_TAG_PONTO_MEDICAO_001, Is.EqualTo(_measuringPoint.TagPointMeasuring));
            Assert.That(result._001File[0].Measurements[0].COD_INSTALACAO_001, Is.EqualTo(_bravoInstallation.UepCod));
            Assert.That(result._001File[0].Measurements[1].COD_TAG_PONTO_MEDICAO_001, Is.EqualTo(_measuringPoint2.TagPointMeasuring));
            Assert.That(result._001File[0].Measurements[1].COD_INSTALACAO_001, Is.EqualTo(_bravoInstallation.UepCod));
        }

        [Test]
        public async Task Validate002File_NonValidXmlStructureMissingProductionTag()
        {
            var fileContent = new FileContent
            {
                ContentBase64 = BravoMocks.BravoInvalidXsd002,
                FileName = "bravo002.xml",
                FileType = XmlUtils.File002
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent
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
                Assert.That(ex.Message, Is.EqualTo("The element 'LISTA_CONFIGURACAO_CV' has incomplete content. List of possible elements expected: 'CONFIGURACAO_CV'.,The element 'LISTA_PRODUCAO' has incomplete content. List of possible elements expected: 'PRODUCAO'."));
            }
        }

        [Test]
        public async Task Validate002File_ReturnsAListOfErrorsWhenNotProperlyRegisteringHierarchy()
        {
            var fileContent = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo002InvalidInstallationAndMeasuringPoint,
                FileName = "bravo002.xml",
                FileType = XmlUtils.File002
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent
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
                Assert.That(ex.Message, Is.EqualTo($"Algum(s) erro(s) ocorreram durante a validação do arquivo de nome: {fileContent.FileName}"));
                Assert.That(ex.Errors, Is.Not.Null);
                Assert.That(ex.Errors.Count, Is.GreaterThan(0));
            }
        }

        [Test]
        public async Task Validate002File_ReturnsAProperResponseWithMoreThanOneMeasurement()
        {
            var fileContent = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo002TwoMeasurements,
                FileName = "bravo002.xml",
                FileType = XmlUtils.File002
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent
                }
            };

            var result = await _service.Validate(requestViewModel, _user);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.InstallationId, Is.EqualTo(_bravoInstallation.Id));
            Assert.That(result.UepCode, Is.EqualTo(_bravoInstallation.UepCod));
            Assert.That(result.UepName, Is.EqualTo(_bravoInstallation.UepName));
            Assert.That(result._002File.Count, Is.EqualTo(1));
            Assert.That(result._002File[0].Measurements.Count, Is.EqualTo(3));

            Assert.That(result._002File[0].Measurements[0].COD_TAG_PONTO_MEDICAO_002, Is.EqualTo(_measuringPoint3.TagPointMeasuring));
            Assert.That(result._002File[0].Measurements[0].COD_INSTALACAO_002, Is.EqualTo(_bravoInstallation.UepCod));
            Assert.That(result._002File[0].Measurements[1].COD_TAG_PONTO_MEDICAO_002, Is.EqualTo(_measuringPoint4.TagPointMeasuring));
            Assert.That(result._002File[0].Measurements[1].COD_INSTALACAO_002, Is.EqualTo(_bravoInstallation.UepCod));


            Assert.That(result._002File[0].Measurements[2].Summary.TagMeasuringPoint, Is.EqualTo(_measuringPoint5.TagPointMeasuring));
        }

        [Test]
        public async Task Validate003File_NonValidXmlStructureMissingProductionTag()
        {
            var fileContent = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo003InvalidXsd,
                FileName = "bravo003.xml",
                FileType = XmlUtils.File003
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent
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
                Assert.That(ex.Message, Is.EqualTo("The element 'PRODUCAO' has invalid child element 'DHA_FIM_PERIODO_MEDICAO'. List of possible elements expected: 'DHA_INICIO_PERIODO_MEDICAO'."));
            }
        }

        [Test]
        public async Task Validate003File_ReturnsAListOfErrorsWhenNotProperlyRegisteringHierarchy()
        {
            var fileContent = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo003InvalidInstallationAndMeasuringPoint,
                FileName = "bravo003.xml",
                FileType = XmlUtils.File003
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent
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
                Assert.That(ex.Message, Is.EqualTo($"Algum(s) erro(s) ocorreram durante a validação do arquivo de nome: {fileContent.FileName}"));
                Assert.That(ex.Errors, Is.Not.Null);
                Assert.That(ex.Errors.Count, Is.GreaterThan(0));
            }
        }

        [Test]
        public async Task Validate003File_ReturnsAProperResponseWithOneMeasurement()
        {
            var fileContent = new FileContent
            {
                ContentBase64 = BravoMocks.Bravo003,
                FileName = "bravo003.xml",
                FileType = XmlUtils.File003
            };

            var requestViewModel = new RequestXmlViewModel
            {
                Files = new List<FileContent>
                {
                    fileContent
                }
            };

            var result = await _service.Validate(requestViewModel, _user);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.InstallationId, Is.EqualTo(_bravoInstallation.Id));
            Assert.That(result.UepCode, Is.EqualTo(_bravoInstallation.UepCod));
            Assert.That(result.UepName, Is.EqualTo(_bravoInstallation.UepName));
            Assert.That(result._003File.Count, Is.EqualTo(1));
            Assert.That(result._003File[0].Measurements.Count, Is.EqualTo(3));

            Assert.That(result._003File[0].Measurements[0].COD_TAG_PONTO_MEDICAO_003, Is.EqualTo(_measuringPoint5.TagPointMeasuring));
            Assert.That(result._003File[0].Measurements[0].COD_INSTALACAO_003, Is.EqualTo(_bravoInstallation.UepCod));

            Assert.That(result._003File[0].Measurements[1].Summary.TagMeasuringPoint, Is.EqualTo(_measuringPoint3.TagPointMeasuring));
            Assert.That(result._003File[0].Measurements[2].Summary.TagMeasuringPoint, Is.EqualTo(_measuringPoint4.TagPointMeasuring));
        }
    }
}
