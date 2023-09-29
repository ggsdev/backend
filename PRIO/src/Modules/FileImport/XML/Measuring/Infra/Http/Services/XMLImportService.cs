using AutoMapper;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.Hierarchy.Dtos;
using PRIO.src.Modules.FileImport.XML.Measuring.Dtos;
using PRIO.src.Modules.FileImport.XML.Measuring.FileContent;
using PRIO.src.Modules.FileImport.XML.Measuring.FileContent._001;
using PRIO.src.Modules.FileImport.XML.Measuring.FileContent._002;
using PRIO.src.Modules.FileImport.XML.Measuring.FileContent._003;
using PRIO.src.Modules.FileImport.XML.Measuring.Infra.Utils;
using PRIO.src.Modules.FileImport.XML.Measuring.ViewModels;
using PRIO.src.Modules.Hierarchy.Fields.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.ViewModels;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Dtos;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Utils;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Utils;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
namespace PRIO.src.Modules.FileImport.XML.Infra.Http.Services
{
    public partial class XMLImportService
    {
        private readonly IMapper _mapper;
        private readonly MeasurementService _measurementService;
        private readonly IInstallationRepository _installationRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IGasVolumeCalculationRepository _gasCalculationRepository;
        private readonly IOilVolumeCalculationRepository _oilCalculationRepository;
        private readonly IProductionRepository _productionRepository;
        private readonly IMeasuringPointRepository _measuringPointRepository;
        private readonly IMeasurementRepository _repository;
        private readonly IMeasurementHistoryRepository _measurementHistoryRepository;
        private readonly FieldFRService _fieldFRService;

        [GeneratedRegex("(?<=data:@file/xml;base64,)\\w+")]
        private static partial Regex XmlRegex();
        public XMLImportService(IMapper mapper, IInstallationRepository installationRepository, IMeasurementRepository xMLImportRepository, MeasurementService measurementService, IGasVolumeCalculationRepository gasVolumeCalculationRepository, IMeasuringPointRepository measuringPointRepository, IOilVolumeCalculationRepository oilVolumeCalculationRepository, IMeasurementHistoryRepository measurementHistoryRepository, IProductionRepository productionRepository, IFieldRepository fieldRepository, FieldFRService fieldFRService)
        {
            _mapper = mapper;
            _installationRepository = installationRepository;
            _repository = xMLImportRepository;
            _measuringPointRepository = measuringPointRepository;
            _measurementService = measurementService;
            _gasCalculationRepository = gasVolumeCalculationRepository;
            _oilCalculationRepository = oilVolumeCalculationRepository;
            _measurementHistoryRepository = measurementHistoryRepository;
            _productionRepository = productionRepository;
            _fieldRepository = fieldRepository;
            _fieldFRService = fieldFRService;
        }

        public async Task<ResponseXmlDto> Validate(RequestXmlViewModel data, User user)
        {
            #region client side validations
            for (int i = 0; i < data.Files.Count; ++i)
            {
                var isValidExtension = data.Files[i].FileName.ToLower().EndsWith(".xml");

                if (isValidExtension is false)
                    throw new BadRequestException($"Formato arquivo inválido, deve ter a extensão xml. Importação falhou arquivo com nome: {data.Files[i].FileName}");

                var fileContent = data.Files[i].ContentBase64.Replace("data:@file/xml;base64,", "");

                if (Decrypt.TryParseBase64String(fileContent, out _) is false)
                    throw new BadRequestException("Não é um base64 válido");
                var isValidFileName = new List<string>()
                    {
                        "001",
                        "002",
                        "003"
                    }.Contains(data.Files[i].FileType);

                if (!isValidFileName)
                    throw new BadRequestException($"Deve pertencer a uma das categorias: 001, 002 e 003. Importação falhou, arquivo com nome: {data.Files[i].FileName}");

                if (data.Files[i].FileName.Count(c => c == '_') >= 2)
                {
                    var datePortion = data.Files[i].FileName.Split('_')[2];

                    if (!IsValidAndUniqueDate(datePortion, data.Files))
                        throw new BadRequestException("Todas as datas no nome dos arquivos devem ser iguais.");
                }
            }

            #endregion

            var errorsInImport = new List<string>();
            var errorsInFormat = new List<string>();

            var userDto = _mapper.Map<UserDTO>(user);
            var response = new ResponseXmlDto();

            var gasResume = new GasSummary();

            var listOfMeasurementDates = new List<DateTime>();

            for (int i = 0; i < data.Files.Count; ++i)
            {

                var relativeSchemaPath = string.Empty;

                var fileContent = data.Files[i].ContentBase64.Replace("data:@file/xml;base64,", "");

                switch (data.Files[i].FileType)
                {

                    case "001":
                        relativeSchemaPath = Path.Combine("schemasXsd", "001.xsd");
                        break;

                    case "002":
                        relativeSchemaPath = Path.Combine("schemasXsd", "002.xsd");
                        break;

                    case "003":
                        relativeSchemaPath = Path.Combine("schemasXsd", "003.xsd");
                        break;

                }


                #region pathing
                //var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\.."));
                //var relativeSchemaPath = Path.Combine("src", "Modules", "FileImport", "XML", "FileContent", $"_{data.Files[i].FileType}\\Schema.xsd");
                var importId = Guid.NewGuid();
                var pathXml = Path.GetTempPath() + importId + ".xml";
                //var pathSchema = Path.GetFullPath(Path.Combine(projectRoot, relativeSchemaPath)); 
                var pathSchema = relativeSchemaPath;

                #endregion

                #region writting, parsing

                await File.WriteAllBytesAsync(pathXml, Convert.FromBase64String(fileContent));
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var parserContext = new XmlParserContext(null, null, null, XmlSpace.None)
                {
                    Encoding = Encoding.GetEncoding(1252)
                };

                using (var r = XmlReader.Create(pathXml, null, parserContext))
                {
                    var result = Functions.CheckFormat(pathXml, pathSchema, errorsInFormat);
                    if (errorsInFormat.Count > 0)
                        throw new BadRequestException($"Algum(s) erro(s) de formatação ocorreram durante a validação do arquivo de nome: {data.Files[i].FileName}", errors: errorsInFormat);
                    if (result is not null && result.Count > 0)
                        throw new BadRequestException(string.Join(",", result));
                }

                var documentXml = XDocument.Load(pathXml);
                #endregion

                #region generic elements and basic validation
                var rootElement = documentXml.Root;
                var dadosBasicosElements = rootElement?.Elements("LISTA_DADOS_BASICOS")?.Elements("DADOS_BASICOS");

                if (dadosBasicosElements is null)
                    throw new BadRequestException("LISTA_DADOS_BASICOS XML deve estar presente.");
                #endregion

                #region response
                var genericFile = new MeasurementHistoryDto
                {
                    FileContent = data.Files[i].ContentBase64,
                    FileName = data.Files[i].FileName,
                    FileType = data.Files[i].FileType,
                    ImportedAt = DateTime.UtcNow.AddHours(-3).ToString("dd/MM/yyyy"),
                    ImportedBy = userDto,
                    ImportId = importId
                };

                var response003 = new Response003DTO
                {
                    File = genericFile
                };
                var response002 = new Response002DTO
                {
                    File = genericFile
                }
                ; var response001 = new Response001DTO
                {
                    File = genericFile
                };

                #endregion

                for (int k = 0; k < dadosBasicosElements.Count(); ++k)
                {
                    var dadosBasicosElement = dadosBasicosElements.ElementAt(k);

                    switch (data.Files[i].FileType)
                    {
                        #region 001
                        case "001":
                            {
                                #region elementos XML
                                var dadosBasicos = dadosBasicosElement is not null ? Functions.DeserializeXml<DADOS_BASICOS_001>(dadosBasicosElement) : null;

                                var configuracaoCvElement = dadosBasicosElement?.Element("LISTA_CONFIGURACAO_CV")?.Element("CONFIGURACAO_CV");
                                var configuracaoCv = configuracaoCvElement is not null ? Functions.DeserializeXml<CONFIGURACAO_CV_001>(configuracaoCvElement) : null;

                                var elementoPrimarioElement = dadosBasicosElement?.Element("LISTA_ELEMENTO_PRIMARIO")?.Element("ELEMENTO_PRIMARIO");
                                var elementoPrimario = elementoPrimarioElement is not null ? Functions.DeserializeXml<ELEMENTO_PRIMARIO_001>(elementoPrimarioElement) : null;

                                var instrumentoPressaoElement = dadosBasicosElement?.Element("LISTA_INSTRUMENTO_PRESSAO")?.Element("INSTRUMENTO_PRESSAO");
                                var instrumentoPressao = instrumentoPressaoElement is not null ? Functions.DeserializeXml<INSTRUMENTO_PRESSAO_001>(instrumentoPressaoElement) : null;

                                var instrumentoTemperaturaElement = dadosBasicosElement?.Element("LISTA_INSTRUMENTO_TEMPERATURA")?.Element("INSTRUMENTO_TEMPERATURA");
                                var instrumentoTemperatura = instrumentoTemperaturaElement is not null ? Functions.DeserializeXml<INSTRUMENTO_TEMPERATURA_001>(instrumentoTemperaturaElement) : null;

                                var producaoElement = dadosBasicosElement?.Element("LISTA_PRODUCAO")?.Element("PRODUCAO");
                                var producao = producaoElement is not null ? Functions.DeserializeXml<PRODUCAO_001>(producaoElement) : null;
                                #endregion

                                if (dadosBasicos is not null && dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_001 is not null && dadosBasicos.COD_TAG_PONTO_MEDICAO_001 is not null && dadosBasicos.COD_INSTALACAO_001 is not null && producao is not null && producao.DHA_INICIO_PERIODO_MEDICAO_001 is not null)
                                {

                                    if (DateTime.TryParseExact(producao.DHA_INICIO_PERIODO_MEDICAO_001, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateBeginningMeasurement))
                                    {

                                        listOfMeasurementDates.Add(dateBeginningMeasurement.Date);

                                        var checkDateExists = await _repository.GetMeasurementByDate(dateBeginningMeasurement, XmlUtils.File001);

                                        if (checkDateExists is not null && checkDateExists.IsActive)
                                            errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS) data: {producao.DHA_INICIO_PERIODO_MEDICAO_001} já existente.");
                                    }

                                    else
                                    {
                                        errorsInFormat.Add("Formato da tag DHA_INICIO_PERIODO_MEDICAO incorreto deve ser: dd/MM/yyyy HH:mm:ss");
                                    }

                                    var installation = await _installationRepository.GetInstallationMeasurementByUepAndAnpCodAsync(dadosBasicos.COD_INSTALACAO_001, XmlUtils.File001);

                                    if (installation is null)
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): {ErrorMessages.NotFound<Installation>()}");

                                    if (installation is not null && installation.IsActive is false)
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): Instalação:{installation.Name} está inativa");


                                    var measuringPoint = await _measuringPointRepository
                                        .GetByTagMeasuringPointXML(dadosBasicos.COD_TAG_PONTO_MEDICAO_001, XmlUtils.File001);

                                    if (measuringPoint is not null && measuringPoint.IsActive is false)
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): Ponto de medição: {measuringPoint.TagPointMeasuring} está inativo.");


                                    if (installation is not null && installation.IsActive is true)
                                    {
                                        var oilCalculation = await _oilCalculationRepository
                                            .GetOilVolumeCalculationByInstallationId(installation.Id);

                                        if (installation.MeasuringPoints is not null && measuringPoint is not null && measuringPoint.IsActive)
                                        {
                                            var containsInInstallation = false;

                                            foreach (var point in installation.MeasuringPoints)
                                                if (measuringPoint is not null && measuringPoint.TagPointMeasuring == point.TagPointMeasuring && point.IsActive && measuringPoint.IsActive)
                                                    containsInInstallation = true;

                                            if (containsInInstallation is false && measuringPoint.IsActive)
                                                errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), TAG do ponto de medição: {measuringPoint.TagPointMeasuring} não encontrado na instalação: {installation.Name}");
                                        }


                                        if (oilCalculation is null)
                                            errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), cálculo de óleo não configurado nesta instalação");

                                        if (oilCalculation is not null && oilCalculation.DrainVolumes.Count == 0 && oilCalculation.DORs.Count == 0 && oilCalculation.Sections.Count == 0 && oilCalculation.TOGRecoveredOils.Count == 0)
                                            errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), cálculo de óleo não configurado nesta instalação, não foi encontrado nenhum ponto de medição vinculado ao cálculo");

                                        if (measuringPoint is not null && measuringPoint.IsActive)
                                        {
                                            var containsInCalculation = false;
                                            var applicable = false;

                                            if (oilCalculation is not null && oilCalculation.Installation is not null)
                                            {
                                                if (oilCalculation.Installation.MeasuringPoints is null)
                                                    errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), Não foram encontrados pontos de medição nesta instalação");

                                                if (oilCalculation.DrainVolumes is not null)
                                                {
                                                    foreach (var drain in oilCalculation.DrainVolumes)
                                                        if (drain.IsActive && drain.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                        {
                                                            containsInCalculation = true;
                                                            if (drain.IsApplicable)
                                                                applicable = true;
                                                        }
                                                }

                                                if (oilCalculation.DORs is not null)
                                                {
                                                    foreach (var dor in oilCalculation.DORs)
                                                    {

                                                        if (dor.IsActive && dor.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                        {
                                                            containsInCalculation = true;
                                                            if (dor.IsApplicable)
                                                            {
                                                                applicable = true;

                                                            }
                                                        }
                                                    }
                                                }

                                                if (oilCalculation.Sections is not null)
                                                {
                                                    foreach (var section in oilCalculation.Sections)
                                                        if (section.IsActive && section.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                        {
                                                            containsInCalculation = true;
                                                            if (section.IsApplicable)
                                                                applicable = true;
                                                        }

                                                }

                                                if (oilCalculation.TOGRecoveredOils is not null)
                                                {
                                                    foreach (var togRecovered in oilCalculation.TOGRecoveredOils)
                                                        if (togRecovered.IsActive && togRecovered.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                        {
                                                            containsInCalculation = true;
                                                            if (togRecovered.IsApplicable)
                                                                applicable = true;
                                                        }
                                                }

                                            }

                                            if (errorsInImport.Count == 0 && applicable)
                                            {
                                                var measurement = new Measurement
                                                {
                                                    Id = Guid.NewGuid(),

                                                    #region atributos dados basicos
                                                    NUM_SERIE_ELEMENTO_PRIMARIO_001 = dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_001,
                                                    COD_INSTALACAO_001 = dadosBasicos.COD_INSTALACAO_001,
                                                    COD_TAG_PONTO_MEDICAO_001 = dadosBasicos.COD_TAG_PONTO_MEDICAO_001,
                                                    #endregion

                                                    #region configuracao cv
                                                    NUM_SERIE_COMPUTADOR_VAZAO_001 = configuracaoCv?.NUM_SERIE_COMPUTADOR_VAZAO_001,
                                                    DHA_COLETA_001 = XmlUtils.DateTimeParser(configuracaoCv?.DHA_COLETA_001, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                    MED_TEMPERATURA_001 = XmlUtils.DecimalParser(configuracaoCv?.MED_TEMPERATURA_001, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                    MED_PRESSAO_ATMSA_001 = XmlUtils.DecimalParser(configuracaoCv?.MED_PRESSAO_ATMSA_001, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                    MED_PRESSAO_RFRNA_001 = XmlUtils.DecimalParser(configuracaoCv?.MED_PRESSAO_RFRNA_001, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                    DSC_VERSAO_SOFTWARE_001 = configuracaoCv?.DSC_VERSAO_SOFTWARE_001,
                                                    #endregion

                                                    #region elemento primario
                                                    ICE_METER_FACTOR_1_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_1_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_METER_FACTOR_2_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_2_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_METER_FACTOR_3_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_3_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_METER_FACTOR_4_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_4_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_METER_FACTOR_5_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_5_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_METER_FACTOR_6_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_6_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_METER_FACTOR_7_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_7_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_METER_FACTOR_8_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_8_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_METER_FACTOR_9_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_9_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_METER_FACTOR_10_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_10_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_METER_FACTOR_11_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_11_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_METER_FACTOR_12_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_12_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_METER_FACTOR_13_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_13_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_METER_FACTOR_14_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_14_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_METER_FACTOR_15_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_15_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),

                                                    QTD_PULSOS_METER_FACTOR_1_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_1_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_METER_FACTOR_2_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_2_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_METER_FACTOR_3_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_3_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_METER_FACTOR_4_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_4_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_METER_FACTOR_5_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_5_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_METER_FACTOR_6_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_6_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_METER_FACTOR_7_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_7_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_METER_FACTOR_8_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_8_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_METER_FACTOR_9_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_9_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_METER_FACTOR_10_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_10_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_METER_FACTOR_11_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_11_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_METER_FACTOR_12_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_12_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_METER_FACTOR_13_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_13_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_METER_FACTOR_14_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_14_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_METER_FACTOR_15_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_15_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),

                                                    ICE_K_FACTOR_1_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_1_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_K_FACTOR_2_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_2_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_K_FACTOR_3_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_3_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_K_FACTOR_4_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_4_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_K_FACTOR_5_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_5_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_K_FACTOR_6_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_6_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_K_FACTOR_7_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_7_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_K_FACTOR_8_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_8_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_K_FACTOR_9_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_9_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_K_FACTOR_10_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_10_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_K_FACTOR_11_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_11_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_K_FACTOR_12_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_12_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_K_FACTOR_13_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_13_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_K_FACTOR_14_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_14_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_K_FACTOR_15_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_15_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),

                                                    QTD_PULSOS_K_FACTOR_1_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_1_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_K_FACTOR_2_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_2_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_K_FACTOR_3_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_3_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_K_FACTOR_4_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_4_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_K_FACTOR_5_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_5_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_K_FACTOR_6_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_6_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_K_FACTOR_7_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_7_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_K_FACTOR_8_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_8_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_K_FACTOR_9_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_9_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_K_FACTOR_10_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_10_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_K_FACTOR_11_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_11_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_K_FACTOR_12_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_12_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_K_FACTOR_13_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_13_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_K_FACTOR_14_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_14_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    QTD_PULSOS_K_FACTOR_15_001 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_15_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),

                                                    ICE_CUTOFF_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_CUTOFF_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_LIMITE_SPRR_ALARME_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_LIMITE_SPRR_ALARME_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    ICE_LIMITE_INFRR_ALARME_001 = XmlUtils.DecimalParser(elementoPrimario?.ICE_LIMITE_INFRR_ALARME_001, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                    IND_HABILITACAO_ALARME_1_001 = elementoPrimario?.IND_HABILITACAO_ALARME_1_001,
                                                    #endregion

                                                    #region instrumento pressao
                                                    NUM_SERIE_1_001 = instrumentoPressao?.NUM_SERIE_1_001,
                                                    MED_PRSO_LIMITE_SPRR_ALRME_001 = XmlUtils.DecimalParser(instrumentoPressao?.MED_PRSO_LIMITE_SPRR_ALRME_001, errorsInFormat, instrumentoPressaoElement?.Name.LocalName),
                                                    MED_PRSO_LMTE_INFRR_ALRME_001 = XmlUtils.DecimalParser(instrumentoPressao?.MED_PRSO_LMTE_INFRR_ALRME_001, errorsInFormat, instrumentoPressaoElement?.Name.LocalName),
                                                    IND_HABILITACAO_ALARME_2_001 = instrumentoPressao?.IND_HABILITACAO_ALARME_2_001,
                                                    MED_PRSO_ADOTADA_FALHA_001 = XmlUtils.DecimalParser(instrumentoPressao?.MED_PRSO_ADOTADA_FALHA_001, errorsInFormat, instrumentoPressaoElement?.Name.LocalName),
                                                    DSC_ESTADO_INSNO_CASO_FALHA_001 = instrumentoPressao?.DSC_ESTADO_INSNO_CASO_FALHA_001,
                                                    IND_TIPO_PRESSAO_CONSIDERADA_001 = instrumentoPressao?.IND_TIPO_PRESSAO_CONSIDERADA_001,
                                                    #endregion

                                                    #region instrumento temperatura

                                                    NUM_SERIE_2_001 = instrumentoTemperatura?.NUM_SERIE_2_001,
                                                    MED_TMPTA_SPRR_ALARME_001 = XmlUtils.DecimalParser(instrumentoTemperatura?.MED_TMPTA_SPRR_ALARME_001, errorsInFormat, instrumentoTemperaturaElement?.Name.LocalName),
                                                    MED_TMPTA_INFRR_ALRME_001 = XmlUtils.DecimalParser(instrumentoTemperatura?.MED_TMPTA_INFRR_ALRME_001, errorsInFormat, instrumentoTemperaturaElement?.Name.LocalName),
                                                    IND_HABILITACAO_ALARME_3_001 = instrumentoTemperatura?.IND_HABILITACAO_ALARME_3_001,
                                                    MED_TMPTA_ADTTA_FALHA_001 = XmlUtils.DecimalParser(instrumentoTemperatura?.MED_TMPTA_ADTTA_FALHA_001, errorsInFormat, instrumentoTemperaturaElement?.Name.LocalName),
                                                    DSC_ESTADO_INSTRUMENTO_FALHA_1_001 = instrumentoTemperatura?.DSC_ESTADO_INSTRUMENTO_FALHA_1_001,

                                                    #endregion

                                                    #region producao

                                                    DHA_INICIO_PERIODO_MEDICAO_001 = XmlUtils.DateTimeParser(producao?.DHA_INICIO_PERIODO_MEDICAO_001, errorsInFormat, producaoElement?.Name.LocalName),
                                                    DHA_FIM_PERIODO_MEDICAO_001 = XmlUtils.DateTimeParser(producao?.DHA_FIM_PERIODO_MEDICAO_001, errorsInFormat, producaoElement?.Name.LocalName),
                                                    ICE_DENSIDADADE_RELATIVA_001 = XmlUtils.DecimalParser(producao?.ICE_DENSIDADADE_RELATIVA_001, errorsInFormat, producaoElement?.Name.LocalName),
                                                    //ICE_CORRECAO_BSW_001 = XmlUtils.DecimalParser(producao?.ICE_CORRECAO_BSW_001?.Replac, errorsInFormat, producaoElement.Name.LocalName)XmlUtils.DecimalParser(producao?.ICE_CORRECAO_PRESSAO_LIQUIDO_001, errorsInFormat, producaoElement.Name.LocalName),
                                                    ICE_CRRCO_TEMPERATURA_LIQUIDO_001 = XmlUtils.DecimalParser(producao?.ICE_CRRCO_TEMPERATURA_LIQUIDO_001, errorsInFormat, producaoElement?.Name.LocalName),
                                                    MED_PRESSAO_ESTATICA_001 = XmlUtils.DecimalParser(producao?.MED_PRESSAO_ESTATICA_001, errorsInFormat, producaoElement?.Name.LocalName),
                                                    MED_TMPTA_FLUIDO_001 = XmlUtils.DecimalParser(producao?.MED_TMPTA_FLUIDO_001, errorsInFormat, producaoElement?.Name.LocalName),
                                                    MED_VOLUME_BRTO_CRRGO_MVMDO_001 = XmlUtils.DecimalParser(producao?.MED_VOLUME_BRTO_CRRGO_MVMDO_001, errorsInFormat, producaoElement?.Name.LocalName),
                                                    MED_VOLUME_BRUTO_MVMDO_001 = XmlUtils.DecimalParser(producao?.MED_VOLUME_BRUTO_MVMDO_001, errorsInFormat, producaoElement?.Name.LocalName),
                                                    //MED_VOLUME_LIQUIDO_MVMDO_001 = XmlUtils.DecimalParser(producao?.MED_VOLUME_LIQUIDO_MVMDO_001?.Replac, errorsInFormat, producaoElement?.Name.LocalName)XmlUtils.DecimalParser(producao?.MED_VOLUME_TTLZO_FIM_PRDO_001, errorsInFormat, producaoElement?.Name.LocalName),
                                                    MED_VOLUME_TTLZO_INCO_PRDO_001 = XmlUtils.DecimalParser(producao?.MED_VOLUME_TTLZO_INCO_PRDO_001, errorsInFormat, producaoElement?.Name.LocalName),

                                                    #endregion

                                                    FileName = data.Files[i].FileName,
                                                    Installation = installation,
                                                    User = user,
                                                    MeasuringPoint = measuringPoint,

                                                    FileType = new FileType
                                                    {
                                                        Name = data.Files[i].FileType,
                                                        Acronym = XmlUtils.FileAcronym001,
                                                        ImportId = importId,

                                                    },
                                                };

                                                //Console.WriteLine(measurement.COD_TAG_PONTO_MEDICAO_001);

                                                var measurement001DTO = _mapper.Map<Measurement, Client001DTO>(measurement);
                                                measurement001DTO.ImportId = importId;
                                                measurement001DTO.Summary = new ClientInfo
                                                {
                                                    Date = dateBeginningMeasurement,
                                                    Status = containsInCalculation,
                                                    LocationMeasuringPoint = measuringPoint.DinamicLocalMeasuringPoint,
                                                    TagMeasuringPoint = measuringPoint.TagPointMeasuring,
                                                    Volume = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,

                                                };

                                                response.UepName = installation.UepName;
                                                response.UepCode = installation.UepCod;
                                                response.DateProduction = dateBeginningMeasurement.ToString("dd/MM/yyyy");
                                                response.InstallationId = installation.Id;
                                                response001.Measurements.Add(measurement001DTO);

                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        #endregion

                        #region 002
                        case "002":
                            {
                                #region elementos XML
                                var dadosBasicos = dadosBasicosElement is not null ? Functions.DeserializeXml<DADOS_BASICOS_002>(dadosBasicosElement) : null;

                                var configuracaoCvElement = dadosBasicosElement?.Element("LISTA_CONFIGURACAO_CV")?.Element("CONFIGURACAO_CV");
                                var configuracaoCv = configuracaoCvElement is not null ? Functions.DeserializeXml<CONFIGURACAO_CV_002>(configuracaoCvElement) : null;

                                var elementoPrimarioElement = dadosBasicosElement?.Element("LISTA_ELEMENTO_PRIMARIO")?.Element("ELEMENTO_PRIMARIO");
                                var elementoPrimario = elementoPrimarioElement is not null ? Functions.DeserializeXml<ELEMENTO_PRIMARIO_002>(elementoPrimarioElement) : null;

                                var instrumentoPressaoElement = dadosBasicosElement?.Element("LISTA_INSTRUMENTO_PRESSAO")?.Element("INSTRUMENTO_PRESSAO");
                                var instrumentoPressao = instrumentoPressaoElement is not null ? Functions.DeserializeXml<INSTRUMENTO_PRESSAO_002>(instrumentoPressaoElement) : null;

                                var instrumentoTemperaturaElement = dadosBasicosElement?.Element("LISTA_INSTRUMENTO_TEMPERATURA")?.Element("INSTRUMENTO_TEMPERATURA");
                                var instrumentoTemperatura = instrumentoTemperaturaElement is not null ? Functions.DeserializeXml<INSTRUMENTO_TEMPERATURA_002>(instrumentoTemperaturaElement) : null;

                                var producaoElement = dadosBasicosElement?.Element("LISTA_PRODUCAO")?.Element("PRODUCAO");
                                var producao = producaoElement is not null ? Functions.DeserializeXml<PRODUCAO_002>(producaoElement) : null;

                                #endregion

                                if (dadosBasicos is not null && dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_002 is not null && dadosBasicos.COD_INSTALACAO_002 is not null && dadosBasicos.COD_TAG_PONTO_MEDICAO_002 is not null && producao is not null && producao.DHA_INICIO_PERIODO_MEDICAO_002 is not null)
                                {

                                    if (DateTime.TryParseExact(producao?.DHA_INICIO_PERIODO_MEDICAO_002, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateBeginningMeasurement))
                                    {
                                        listOfMeasurementDates.Add(dateBeginningMeasurement.Date);

                                        var checkDateExists = await _repository.GetMeasurementByDate(dateBeginningMeasurement, XmlUtils.File002);

                                        if (checkDateExists is not null && checkDateExists.IsActive)
                                            errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS) data: {producao.DHA_INICIO_PERIODO_MEDICAO_002} já existente.");


                                        var checkGasProductionExists = await _productionRepository.GetProductionGasByDate(dateBeginningMeasurement);
                                        if (checkGasProductionExists is not null && checkGasProductionExists.Gas is not null && checkGasProductionExists.IsActive)
                                            errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS) já existe produção de gás na data: {producao.DHA_INICIO_PERIODO_MEDICAO_002}");

                                    }
                                    else
                                    {
                                        errorsInFormat.Add("Formato da tag DHA_INICIO_PERIODO_MEDICAO incorreto deve ser: dd/MM/yyyy HH:mm:ss");
                                    }

                                    var installation = await _installationRepository
                                        .GetInstallationMeasurementByUepAndAnpCodAsync(dadosBasicos.COD_INSTALACAO_002, XmlUtils.File002);

                                    if (installation is null)
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): {ErrorMessages.NotFound<Installation>()}");

                                    var measuringPoint = await _measuringPointRepository
                                        .GetByTagMeasuringPointXML(dadosBasicos.COD_TAG_PONTO_MEDICAO_002, XmlUtils.File002);

                                    if (measuringPoint is not null && measuringPoint.IsActive is false)
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): Ponto de medição: {measuringPoint.TagPointMeasuring} está inativo.");

                                    if (installation is not null && installation.IsActive is false)
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): Instalação:{installation.Name} está inativa");
                                    //if (measuringPoint is null)
                                    //    errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), ponto de medição com TAG: {dadosBasicos.COD_TAG_PONTO_MEDICAO_002}, {ErrorMessages.NotFound<MeasuringPoint>()}");

                                    if (installation is not null && measuringPoint is not null && installation.IsActive)
                                    {
                                        if (installation.MeasuringPoints is not null)
                                        {
                                            var containsInInstallation = false;

                                            foreach (var point in installation.MeasuringPoints)
                                                if (measuringPoint is not null && measuringPoint.TagPointMeasuring == point.TagPointMeasuring && measuringPoint.IsActive)
                                                    containsInInstallation = true;

                                            if (containsInInstallation is false && measuringPoint.IsActive)
                                                errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), TAG do ponto de medição: {measuringPoint.TagPointMeasuring} não encontrado na instalação: {installation.Name}");
                                        }

                                        var gasCalculation = await _gasCalculationRepository
                                            .GetGasVolumeCalculationByInstallationId(installation.Id);

                                        if (gasCalculation is null)
                                            errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), Gás de cálculo não configurado nesta instalação");

                                        if (gasCalculation is not null && gasCalculation.AssistanceGases.Count == 0 && gasCalculation.ExportGases.Count == 0 && gasCalculation.HighPressureGases.Count == 0 && gasCalculation.HPFlares.Count == 0 && gasCalculation.ImportGases.Count == 0 && gasCalculation.LowPressureGases.Count == 0 && gasCalculation.LPFlares.Count == 0 && gasCalculation.PilotGases.Count == 0 && gasCalculation.PurgeGases.Count == 0)
                                            errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), cálculo de gás não configurado nesta instalação, não foi encontrado nenhum ponto de medição vinculado ao cálculo");

                                        var containsInCalculation = false;
                                        var applicable = false;

                                        if (gasCalculation is not null)
                                        {
                                            if (gasCalculation.Installation.MeasuringPoints is null)
                                                errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), Não foram encontrados pontos de medição nesta instalação");

                                            foreach (var assistanceGas in gasCalculation.AssistanceGases)
                                                if (assistanceGas.IsActive && assistanceGas.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (assistanceGas.IsApplicable)
                                                        applicable = true;
                                                }


                                            foreach (var highPressure in gasCalculation.HighPressureGases)
                                                if (highPressure.IsActive && highPressure.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (highPressure.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var exportGas in gasCalculation.ExportGases)
                                                if (exportGas.IsActive && exportGas.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (exportGas.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var importGas in gasCalculation.ImportGases)
                                                if (importGas.IsActive && importGas.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (importGas.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var hpFlare in gasCalculation.HPFlares)
                                                if (hpFlare.IsActive && hpFlare.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (hpFlare.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var lowPressure in gasCalculation.LowPressureGases)
                                                if (lowPressure.IsActive && lowPressure.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (lowPressure.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var lpFlare in gasCalculation.LPFlares)
                                                if (lpFlare.IsActive && lpFlare.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (lpFlare.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var pilotGas in gasCalculation.PilotGases)
                                                if (pilotGas.IsActive && pilotGas.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (pilotGas.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var purgeGas in gasCalculation.PurgeGases)
                                                if (purgeGas.IsActive && purgeGas.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;

                                                    if (purgeGas.IsApplicable)
                                                        applicable = true;
                                                }
                                        }

                                        if (errorsInImport.Count == 0 && applicable)
                                        {

                                            var measurement = new Measurement()
                                            {
                                                Id = Guid.NewGuid(),

                                                #region atributos dados basicos
                                                NUM_SERIE_ELEMENTO_PRIMARIO_002 = dadosBasicos?.NUM_SERIE_ELEMENTO_PRIMARIO_002,
                                                COD_INSTALACAO_002 = dadosBasicos?.COD_INSTALACAO_002,
                                                COD_TAG_PONTO_MEDICAO_002 = dadosBasicos?.COD_TAG_PONTO_MEDICAO_002,
                                                #endregion

                                                #region configuracao cv
                                                NUM_SERIE_COMPUTADOR_VAZAO_002 = configuracaoCv?.NUM_SERIE_COMPUTADOR_VAZAO_002,
                                                DHA_COLETA_002 = XmlUtils.DateTimeParser(configuracaoCv?.DHA_COLETA_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                MED_TEMPERATURA_1_002 = XmlUtils.DecimalParser(configuracaoCv?.MED_TEMPERATURA_1_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                MED_PRESSAO_ATMSA_002 = XmlUtils.DecimalParser(configuracaoCv?.MED_PRESSAO_ATMSA_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                MED_PRESSAO_RFRNA_002 = XmlUtils.DecimalParser(configuracaoCv?.MED_PRESSAO_RFRNA_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                MED_DENSIDADE_RELATIVA_002 = XmlUtils.DecimalParser(configuracaoCv?.MED_DENSIDADE_RELATIVA_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                DSC_NORMA_UTILIZADA_CALCULO_002 = configuracaoCv?.DSC_NORMA_UTILIZADA_CALCULO_002,
                                                PCT_CROMATOGRAFIA_NITROGENIO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_NITROGENIO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),

                                                PCT_CROMATOGRAFIA_CO2_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_CO2_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_METANO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_METANO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_ETANO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_ETANO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_PROPANO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_PROPANO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_N_BUTANO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_N_BUTANO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_I_BUTANO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_I_BUTANO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_N_PENTANO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_N_PENTANO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_I_PENTANO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_I_PENTANO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_HEXANO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_HEXANO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_HEPTANO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_HEPTANO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_OCTANO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_OCTANO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_NONANO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_NONANO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_DECANO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_DECANO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_H2S_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_H2S_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_AGUA_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_AGUA_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_HELIO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_HELIO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_OXIGENIO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_OXIGENIO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_CO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_CO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_HIDROGENIO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_HIDROGENIO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_ARGONIO_002 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_ARGONIO_002, errorsInFormat, configuracaoCvElement?.Name.LocalName),

                                                DSC_VERSAO_SOFTWARE_002 = configuracaoCv?.DSC_VERSAO_SOFTWARE_002,

                                                #endregion

                                                #region elemento primario
                                                ICE_METER_FACTOR_1_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_1_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_METER_FACTOR_2_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_2_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_METER_FACTOR_3_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_3_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_METER_FACTOR_4_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_4_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_METER_FACTOR_5_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_5_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_METER_FACTOR_6_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_6_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_METER_FACTOR_7_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_7_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_METER_FACTOR_8_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_8_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_METER_FACTOR_9_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_9_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_METER_FACTOR_10_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_10_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_METER_FACTOR_11_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_11_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_METER_FACTOR_12_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_12_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_METER_FACTOR_13_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_13_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_METER_FACTOR_14_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_14_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_METER_FACTOR_15_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_METER_FACTOR_15_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),

                                                QTD_PULSOS_METER_FACTOR_1_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_1_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_METER_FACTOR_2_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_2_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_METER_FACTOR_3_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_3_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_METER_FACTOR_4_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_4_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_METER_FACTOR_5_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_5_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_METER_FACTOR_6_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_6_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_METER_FACTOR_7_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_7_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_METER_FACTOR_8_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_8_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_METER_FACTOR_9_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_9_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_METER_FACTOR_10_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_10_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_METER_FACTOR_11_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_11_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_METER_FACTOR_12_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_12_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_METER_FACTOR_13_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_13_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_METER_FACTOR_14_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_14_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_METER_FACTOR_15_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_METER_FACTOR_15_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),

                                                ICE_K_FACTOR_1_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_1_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_K_FACTOR_2_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_2_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_K_FACTOR_3_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_3_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_K_FACTOR_4_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_4_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_K_FACTOR_5_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_5_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_K_FACTOR_6_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_6_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_K_FACTOR_7_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_7_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_K_FACTOR_8_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_8_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_K_FACTOR_9_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_9_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_K_FACTOR_10_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_10_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_K_FACTOR_11_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_11_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_K_FACTOR_12_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_12_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_K_FACTOR_13_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_13_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_K_FACTOR_14_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_14_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_K_FACTOR_15_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_K_FACTOR_15_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),

                                                QTD_PULSOS_K_FACTOR_1_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_1_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_K_FACTOR_2_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_2_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_K_FACTOR_3_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_3_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_K_FACTOR_4_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_4_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_K_FACTOR_5_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_5_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_K_FACTOR_6_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_6_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_K_FACTOR_7_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_7_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_K_FACTOR_8_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_8_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_K_FACTOR_9_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_9_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_K_FACTOR_10_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_10_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_K_FACTOR_11_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_11_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_K_FACTOR_12_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_12_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_K_FACTOR_13_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_13_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_K_FACTOR_14_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_14_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                QTD_PULSOS_K_FACTOR_15_002 = XmlUtils.DecimalParser(elementoPrimario?.QTD_PULSOS_K_FACTOR_15_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),

                                                ICE_CUTOFF_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_CUTOFF_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_LIMITE_SPRR_ALARME_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_LIMITE_SPRR_ALARME_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_LIMITE_INFRR_ALARME_002 = XmlUtils.DecimalParser(elementoPrimario?.ICE_LIMITE_INFRR_ALARME_002, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                IND_HABILITACAO_ALARME_1_002 = elementoPrimario?.IND_HABILITACAO_ALARME_1_002,
                                                #endregion

                                                #region instrumento pressao

                                                NUM_SERIE_1_002 = instrumentoPressao?.NUM_SERIE_1_002,
                                                MED_PRSO_LIMITE_SPRR_ALRME_002 = XmlUtils.DecimalParser(instrumentoPressao?.MED_PRSO_LIMITE_SPRR_ALRME_002, errorsInFormat, instrumentoPressaoElement?.Name.LocalName),
                                                MED_PRSO_LMTE_INFRR_ALRME_002 = XmlUtils.DecimalParser(instrumentoPressao?.MED_PRSO_LMTE_INFRR_ALRME_002, errorsInFormat, instrumentoPressaoElement?.Name.LocalName),
                                                IND_HABILITACAO_ALARME_2_002 = instrumentoPressao?.IND_HABILITACAO_ALARME_2_002,
                                                MED_PRSO_ADOTADA_FALHA_002 = XmlUtils.DecimalParser(instrumentoPressao?.MED_PRSO_ADOTADA_FALHA_002, errorsInFormat, instrumentoPressaoElement?.Name.LocalName),
                                                DSC_ESTADO_INSNO_CASO_FALHA_002 = instrumentoPressao?.DSC_ESTADO_INSNO_CASO_FALHA_002,
                                                IND_TIPO_PRESSAO_CONSIDERADA_002 = instrumentoPressao?.IND_TIPO_PRESSAO_CONSIDERADA_002,

                                                #endregion

                                                #region instrumento temperatura
                                                NUM_SERIE_2_002 = instrumentoTemperatura?.NUM_SERIE_2_002,
                                                MED_TMPTA_SPRR_ALARME_002 = XmlUtils.DecimalParser(instrumentoTemperatura?.MED_TMPTA_SPRR_ALARME_002, errorsInFormat, instrumentoTemperaturaElement?.Name.LocalName),
                                                MED_TMPTA_INFRR_ALRME_002 = XmlUtils.DecimalParser(instrumentoTemperatura?.MED_TMPTA_INFRR_ALRME_002, errorsInFormat, instrumentoTemperaturaElement?.Name.LocalName),
                                                IND_HABILITACAO_ALARME_3_002 = instrumentoTemperatura?.IND_HABILITACAO_ALARME_3_002,
                                                MED_TMPTA_ADTTA_FALHA_002 = XmlUtils.DecimalParser(instrumentoTemperatura?.MED_TMPTA_ADTTA_FALHA_002, errorsInFormat, instrumentoTemperaturaElement?.Name.LocalName),
                                                DSC_ESTADO_INSTRUMENTO_FALHA_002 = instrumentoTemperatura?.DSC_ESTADO_INSTRUMENTO_FALHA_002,

                                                #endregion

                                                #region producao
                                                DHA_INICIO_PERIODO_MEDICAO_002 = XmlUtils.DateTimeParser(producao?.DHA_INICIO_PERIODO_MEDICAO_002, errorsInFormat, producaoElement?.Name.LocalName),
                                                DHA_FIM_PERIODO_MEDICAO_002 = XmlUtils.DateTimeParser(producao?.DHA_FIM_PERIODO_MEDICAO_002, errorsInFormat, producaoElement?.Name.LocalName),
                                                ICE_DENSIDADE_RELATIVA_002 = XmlUtils.DecimalParser(producao?.ICE_DENSIDADE_RELATIVA_002, errorsInFormat, producaoElement?.Name.LocalName),
                                                MED_PRESSAO_ESTATICA_002 = XmlUtils.DecimalParser(producao?.MED_PRESSAO_ESTATICA_002, errorsInFormat, producaoElement?.Name.LocalName),
                                                MED_TEMPERATURA_2_002 = XmlUtils.DecimalParser(producao?.MED_TEMPERATURA_2_002, errorsInFormat, producaoElement?.Name.LocalName),
                                                PRZ_DURACAO_FLUXO_EFETIVO_002 = XmlUtils.DecimalParser(producao?.PRZ_DURACAO_FLUXO_EFETIVO_002, errorsInFormat, producaoElement?.Name.LocalName),
                                                MED_BRUTO_MOVIMENTADO_002 = XmlUtils.DecimalParser(producao?.MED_BRUTO_MOVIMENTADO_002, errorsInFormat, producaoElement?.Name.LocalName) * 1000,
                                                MED_CORRIGIDO_MVMDO_002 = XmlUtils.DecimalParser(producao?.MED_CORRIGIDO_MVMDO_002, errorsInFormat, producaoElement?.Name.LocalName) * 1000,
                                                #endregion

                                                FileName = data.Files[i].FileName,
                                                FileType = new FileType
                                                {
                                                    Name = data.Files[i].FileType,
                                                    Acronym = XmlUtils.FileAcronym002,
                                                    ImportId = importId,

                                                },
                                                User = user,
                                                Installation = installation,
                                                MeasuringPoint = measuringPoint

                                            };

                                            var measurement002DTO = _mapper.Map<Measurement, Client002DTO>(measurement);

                                            measurement002DTO.ImportId = importId;
                                            measurement002DTO.Summary = new ClientInfo
                                            {
                                                Date = dateBeginningMeasurement,
                                                Status = containsInCalculation,
                                                LocationMeasuringPoint = measuringPoint.DinamicLocalMeasuringPoint,
                                                TagMeasuringPoint = measuringPoint.TagPointMeasuring,
                                                Volume = measurement.MED_CORRIGIDO_MVMDO_002,

                                            };
                                            response.UepCode = installation.UepCod;
                                            response.InstallationId = installation.Id;
                                            response.UepName = installation.UepName;
                                            response.DateProduction = dateBeginningMeasurement.ToString("dd/MM/yyyy");
                                            response002.Measurements.Add(measurement002DTO);
                                        }
                                    }


                                }

                                break;
                            }

                        #endregion

                        #region 003
                        case "003":
                            {
                                #region elementos XML
                                var dadosBasicos = dadosBasicosElement is not null ? Functions.DeserializeXml<DADOS_BASICOS_003>(dadosBasicosElement) : null;

                                var configuracaoCvElement = dadosBasicosElement?.Element("LISTA_CONFIGURACAO_CV")?.Element("CONFIGURACAO_CV");
                                var configuracaoCv = configuracaoCvElement is not null ? Functions.DeserializeXml<CONFIGURACAO_CV_003>(configuracaoCvElement) : null;

                                var elementoPrimarioElement = dadosBasicosElement?.Element("LISTA_ELEMENTO_PRIMARIO")?.Element("ELEMENTO_PRIMARIO");
                                var elementoPrimario = elementoPrimarioElement is not null ? Functions.DeserializeXml<ELEMENTO_PRIMARIO_003>(elementoPrimarioElement) : null;

                                var instrumentoPressaoElement = dadosBasicosElement?.Element("LISTA_INSTRUMENTO_PRESSAO")?.Element("INSTRUMENTO_PRESSAO");
                                var instrumentoPressao = instrumentoPressaoElement is not null ? Functions.DeserializeXml<INSTRUMENTO_PRESSAO_003>(instrumentoPressaoElement) : null;

                                var instrumentoTemperaturaElement = dadosBasicosElement?.Element("LISTA_INSTRUMENTO_TEMPERATURA")?.Element("INSTRUMENTO_TEMPERATURA");
                                var instrumentoTemperatura = instrumentoTemperaturaElement is not null ? Functions.DeserializeXml<INSTRUMENTO_TEMPERATURA_003>(instrumentoTemperaturaElement) : null;

                                var placaOrificioElement = dadosBasicosElement?.Element("LISTA_PLACA_ORIFICIO")?.Element("PLACA_ORIFICIO");
                                var placaOrificio = placaOrificioElement is not null ? Functions.DeserializeXml<PLACA_ORIFICIO_003>(placaOrificioElement) : null;

                                var instDiferenPressaoAltaElement = dadosBasicosElement?.Element("LISTA_INST_DIFEREN_PRESSAO_ALTA")?.Element("INST_DIFEREN_PRESSAO_ALTA");
                                var instDiferenPressaoAlta = instDiferenPressaoAltaElement is not null ? Functions.DeserializeXml<INST_DIFEREN_PRESSAO_ALTA_003>(instDiferenPressaoAltaElement) : null;

                                var instDiferenPressaoMediaElement = dadosBasicosElement?.Element("LISTA_INST_DIFEREN_PRESSAO_MEDIA")?.Element("INST_DIFEREN_PRESSAO_MEDIA");
                                var instDiferenPressaoMedia = instDiferenPressaoMediaElement is not null ? Functions.DeserializeXml<INST_DIFEREN_PRESSAO_MEDIA_003>(instDiferenPressaoMediaElement) : null;

                                var instDiferenPressaoBaixaElement = dadosBasicosElement?.Element("LISTA_INST_DIFEREN_PRESSAO_BAIXA")?.Element("INST_DIFEREN_PRESSAO_BAIXA");
                                var instDiferenPressaoBaixa = instDiferenPressaoBaixaElement is not null ? Functions.DeserializeXml<INST_DIFEREN_PRESSAO_BAIXA_003>(instDiferenPressaoBaixaElement) : null;

                                var instDiferenPressaoPrincipalElement = dadosBasicosElement?.Element("LISTA_INST_DIFEREN_PRESSAO_PRINCIPAL")?.Element("INST_DIFEREN_PRESSAO_PRINCIPAL");
                                var instDiferenPressaoPrincipal = instDiferenPressaoPrincipalElement is not null ? Functions.DeserializeXml<INST_DIFEREN_PRESSAO_PRINCIPAL_003>(instDiferenPressaoPrincipalElement) : null;

                                var producaoElement = dadosBasicosElement?.Element("LISTA_PRODUCAO")?.Element("PRODUCAO");
                                var producao = producaoElement is not null ? Functions.DeserializeXml<PRODUCAO_003>(producaoElement) : null;
                                #endregion

                                if (dadosBasicos is not null && dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_003 is not null && dadosBasicos.COD_INSTALACAO_003 is not null && producao is not null && producao.DHA_INICIO_PERIODO_MEDICAO_003 is not null)
                                {

                                    if (DateTime.TryParseExact(producao?.DHA_INICIO_PERIODO_MEDICAO_003, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateBeginningMeasurement))
                                    {
                                        listOfMeasurementDates.Add(dateBeginningMeasurement.Date);

                                        var checkDateExists = await _repository.GetMeasurementByDate(dateBeginningMeasurement, XmlUtils.File003);

                                        if (checkDateExists is not null && checkDateExists.IsActive)
                                            errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS) data: {producao.DHA_INICIO_PERIODO_MEDICAO_003} já existente.");

                                        var checkGasProductionExists = await _productionRepository
                                            .GetProductionGasByDate(dateBeginningMeasurement);

                                        if (checkGasProductionExists is not null && checkGasProductionExists.Gas is not null && checkGasProductionExists.IsActive)
                                            errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS) já existe produção de gás na data: {producao.DHA_INICIO_PERIODO_MEDICAO_003}");

                                    }
                                    else
                                    {
                                        errorsInFormat.Add("Formato da tag DHA_INICIO_PERIODO_MEDICAO incorreto deve ser: dd/MM/yyyy HH:mm:ss");
                                    }

                                    var installation = await _installationRepository.GetInstallationMeasurementByUepAndAnpCodAsync(dadosBasicos.COD_INSTALACAO_003, XmlUtils.File003);

                                    if (installation is null)
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): {ErrorMessages.NotFound<Installation>()}");

                                    if (installation is not null && installation.IsActive is false)
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): Instalação:{installation.Name} está inativa");

                                    var measuringPoint = await _measuringPointRepository
                                        .GetByTagMeasuringPointXML(dadosBasicos.COD_TAG_PONTO_MEDICAO_003, XmlUtils.File003);

                                    if (measuringPoint is not null && measuringPoint.IsActive is false)
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): Ponto de medição: {measuringPoint.TagPointMeasuring} está inativo.");

                                    //if (measuringPoint is null)
                                    //    errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), ponto de medição TAG: {dadosBasicos.COD_TAG_PONTO_MEDICAO_003}: {ErrorMessages.NotFound<MeasuringPoint>()}");

                                    if (installation is not null && measuringPoint is not null && installation.IsActive)
                                    {

                                        if (installation.MeasuringPoints is not null)
                                        {
                                            var containsInInstallation = false;

                                            foreach (var point in installation.MeasuringPoints)
                                                if (measuringPoint is not null && measuringPoint.TagPointMeasuring == point.TagPointMeasuring && measuringPoint.IsActive)
                                                    containsInInstallation = true;

                                            if (containsInInstallation is false && measuringPoint.IsActive)
                                                errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), TAG do ponto de medição: {measuringPoint.TagPointMeasuring} não encontrado na instalação: {installation.Name}");
                                        }

                                        var gasCalculation = await _gasCalculationRepository
                                            .GetGasVolumeCalculationByInstallationId(installation.Id);

                                        if (gasCalculation is null)
                                            errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), Gás de cálculo não configurado nesta instalação");

                                        if (gasCalculation is not null && gasCalculation.AssistanceGases.Count == 0 && gasCalculation.ExportGases.Count == 0 && gasCalculation.HighPressureGases.Count == 0 && gasCalculation.HPFlares.Count == 0 && gasCalculation.ImportGases.Count == 0 && gasCalculation.LowPressureGases.Count == 0 && gasCalculation.LPFlares.Count == 0 && gasCalculation.PilotGases.Count == 0 && gasCalculation.PurgeGases.Count == 0)
                                            errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), cálculo de gás não configurado nesta instalação, não foi encontrado nenhum ponto de medição vinculado ao cálculo");

                                        var containsInCalculation = false;
                                        var applicable = false;
                                        if (gasCalculation is not null)
                                        {
                                            foreach (var assistanceGas in gasCalculation.AssistanceGases)
                                                if (assistanceGas.IsActive && assistanceGas.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (assistanceGas.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var highPressure in gasCalculation.HighPressureGases)
                                                if (highPressure.IsActive && highPressure.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (highPressure.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var exportGas in gasCalculation.ExportGases)
                                                if (exportGas.IsActive && exportGas.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (exportGas.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var importGas in gasCalculation.ImportGases)
                                                if (importGas.IsActive && importGas.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (importGas.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var hpFlare in gasCalculation.HPFlares)
                                                if (hpFlare.IsActive && hpFlare.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (hpFlare.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var lowPressure in gasCalculation.LowPressureGases)
                                                if (lowPressure.IsActive && lowPressure.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (lowPressure.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var lpFlare in gasCalculation.LPFlares)
                                                if (lpFlare.IsActive && lpFlare.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (lpFlare.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var pilotGas in gasCalculation.PilotGases)
                                                if (pilotGas.IsActive && pilotGas.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;
                                                    if (pilotGas.IsApplicable)
                                                        applicable = true;
                                                }

                                            foreach (var purgeGas in gasCalculation.PurgeGases)
                                                if (purgeGas.IsActive && purgeGas.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                                                {
                                                    containsInCalculation = true;

                                                    if (purgeGas.IsApplicable)
                                                        applicable = true;
                                                }
                                        }

                                        if (errorsInImport.Count == 0 && applicable)
                                        {

                                            var measurement = new Measurement
                                            {
                                                Id = Guid.NewGuid(),

                                                #region atributos
                                                NUM_SERIE_ELEMENTO_PRIMARIO_003 = dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_003,
                                                COD_INSTALACAO_003 = dadosBasicos.COD_INSTALACAO_003,
                                                COD_TAG_PONTO_MEDICAO_003 = dadosBasicos.COD_TAG_PONTO_MEDICAO_003,
                                                #endregion

                                                #region configuracao cv

                                                NUM_SERIE_COMPUTADOR_VAZAO_003 = configuracaoCv?.NUM_SERIE_COMPUTADOR_VAZAO_003,
                                                DHA_COLETA_003 = XmlUtils.DateTimeParser(configuracaoCv?.DHA_COLETA_003, errorsInFormat, configuracaoCvElement?.Element("DHA_COLETA")?.Name.LocalName),
                                                MED_TEMPERATURA_1_003 = XmlUtils.DecimalParser(configuracaoCv?.MED_TEMPERATURA_1_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                MED_PRESSAO_ATMSA_003 = XmlUtils.DecimalParser(configuracaoCv?.MED_PRESSAO_ATMSA_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                MED_PRESSAO_RFRNA_003 = XmlUtils.DecimalParser(configuracaoCv?.MED_PRESSAO_RFRNA_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),

                                                MED_DENSIDADE_RELATIVA_003 = XmlUtils.DecimalParser(configuracaoCv?.MED_DENSIDADE_RELATIVA_003, errorsInFormat, configuracaoCvElement?.Element("MED_DENSIDADE_RELATIVA")?.Name.LocalName),
                                                DSC_NORMA_UTILIZADA_CALCULO_003 = configuracaoCv?.DSC_NORMA_UTILIZADA_CALCULO_003,

                                                PCT_CROMATOGRAFIA_NITROGENIO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_NITROGENIO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_CO2_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_CO2_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_METANO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_METANO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_ETANO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_ETANO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_PROPANO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_PROPANO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_N_BUTANO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_N_BUTANO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_I_BUTANO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_I_BUTANO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_N_PENTANO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_N_PENTANO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_I_PENTANO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_I_PENTANO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_HEXANO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_HEXANO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_HEPTANO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_HEPTANO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_OCTANO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_OCTANO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_NONANO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_NONANO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_DECANO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_DECANO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_H2S_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_H2S_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_AGUA_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_AGUA_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_HELIO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_HELIO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_OXIGENIO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_OXIGENIO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_CO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_CO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_HIDROGENIO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_HIDROGENIO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                PCT_CROMATOGRAFIA_ARGONIO_003 = XmlUtils.DecimalParser(configuracaoCv?.PCT_CROMATOGRAFIA_ARGONIO_003, errorsInFormat, configuracaoCvElement?.Name.LocalName),
                                                DSC_VERSAO_SOFTWARE_003 = configuracaoCv?.DSC_VERSAO_SOFTWARE_003,

                                                #endregion

                                                #region elemento primario
                                                CE_LIMITE_SPRR_ALARME_003 = XmlUtils.DecimalParser(elementoPrimario?.CE_LIMITE_SPRR_ALARME_003, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                ICE_LIMITE_INFRR_ALARME_003 = XmlUtils.DecimalParser(elementoPrimario?.ICE_LIMITE_INFRR_ALARME_1_003, errorsInFormat, elementoPrimarioElement?.Name.LocalName),
                                                IND_HABILITACAO_ALARME_1_003 = elementoPrimario?.IND_HABILITACAO_ALARME_1_003,

                                                #endregion

                                                #region instrumento pressao
                                                NUM_SERIE_1_003 = instrumentoPressao?.NUM_SERIE_1_003,
                                                MED_PRSO_LIMITE_SPRR_ALRME_1_003 = XmlUtils.DecimalParser(instrumentoPressao?.MED_PRSO_LIMITE_SPRR_ALRME_1_003, errorsInFormat, instrumentoPressaoElement?.Name.LocalName),
                                                MED_PRSO_LMTE_INFRR_ALRME_1_003 = XmlUtils.DecimalParser(instrumentoPressao?.MED_PRSO_LMTE_INFRR_ALRME_1_003, errorsInFormat, instrumentoPressaoElement?.Name.LocalName),
                                                MED_PRSO_ADOTADA_FALHA_1_003 = XmlUtils.DecimalParser(instrumentoPressao?.MED_PRSO_ADOTADA_FALHA_1_003, errorsInFormat, instrumentoPressaoElement?.Name.LocalName),
                                                DSC_ESTADO_INSNO_CASO_FALHA_1_003 = instrumentoPressao?.DSC_ESTADO_INSNO_CASO_FALHA_1_003,
                                                IND_TIPO_PRESSAO_CONSIDERADA_003 = instrumentoPressao?.IND_TIPO_PRESSAO_CONSIDERADA_003,
                                                IND_HABILITACAO_ALARME_2_003 = instrumentoPressao?.IND_HABILITACAO_ALARME_2_003,

                                                #endregion

                                                #region instrumento temperatura
                                                NUM_SERIE_2_003 = instrumentoTemperatura?.NUM_SERIE_2_003,
                                                MED_TMPTA_SPRR_ALARME_003 = XmlUtils.DecimalParser(instrumentoTemperatura?.MED_TMPTA_SPRR_ALARME_003, errorsInFormat, instrumentoTemperaturaElement?.Name.LocalName),
                                                MED_TMPTA_INFRR_ALRME_003 = XmlUtils.DecimalParser(instrumentoTemperatura?.MED_TMPTA_INFRR_ALRME_003, errorsInFormat, instrumentoTemperaturaElement?.Name.LocalName),
                                                IND_HABILITACAO_ALARME_3_003 = instrumentoTemperatura?.IND_HABILITACAO_ALARME_3_003,
                                                MED_TMPTA_ADTTA_FALHA_003 = XmlUtils.DecimalParser(instrumentoTemperatura?.MED_TMPTA_ADTTA_FALHA_003, errorsInFormat, instrumentoTemperaturaElement?.Name.LocalName),
                                                DSC_ESTADO_INSTRUMENTO_FALHA_003 = instrumentoTemperatura?.DSC_ESTADO_INSTRUMENTO_FALHA_003,
                                                #endregion

                                                #region placa orificio
                                                MED_DIAMETRO_REFERENCIA_003 = XmlUtils.DecimalParser(placaOrificio?.MED_DIAMETRO_REFERENCIA_003, errorsInFormat, placaOrificioElement?.Name.LocalName),
                                                MED_TEMPERATURA_RFRNA_003 = XmlUtils.DecimalParser(placaOrificio?.MED_TEMPERATURA_RFRNA_003, errorsInFormat, placaOrificioElement?.Name.LocalName),
                                                DSC_MATERIAL_CONTRUCAO_PLACA_003 = placaOrificio?.DSC_MATERIAL_CONTRUCAO_PLACA_003,
                                                MED_DMTRO_INTRO_TRCHO_MDCO_003 = XmlUtils.DecimalParser(placaOrificio?.MED_DMTRO_INTRO_TRCHO_MDCO_003, errorsInFormat, placaOrificioElement?.Name.LocalName),
                                                MED_TMPTA_TRCHO_MDCO_003 = XmlUtils.DecimalParser(placaOrificio?.MED_TMPTA_TRCHO_MDCO_003, errorsInFormat, placaOrificioElement?.Name.LocalName),
                                                DSC_MATERIAL_CNSTO_TRCHO_MDCO_003 = placaOrificio?.DSC_MATERIAL_CNSTO_TRCHO_MDCO_003,
                                                DSC_LCLZO_TMDA_PRSO_DFRNL_003 = placaOrificio?.DSC_LCLZO_TMDA_PRSO_DFRNL_003,
                                                IND_TOMADA_PRESSAO_ESTATICA_003 = placaOrificio?.IND_TOMADA_PRESSAO_ESTATICA_003,
                                                #endregion

                                                #region inst diferen pressao alta   
                                                NUM_SERIE_3_003 = instDiferenPressaoAlta?.NUM_SERIE_3_003,
                                                MED_PRSO_LIMITE_SPRR_ALRME_2_003 = XmlUtils.DecimalParser(instDiferenPressaoAlta?.MED_PRSO_LIMITE_SPRR_ALRME_2_003, errorsInFormat, instDiferenPressaoAltaElement?.Name.LocalName),
                                                MED_PRSO_LMTE_INFRR_ALRME_2_003 = XmlUtils.DecimalParser(instDiferenPressaoAlta?.MED_PRSO_LMTE_INFRR_ALRME_2_003, errorsInFormat, instDiferenPressaoAltaElement?.Name.LocalName),

                                                #endregion

                                                #region inst diferen pressao media
                                                NUM_SERIE_4_003 = instDiferenPressaoMedia?.NUM_SERIE_4_003,
                                                MED_PRSO_LIMITE_SPRR_ALRME_3_003 = XmlUtils.DecimalParser(instDiferenPressaoMedia?.MED_PRSO_LIMITE_SPRR_ALRME_3_003, errorsInFormat, instDiferenPressaoMediaElement?.Name.LocalName),
                                                MED_PRSO_LMTE_INFRR_ALRME_3_003 = XmlUtils.DecimalParser(instDiferenPressaoMedia?.MED_PRSO_LMTE_INFRR_ALRME_3_003, errorsInFormat, instDiferenPressaoMediaElement?.Name.LocalName),
                                                #endregion

                                                #region inst diferen pressao baixa
                                                NUM_SERIE_5_003 = instDiferenPressaoBaixa?.NUM_SERIE_5_003,
                                                MED_PRSO_LIMITE_SPRR_ALRME_4_003 = XmlUtils.DecimalParser(instDiferenPressaoBaixa?.MED_PRSO_LIMITE_SPRR_ALRME_4_003, errorsInFormat, instDiferenPressaoBaixaElement?.Name.LocalName),
                                                MED_PRSO_LMTE_INFRR_ALRME_4_003 = XmlUtils.DecimalParser(instDiferenPressaoBaixa?.MED_PRSO_LMTE_INFRR_ALRME_4_003, errorsInFormat, instDiferenPressaoBaixaElement?.Name.LocalName),
                                                IND_HABILITACAO_ALARME_4_003 = instDiferenPressaoBaixa?.IND_HABILITACAO_ALARME_4_003,
                                                MED_PRSO_ADOTADA_FALHA_2_003 = XmlUtils.DecimalParser(instDiferenPressaoBaixa?.MED_PRSO_ADOTADA_FALHA_2_003, errorsInFormat, instDiferenPressaoBaixaElement?.Name.LocalName),
                                                DSC_ESTADO_INSNO_CASO_FALHA_2_003 = instDiferenPressaoBaixa?.DSC_ESTADO_INSNO_CASO_FALHA_2_003,
                                                MED_CUTOFF_KPA_1_003 = XmlUtils.DecimalParser(instDiferenPressaoBaixa?.MED_CUTOFF_KPA_1_003, errorsInFormat, instDiferenPressaoBaixaElement?.Name.LocalName),

                                                #endregion

                                                #region inst diferen pressao principal
                                                NUM_SERIE_6_003 = instDiferenPressaoPrincipal?.NUM_SERIE_6_003,
                                                MED_PRSO_LIMITE_SPRR_ALRME_5_003 = XmlUtils.DecimalParser(instDiferenPressaoPrincipal?.MED_PRSO_LIMITE_SPRR_ALRME_5_003, errorsInFormat, instDiferenPressaoPrincipalElement?.Name.LocalName),
                                                MED_PRSO_LMTE_INFRR_ALRME_5_003 = XmlUtils.DecimalParser(instDiferenPressaoPrincipal?.MED_PRSO_LMTE_INFRR_ALRME_5_003, errorsInFormat, instDiferenPressaoPrincipalElement?.Name.LocalName),
                                                IND_HABILITACAO_ALARME_5_003 = instDiferenPressaoPrincipal?.IND_HABILITACAO_ALARME_5_003,
                                                MED_PRSO_ADOTADA_FALHA_3_003 = XmlUtils.DecimalParser(instDiferenPressaoPrincipal?.MED_PRSO_ADOTADA_FALHA_3_003, errorsInFormat, instDiferenPressaoPrincipalElement?.Name.LocalName),
                                                DSC_ESTADO_INSNO_CASO_FALHA_3_003 = instDiferenPressaoPrincipal?.DSC_ESTADO_INSNO_CASO_FALHA_3_003,
                                                MED_CUTOFF_KPA_2_003 = XmlUtils.DecimalParser(instDiferenPressaoPrincipal?.MED_CUTOFF_KPA_2_003, errorsInFormat, instDiferenPressaoPrincipalElement?.Name.LocalName),
                                                #endregion

                                                #region producao
                                                DHA_INICIO_PERIODO_MEDICAO_003 = XmlUtils.DateTimeParser(producao?.DHA_INICIO_PERIODO_MEDICAO_003, errorsInFormat, producaoElement?.Name.LocalName),
                                                DHA_FIM_PERIODO_MEDICAO_003 = XmlUtils.DateTimeParser(producao?.DHA_FIM_PERIODO_MEDICAO_003, errorsInFormat, producaoElement?.Name.LocalName),
                                                ICE_DENSIDADE_RELATIVA_003 = XmlUtils.DecimalParser(producao?.ICE_DENSIDADE_RELATIVA_003, errorsInFormat, producaoElement?.Name.LocalName),
                                                MED_DIFERENCIAL_PRESSAO_003 = XmlUtils.DecimalParser(producao?.MED_DIFERENCIAL_PRESSAO_003, errorsInFormat, producaoElement?.Name.LocalName),
                                                MED_PRESSAO_ESTATICA_003 = XmlUtils.DecimalParser(producao?.MED_PRESSAO_ESTATICA_003, errorsInFormat, producaoElement?.Name.LocalName),
                                                MED_TEMPERATURA_2_003 = XmlUtils.DecimalParser(producao?.MED_TEMPERATURA_2_003, errorsInFormat, producaoElement?.Name.LocalName),
                                                PRZ_DURACAO_FLUXO_EFETIVO_003 = XmlUtils.DecimalParser(producao?.PRZ_DURACAO_FLUXO_EFETIVO_003, errorsInFormat, producaoElement?.Name.LocalName),
                                                MED_CORRIGIDO_MVMDO_003 = XmlUtils.DecimalParser(producao?.MED_CORRIGIDO_MVMDO_003, errorsInFormat, producaoElement?.Name.LocalName),
                                                #endregion

                                                FileName = data.Files[i].FileName,
                                                FileType = new FileType
                                                {
                                                    Name = data.Files[i].FileType,
                                                    Acronym = XmlUtils.FileAcronym003,
                                                    ImportId = importId,

                                                },

                                                Installation = installation,
                                                User = user,
                                                MeasuringPoint = measuringPoint,
                                            };

                                            var measurement003DTO = _mapper.Map<Measurement, Client003DTO>(measurement);
                                            measurement003DTO.ImportId = importId;
                                            measurement003DTO.Summary = new ClientInfo
                                            {
                                                Date = dateBeginningMeasurement,
                                                Status = containsInCalculation,
                                                LocationMeasuringPoint = measuringPoint.DinamicLocalMeasuringPoint,
                                                TagMeasuringPoint = measuringPoint.TagPointMeasuring,
                                                Volume = measurement.MED_CORRIGIDO_MVMDO_003,

                                            };
                                            response.InstallationId = installation.Id;
                                            response.UepName = installation.UepName;
                                            response.UepCode = installation.UepCod;
                                            response.DateProduction = dateBeginningMeasurement.ToString("dd/MM/yyyy");
                                            response003.Measurements.Add(measurement003DTO);
                                        }
                                    }

                                }
                            }

                            break;
                            #endregion
                    }
                }


                if (errorsInImport.Count > 0)
                    throw new BadRequestException($"Algum(s) erro(s) ocorreram durante a validação do arquivo de nome: {data.Files[i].FileName}", errors: errorsInImport);

                if (errorsInFormat.Count > 0)
                    throw new BadRequestException($"Algum(s) erro(s) de formatação ocorreram durante a validação do arquivo de nome: {data.Files[i].FileName}", errors: errorsInFormat);

                if (response003.Measurements.Count == 0 && response002.Measurements.Count == 0 && response001.Measurements.Count == 0)
                    errorsInImport.Add($"Algum erro ocorreu na hora de achar a configuração de cálculo, certifique-se que os pontos de medição do xml estão associados corretamente na configuração de cálculo dos fluídos, arquivo de nome: {data.Files[i].FileName}");

                if (response003.Measurements.Count > 0)
                {
                    response._003File.Add(response003);
                }

                if (response002.Measurements.Count > 0)
                {
                    response._002File.Add(response002);

                }

                if (response001.Measurements.Count > 0)
                {
                    response._001File.Add(response001);
                }
            }


            if (listOfMeasurementDates.Count > 0)
            {
                var referenceDate = listOfMeasurementDates[0];

                var hasDifferentDates = listOfMeasurementDates.Any(dateMeasured => dateMeasured != referenceDate);

                if (hasDifferentDates)
                {
                    throw new BadRequestException("Datas entre medições diferente.");
                }

            }

            decimal totalLinearBurnetGas = 0;
            decimal totalLinearFuelGas = 0;
            decimal totalLinearExportedGas = 0;
            decimal totalLinearImportedGas = 0;

            decimal totalDiferencialBurnetGas = 0;
            decimal totalDiferencialFuelGas = 0;
            decimal totalDiferencialExportedGas = 0;
            decimal totalDiferencialImportedGas = 0;

            decimal totalOil = 0;
            decimal totalOilWithoutBsw = 0;
            var bswTotal = 0m;
            var divideBsw = 1;


            if (response._001File.Any())
            {
                var oilCalculationByUepCode = await _oilCalculationRepository
                    .GetOilVolumeCalculationByInstallationUEP(response._001File[0].Measurements[0].COD_INSTALACAO_001);

                if (oilCalculationByUepCode is null)
                    throw new NotFoundException("Cálculo de gás não encontrado");

                foreach (var file001 in response._001File)
                {
                    foreach (var measurement in file001.Measurements)
                    {

                        bswTotal += measurement.BswManual;
                        divideBsw++;
                        totalOilWithoutBsw += measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001;

                    }

                    foreach (var drain in oilCalculationByUepCode.DrainVolumes)
                    {
                        for (int i = 0; i < file001.Measurements.Count; ++i)
                        {
                            var measurementResponse = file001.Measurements[i];

                            if (drain.IsApplicable && drain.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_001)
                            {
                                totalOil += measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001;
                                break;
                            }
                        }

                        //if (containDrain is false && response._001File.Count > 0)
                        //{
                        //    var measurementWrong = new Client001DTO
                        //    {
                        //        DHA_INICIO_PERIODO_MEDICAO_001 = file001.Measurements[0].DHA_INICIO_PERIODO_MEDICAO_001,
                        //        COD_INSTALACAO_001 = file001.Measurements[0].COD_INSTALACAO_001,
                        //        COD_TAG_PONTO_MEDICAO_001 = drain.MeasuringPoint.TagPointMeasuring,
                        //        Summary = new ClientInfo
                        //        {
                        //            Status = false,
                        //            Date = file001.Measurements[0].DHA_INICIO_PERIODO_MEDICAO_001,
                        //            LocationMeasuringPoint = drain.MeasuringPoint.DinamicLocalMeasuringPoint,
                        //            TagMeasuringPoint = drain.MeasuringPoint.TagPointMeasuring,
                        //            Volume = 0
                        //        }
                        //    };
                        //    file001.Measurements.Add(measurementWrong);
                        //}
                    }

                    foreach (var dor in oilCalculationByUepCode.DORs)
                    {
                        for (int i = 0; i < file001.Measurements.Count; ++i)
                        {

                            var measurementResponse = file001.Measurements[i];

                            if (dor.IsApplicable && dor.MeasuringPoint.TagPointMeasuring.ToUpper() == measurementResponse.COD_TAG_PONTO_MEDICAO_001.ToUpper())
                            {
                                totalOil -= measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001 * (1 - measurementResponse.BswManual);
                                break;
                            }
                        }

                        //if (containDOR is false && response._001File.Count > 0)
                        //{
                        //    var measurementWrong = new Client001DTO
                        //    {
                        //        DHA_INICIO_PERIODO_MEDICAO_001 = file001.Measurements[0].DHA_INICIO_PERIODO_MEDICAO_001,
                        //        COD_INSTALACAO_001 = file001.Measurements[0].COD_INSTALACAO_001,
                        //        COD_TAG_PONTO_MEDICAO_001 = dor.MeasuringPoint.TagPointMeasuring,
                        //        Summary = new ClientInfo
                        //        {
                        //            Status = false,
                        //            Date = file001.Measurements[0].DHA_INICIO_PERIODO_MEDICAO_001,
                        //            LocationMeasuringPoint = dor.MeasuringPoint.DinamicLocalMeasuringPoint,
                        //            TagMeasuringPoint = dor.MeasuringPoint.TagPointMeasuring,
                        //            Volume = 0
                        //        }
                        //    };
                        //    file001.Measurements.Add(measurementWrong);
                        //}
                    }

                    foreach (var section in oilCalculationByUepCode.Sections)
                    {
                        for (int i = 0; i < file001.Measurements.Count; ++i)
                        {
                            var measurementResponse = file001.Measurements[i];

                            if (section.IsApplicable && section.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_001)
                            {
                                totalOil += measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001 * (1 - measurementResponse.BswManual);
                                break;
                            }
                        }

                        //if (containSection is false && response._001File.Count > 0)
                        //{
                        //    var measurementWrong = new Client001DTO
                        //    {
                        //        DHA_INICIO_PERIODO_MEDICAO_001 = file001.Measurements[0].DHA_INICIO_PERIODO_MEDICAO_001,
                        //        COD_INSTALACAO_001 = file001.Measurements[0].COD_INSTALACAO_001,
                        //        COD_TAG_PONTO_MEDICAO_001 = section.MeasuringPoint.TagPointMeasuring,
                        //        Summary = new ClientInfo
                        //        {
                        //            Status = false,
                        //            Date = file001.Measurements[0].DHA_INICIO_PERIODO_MEDICAO_001,
                        //            LocationMeasuringPoint = section.MeasuringPoint.DinamicLocalMeasuringPoint,
                        //            TagMeasuringPoint = section.MeasuringPoint.TagPointMeasuring,
                        //            Volume = 0
                        //        }
                        //    };

                        //    file001.Measurements.Add(measurementWrong);
                        //}
                    }

                    foreach (var togRecovered in oilCalculationByUepCode.TOGRecoveredOils)
                    {
                        for (int i = 0; i < file001.Measurements.Count; ++i)
                        {
                            var measurementResponse = file001.Measurements[i];

                            if (togRecovered.IsApplicable && togRecovered.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_001)
                            {
                                totalOil += measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001;

                                break;
                            }
                        }

                        //if (containTOGRecoveredOil is false && response._001File.Count > 0)
                        //{
                        //    var measurementWrong = new Client001DTO
                        //    {
                        //        DHA_INICIO_PERIODO_MEDICAO_001 = file001.Measurements[0].DHA_INICIO_PERIODO_MEDICAO_001,
                        //        COD_INSTALACAO_001 = file001.Measurements[0].COD_INSTALACAO_001,
                        //        COD_TAG_PONTO_MEDICAO_001 = togRecovered.MeasuringPoint.TagPointMeasuring,
                        //        Summary = new ClientInfo
                        //        {
                        //            Status = false,
                        //            Date = file001.Measurements[0].DHA_INICIO_PERIODO_MEDICAO_001,
                        //            LocationMeasuringPoint = togRecovered.MeasuringPoint.DinamicLocalMeasuringPoint,
                        //            TagMeasuringPoint = togRecovered.MeasuringPoint.TagPointMeasuring,
                        //            Volume = 0
                        //        }
                        //    };
                        //    file001.Measurements.Add(measurementWrong);


                        //}
                    }
                }

                foreach (var section in oilCalculationByUepCode.Sections)
                {
                    var containsSection = false;

                    foreach (var file in response._001File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_001 == section.MeasuringPoint.TagPointMeasuring && section.IsApplicable))
                        {
                            containsSection = true;
                        }
                    }

                    if (containsSection is false && response._001File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == section.MeasuringPoint.TagPointMeasuring))
                    {

                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._001File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_001.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = section.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = section.MeasuringPoint.TagPointMeasuring,
                            Volume = 0,
                            Fluid = "Oil"
                        });
                    }
                }

                foreach (var drain in oilCalculationByUepCode.DrainVolumes)
                {
                    var containsDrain = false;

                    foreach (var file in response._001File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_001 == drain.MeasuringPoint.TagPointMeasuring && drain.IsApplicable))
                        {
                            containsDrain = true;
                        }
                    }

                    if (containsDrain is false && response._001File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == drain.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._001File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_001.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = drain.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = drain.MeasuringPoint.TagPointMeasuring,
                            Volume = 0,
                            Fluid = "Oil"
                        });
                    }
                }

                foreach (var dor in oilCalculationByUepCode.DORs)
                {
                    var containsDor = false;

                    foreach (var file in response._001File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_001 == dor.MeasuringPoint.TagPointMeasuring && dor.IsApplicable))
                        {
                            containsDor = true;
                        }
                    }

                    if (containsDor is false && response._001File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == dor.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._001File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_001.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = dor.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = dor.MeasuringPoint.TagPointMeasuring,
                            Volume = 0,
                            Fluid = "Oil"
                        });
                    }
                }

                foreach (var togRecovered in oilCalculationByUepCode.TOGRecoveredOils)
                {
                    var containsTog = false;

                    foreach (var file in response._001File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_001 == togRecovered.MeasuringPoint.TagPointMeasuring && togRecovered.IsApplicable))
                        {
                            containsTog = true;
                        }
                    }

                    if (containsTog is false && response._001File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == togRecovered.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._001File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_001.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = togRecovered.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = togRecovered.MeasuringPoint.TagPointMeasuring,
                            Volume = 0,
                            Fluid = "Oil"
                        });
                    }
                }

            }

            if (response._002File.Any())
            {
                var gasCalculationByUepCode = await _gasCalculationRepository
              .GetGasVolumeCalculationByInstallationUEP(response._002File[0].Measurements[0].COD_INSTALACAO_002);

                if (gasCalculationByUepCode is null)
                    throw new NotFoundException("Cálculo de gás não encontrado");

                foreach (var file002 in response._002File)
                {
                    foreach (var assistanceGas in gasCalculationByUepCode.AssistanceGases)
                    {
                        for (int i = 0; i < file002.Measurements.Count; ++i)
                        {
                            var measurementResponse = file002.Measurements[i];

                            if (assistanceGas.IsApplicable && assistanceGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002)
                            {
                                totalLinearBurnetGas += measurementResponse.MED_CORRIGIDO_MVMDO_002;
                                gasResume.BurnedGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }
                    }

                    foreach (var exportGas in gasCalculationByUepCode.ExportGases)
                    {
                        for (int i = 0; i < file002.Measurements.Count; ++i)
                        {
                            var measurementResponse = file002.Measurements[i];

                            if (exportGas.IsApplicable && exportGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002)
                            {
                                totalLinearExportedGas += measurementResponse.MED_CORRIGIDO_MVMDO_002;
                                gasResume.ExportedGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }
                    }

                    foreach (var highPressureGas in gasCalculationByUepCode.HighPressureGases)
                    {
                        for (int i = 0; i < file002.Measurements.Count; ++i)
                        {
                            var measurementResponse = file002.Measurements[i];

                            if (highPressureGas.IsApplicable && highPressureGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002)
                            {
                                totalLinearFuelGas += measurementResponse.MED_CORRIGIDO_MVMDO_002;

                                gasResume.FuelGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }

                    }

                    foreach (var hpFlare in gasCalculationByUepCode.HPFlares)
                    {
                        for (int i = 0; i < file002.Measurements.Count; ++i)
                        {
                            var measurementResponse = file002.Measurements[i];

                            if (hpFlare.IsApplicable && hpFlare.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002)
                            {
                                totalLinearBurnetGas += measurementResponse.MED_CORRIGIDO_MVMDO_002;

                                gasResume.BurnedGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }
                    }

                    foreach (var importGas in gasCalculationByUepCode.ImportGases)
                    {
                        for (int i = 0; i < file002.Measurements.Count; ++i)
                        {
                            var measurementResponse = file002.Measurements[i];

                            if (importGas.IsApplicable && importGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002)
                            {
                                totalLinearImportedGas += measurementResponse.MED_CORRIGIDO_MVMDO_002;

                                gasResume.ImportedGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }
                    }

                    foreach (var lowPressure in gasCalculationByUepCode.LowPressureGases)
                    {
                        for (int i = 0; i < file002.Measurements.Count; ++i)
                        {
                            var measurementResponse = file002.Measurements[i];

                            if (lowPressure.IsApplicable && lowPressure.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002)
                            {
                                totalLinearFuelGas += measurementResponse.MED_CORRIGIDO_MVMDO_002;

                                gasResume.FuelGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }
                    }

                    foreach (var lpFlare in gasCalculationByUepCode.LPFlares)
                    {
                        for (int i = 0; i < file002.Measurements.Count; ++i)
                        {
                            var measurementResponse = file002.Measurements[i];

                            if (lpFlare.IsApplicable && lpFlare.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002)
                            {
                                totalLinearBurnetGas += measurementResponse.MED_CORRIGIDO_MVMDO_002;
                                gasResume.BurnedGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }
                    }

                    foreach (var pilotGas in gasCalculationByUepCode.PilotGases)
                    {
                        for (int i = 0; i < file002.Measurements.Count; ++i)
                        {
                            var measurementResponse = file002.Measurements[i];

                            if (pilotGas.IsApplicable && pilotGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002)
                            {
                                totalLinearBurnetGas += measurementResponse.MED_CORRIGIDO_MVMDO_002;
                                totalLinearFuelGas -= measurementResponse.MED_CORRIGIDO_MVMDO_002;

                                gasResume.FuelGas.MeasuringPoints.Add(measurementResponse.Summary);
                                gasResume.BurnedGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }
                    }

                    foreach (var purgeGas in gasCalculationByUepCode.PurgeGases)
                    {
                        for (int i = 0; i < file002.Measurements.Count; ++i)
                        {
                            var measurementResponse = file002.Measurements[i];

                            if (purgeGas.IsApplicable && purgeGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002)
                            {
                                totalLinearBurnetGas += measurementResponse.MED_CORRIGIDO_MVMDO_002;
                                totalLinearFuelGas -= measurementResponse.MED_CORRIGIDO_MVMDO_002;

                                gasResume.FuelGas.MeasuringPoints.Add(measurementResponse.Summary);
                                gasResume.BurnedGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }

                    }
                }

                foreach (var assistance in gasCalculationByUepCode.AssistanceGases)
                {
                    var containsAssistance = false;

                    foreach (var file in response._002File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_002 == assistance.MeasuringPoint.TagPointMeasuring && assistance.IsApplicable))
                        {
                            containsAssistance = true;
                        }
                    }

                    if (containsAssistance is false && response._002File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == assistance.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._002File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_002.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = assistance.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = assistance.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var hpFlare in gasCalculationByUepCode.HPFlares)
                {
                    var containsHpFlare = false;

                    foreach (var file in response._002File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_002 == hpFlare.MeasuringPoint.TagPointMeasuring && hpFlare.IsApplicable))
                        {
                            containsHpFlare = true;
                        }
                    }

                    if (containsHpFlare is false && response._002File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == hpFlare.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._002File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_002.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = hpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = hpFlare.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var lpFlare in gasCalculationByUepCode.LPFlares)
                {
                    var containsLpFlare = false;

                    foreach (var file in response._002File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_002 == lpFlare.MeasuringPoint.TagPointMeasuring && lpFlare.IsApplicable))
                        {
                            containsLpFlare = true;
                        }
                    }

                    if (containsLpFlare is false && response._002File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == lpFlare.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._002File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_002.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = lpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = lpFlare.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var exportGas in gasCalculationByUepCode.ExportGases)
                {
                    var containsExportGas = false;

                    foreach (var file in response._002File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_002 == exportGas.MeasuringPoint.TagPointMeasuring && exportGas.IsApplicable))
                        {
                            containsExportGas = true;
                        }
                    }

                    if (containsExportGas is false && response._002File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == exportGas.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._002File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_002.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = exportGas.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = exportGas.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var importGas in gasCalculationByUepCode.ImportGases)
                {
                    var containsImportGas = false;

                    foreach (var file in response._002File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_002 == importGas.MeasuringPoint.TagPointMeasuring && importGas.IsApplicable))
                        {
                            containsImportGas = true;
                        }
                    }

                    if (containsImportGas is false && response._002File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == importGas.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._002File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_002.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = importGas.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = importGas.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var purgeGas in gasCalculationByUepCode.PurgeGases)
                {
                    var containsPurge = false;

                    foreach (var file in response._002File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_002 == purgeGas.MeasuringPoint.TagPointMeasuring && purgeGas.IsApplicable))
                        {
                            containsPurge = true;
                        }
                    }

                    if (containsPurge is false && response._002File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == purgeGas.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._002File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_002.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = purgeGas.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = purgeGas.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var pilotGas in gasCalculationByUepCode.PilotGases)
                {
                    var containsPilot = false;

                    foreach (var file in response._002File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_002 == pilotGas.MeasuringPoint.TagPointMeasuring && pilotGas.IsApplicable))
                        {
                            containsPilot = true;
                        }
                    }

                    if (containsPilot is false && response._002File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == pilotGas.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._002File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_002.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = pilotGas.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = pilotGas.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var highPressure in gasCalculationByUepCode.HighPressureGases)
                {
                    var containsHigh = false;

                    foreach (var file in response._002File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_002 == highPressure.MeasuringPoint.TagPointMeasuring && highPressure.IsApplicable))
                        {
                            containsHigh = true;
                        }
                    }

                    if (containsHigh is false && response._002File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == highPressure.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._002File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_002.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = highPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = highPressure.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var lowPressure in gasCalculationByUepCode.LowPressureGases)
                {
                    var containsLow = false;

                    foreach (var file in response._002File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_002 == lowPressure.MeasuringPoint.TagPointMeasuring && lowPressure.IsApplicable))
                        {
                            containsLow = true;
                        }
                    }

                    if (containsLow is false && response._002File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == lowPressure.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._002File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_002.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = lowPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = lowPressure.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

            }

            if (response._003File.Any())
            {
                var gasCalculationByUepCode = await _gasCalculationRepository
                                  .GetGasVolumeCalculationByInstallationUEP(response._003File[0].Measurements[0].COD_INSTALACAO_003);
                if (gasCalculationByUepCode is null)
                    throw new NotFoundException("Cálculo de gás não encontrado");

                foreach (var file003 in response._003File)
                {

                    foreach (var assistanceGas in gasCalculationByUepCode.AssistanceGases)
                    {
                        for (int i = 0; i < file003.Measurements.Count; ++i)
                        {
                            var measurementResponse = file003.Measurements[i];

                            if (assistanceGas.IsApplicable && assistanceGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003)
                            {
                                totalDiferencialBurnetGas += measurementResponse.MED_CORRIGIDO_MVMDO_003;
                                gasResume.BurnedGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }
                    }

                    foreach (var exportGas in gasCalculationByUepCode.ExportGases)
                    {
                        for (int i = 0; i < file003.Measurements.Count; ++i)
                        {
                            var measurementResponse = file003.Measurements[i];

                            if (exportGas.IsApplicable && exportGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003)
                            {
                                totalDiferencialExportedGas += measurementResponse.MED_CORRIGIDO_MVMDO_003;
                                gasResume.ExportedGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }
                    }

                    foreach (var highPressureGas in gasCalculationByUepCode.HighPressureGases)
                    {
                        for (int i = 0; i < file003.Measurements.Count; ++i)
                        {
                            var measurementResponse = file003.Measurements[i];

                            if (highPressureGas.IsApplicable && highPressureGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003)
                            {
                                totalDiferencialFuelGas += measurementResponse.MED_CORRIGIDO_MVMDO_003;
                                gasResume.FuelGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }
                    }

                    foreach (var hpFlare in gasCalculationByUepCode.HPFlares)
                    {
                        for (int i = 0; i < file003.Measurements.Count; ++i)
                        {
                            var measurementResponse = file003.Measurements[i];

                            if (hpFlare.IsApplicable && hpFlare.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003)
                            {
                                totalDiferencialBurnetGas += measurementResponse.MED_CORRIGIDO_MVMDO_003;
                                gasResume.BurnedGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }
                    }

                    foreach (var importGas in gasCalculationByUepCode.ImportGases)
                    {
                        for (int i = 0; i < file003.Measurements.Count; ++i)
                        {
                            var measurementResponse = file003.Measurements[i];

                            if (importGas.IsApplicable && importGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003)
                            {
                                totalDiferencialImportedGas += measurementResponse.MED_CORRIGIDO_MVMDO_003;
                                gasResume.ImportedGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }
                    }

                    foreach (var lowPressure in gasCalculationByUepCode.LowPressureGases)
                    {
                        for (int i = 0; i < file003.Measurements.Count; ++i)
                        {
                            var measurementResponse = file003.Measurements[i];

                            if (lowPressure.IsApplicable && lowPressure.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003)
                            {
                                totalDiferencialFuelGas += measurementResponse.MED_CORRIGIDO_MVMDO_003;
                                gasResume.FuelGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }
                    }

                    foreach (var lpFlare in gasCalculationByUepCode.LPFlares)
                    {
                        for (int i = 0; i < file003.Measurements.Count; ++i)
                        {
                            var measurementResponse = file003.Measurements[i];

                            if (lpFlare.IsApplicable && lpFlare.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003)
                            {
                                totalDiferencialBurnetGas += measurementResponse.MED_CORRIGIDO_MVMDO_003;
                                gasResume.BurnedGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }
                    }

                    foreach (var pilotGas in gasCalculationByUepCode.PilotGases)
                    {
                        for (int i = 0; i < file003.Measurements.Count; ++i)
                        {
                            var measurementResponse = file003.Measurements[i];

                            if (pilotGas.IsApplicable && pilotGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003)
                            {
                                totalDiferencialBurnetGas += measurementResponse.MED_CORRIGIDO_MVMDO_003;
                                totalDiferencialFuelGas -= measurementResponse.MED_CORRIGIDO_MVMDO_003;

                                gasResume.BurnedGas.MeasuringPoints.Add(measurementResponse.Summary);
                                gasResume.FuelGas.MeasuringPoints.Add(measurementResponse.Summary);

                                break;
                            }
                        }

                    }

                    foreach (var purgeGas in gasCalculationByUepCode.PurgeGases)
                    {
                        for (int i = 0; i < file003.Measurements.Count; ++i)
                        {
                            var measurementResponse = file003.Measurements[i];

                            if (purgeGas.IsApplicable && purgeGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003)
                            {
                                totalDiferencialBurnetGas += measurementResponse.MED_CORRIGIDO_MVMDO_003;
                                totalDiferencialFuelGas -= measurementResponse.MED_CORRIGIDO_MVMDO_003;
                                gasResume.BurnedGas.MeasuringPoints.Add(measurementResponse.Summary);
                                gasResume.FuelGas.MeasuringPoints.Add(measurementResponse.Summary);
                                break;
                            }
                        }
                    }
                }

                foreach (var assistance in gasCalculationByUepCode.AssistanceGases)
                {
                    var containsAssistance = false;

                    foreach (var file in response._003File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_003 == assistance.MeasuringPoint.TagPointMeasuring && assistance.IsApplicable))
                        {
                            containsAssistance = true;
                        }
                    }

                    if (containsAssistance is false && response._003File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == assistance.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._003File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_003.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = assistance.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = assistance.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var hpFlare in gasCalculationByUepCode.HPFlares)
                {
                    var containsHpFlare = false;

                    foreach (var file in response._003File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_003 == hpFlare.MeasuringPoint.TagPointMeasuring && hpFlare.IsApplicable))
                        {
                            containsHpFlare = true;
                        }
                    }

                    if (containsHpFlare is false && response._003File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == hpFlare.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._003File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_003.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = hpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = hpFlare.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var lpFlare in gasCalculationByUepCode.LPFlares)
                {
                    var containsLpFlare = false;

                    foreach (var file in response._003File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_003 == lpFlare.MeasuringPoint.TagPointMeasuring && lpFlare.IsApplicable))
                        {
                            containsLpFlare = true;
                        }
                    }

                    if (containsLpFlare is false && response._003File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == lpFlare.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._003File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_003.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = lpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = lpFlare.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var exportGas in gasCalculationByUepCode.ExportGases)
                {
                    var containsExportGas = false;

                    foreach (var file in response._003File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_003 == exportGas.MeasuringPoint.TagPointMeasuring && exportGas.IsApplicable))
                        {
                            containsExportGas = true;
                        }
                    }

                    if (containsExportGas is false && response._003File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == exportGas.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._003File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_003.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = exportGas.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = exportGas.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var importGas in gasCalculationByUepCode.ImportGases)
                {
                    var containsImportGas = false;

                    foreach (var file in response._003File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_003 == importGas.MeasuringPoint.TagPointMeasuring && importGas.IsApplicable))
                        {
                            containsImportGas = true;
                        }
                    }

                    if (containsImportGas is false && response._003File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == importGas.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._003File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_003.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = importGas.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = importGas.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var purgeGas in gasCalculationByUepCode.PurgeGases)
                {
                    var containsPurge = false;

                    foreach (var file in response._003File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_003 == purgeGas.MeasuringPoint.TagPointMeasuring && purgeGas.IsApplicable))
                        {
                            containsPurge = true;
                        }
                    }

                    if (containsPurge is false && response._003File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == purgeGas.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._003File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_003.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = purgeGas.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = purgeGas.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var pilotGas in gasCalculationByUepCode.PilotGases)
                {
                    var containsPilot = false;

                    foreach (var file in response._003File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_003 == pilotGas.MeasuringPoint.TagPointMeasuring && pilotGas.IsApplicable))
                        {
                            containsPilot = true;
                        }
                    }

                    if (containsPilot is false && response._003File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == pilotGas.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._003File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_003.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = pilotGas.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = pilotGas.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var highPressure in gasCalculationByUepCode.HighPressureGases)
                {
                    var containsHigh = false;

                    foreach (var file in response._003File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_003 == highPressure.MeasuringPoint.TagPointMeasuring && highPressure.IsApplicable))
                        {
                            containsHigh = true;
                        }
                    }

                    if (containsHigh is false && response._003File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == highPressure.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._003File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_003.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = highPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = highPressure.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var lowPressure in gasCalculationByUepCode.LowPressureGases)
                {
                    var containsLow = false;

                    foreach (var file in response._003File)
                    {
                        if (file.Measurements.Any(x => x.COD_TAG_PONTO_MEDICAO_003 == lowPressure.MeasuringPoint.TagPointMeasuring && lowPressure.IsApplicable))
                        {
                            containsLow = true;
                        }
                    }

                    if (containsLow is false && response._003File.Any() && !response.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == lowPressure.MeasuringPoint.TagPointMeasuring))
                    {
                        response.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = response._003File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_003.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = lowPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = lowPressure.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }
            }

            var totalDiferencialGas = totalDiferencialBurnetGas + totalDiferencialFuelGas + (totalDiferencialExportedGas - totalDiferencialImportedGas);

            var gasDiferencial = new GasDiferencialDto
            {
                TotalGasBurnt = totalDiferencialBurnetGas,
                TotalGas = totalDiferencialGas,
                TotalGasExported = totalDiferencialExportedGas,
                TotalGasFuel = totalDiferencialFuelGas,
                TotalGasImported = totalDiferencialImportedGas

            };

            var totalLinearGas = totalLinearBurnetGas + totalLinearFuelGas + (totalLinearExportedGas - totalLinearImportedGas);
            var gasLinear = new GasLinearDto
            {
                TotalGasBurnt = totalLinearBurnetGas,
                TotalGas = totalLinearGas,
                TotalGasExported = totalLinearExportedGas,
                TotalGasFuel = totalLinearFuelGas,
                TotalGasImported = totalLinearImportedGas

            };

            var gasTotalResponse = new GasDto
            {
                TotalGasProductionM3 = Math.Round(gasLinear.TotalGas + gasDiferencial.TotalGas, 5),
                TotalGasProductionBBL = Math.Round((gasLinear.TotalGas + gasDiferencial.TotalGas) * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                TotalGasBurnt = Math.Round(gasLinear.TotalGasBurnt + gasDiferencial.TotalGasBurnt, 5),
                TotalGasFuel = Math.Round(gasLinear.TotalGasFuel + gasDiferencial.TotalGasFuel, 5),
                TotalGasExported = Math.Round(gasLinear.TotalGasExported + gasDiferencial.TotalGasExported, 5),
                TotalGasImported = Math.Round(gasLinear.TotalGasImported + gasDiferencial.TotalGasImported, 5),
            };

            var fields = await _fieldRepository
                .GetFieldsByUepCode(response.UepCode);

            var fieldsConverted = new List<FRFieldsViewModel>();

            foreach (var field in fields)
            {
                var fieldViewModel = new FRFieldsViewModel
                {
                    FieldId = field.Id,
                    FluidFr = 0,
                    FieldName = field.Name
                };

                fieldsConverted.Add(fieldViewModel);
            }
            var dateParsed = DateTime.Parse(response.DateProduction);
            var productionOfTheDay = await _productionRepository
                .GetExistingByDate(dateParsed);

            var isApplicable = false;

            if (productionOfTheDay is not null && productionOfTheDay.FieldsFR is not null && productionOfTheDay.FieldsFR.Any())
            {
                isApplicable = true;
            }

            var frProduction = new FRViewModel
            {
                Fields = fieldsConverted,
                IsApplicable = isApplicable,
            };

            if (response._001File.Count > 0)
            {

                var oilResponse = new OilDto
                {
                    TotalOilProductionM3 = totalOil,
                    TotalOilProductionBBL = Math.Round(totalOil * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                    FR = frProduction,
                };

                response.Oil = oilResponse;
                var totalWater = 0m;
                totalWater += (bswTotal / divideBsw) * totalOilWithoutBsw;

                var waterResponse = new WaterDto
                {
                    TotalWaterM3 = totalWater,
                    TotalWaterBBL = totalWater * ProductionUtils.m3ToSCFConversionMultipler
                };

                response.Water = waterResponse;
            }

            if (response._002File.Count > 0)
            {
                response.GasLinear = gasLinear;
            }

            if (response._003File.Count > 0)
                response.GasDiferencial = gasDiferencial;

            if (response._002File.Count > 0 || response._003File.Count > 0)
            {
                response.Gas = gasTotalResponse;
                gasResume.BurnedGas.TotalBurnedGas = gasLinear.TotalGasBurnt + gasDiferencial.TotalGasBurnt;
                gasResume.FuelGas.TotalFuelGas = gasLinear.TotalGasFuel + gasDiferencial.TotalGasFuel;
                gasResume.ExportedGas.TotalExportedGas = gasLinear.TotalGasExported + gasDiferencial.TotalGasExported;
                gasResume.ImportedGas.TotalImportedGas = gasLinear.TotalGasImported + gasDiferencial.TotalGasImported;

                response.GasSummary = gasResume;
                response.GasSummary.DetailedBurnedGas.OthersBurn = gasResume.BurnedGas.TotalBurnedGas;
                response.Gas.FR = frProduction;
            }

            if (productionOfTheDay is null)
                response.StatusProduction = "aberto";
            else
                response.StatusProduction = productionOfTheDay.StatusProduction;

            return response;
        }
        public async Task<ImportResponseDTO> Import(ResponseXmlDto data, User user)
        {
            var date001 = data._001File.Count > 0 ? data._001File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_001 : DateTime.MinValue;
            var date002 = data._002File.Count > 0 ? data._002File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_002 : DateTime.MinValue;
            var date003 = data._003File.Count > 0 ? data._003File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_003 : DateTime.MinValue;

            if ((date001 != date002 && date001 != DateTime.MinValue && date002 != DateTime.MinValue) || (date001 != date003 && date001 != DateTime.MinValue && date003 != DateTime.MinValue) || (date002 != date003 && date002 != DateTime.MinValue && date003 != DateTime.MinValue))
                throw new BadRequestException("Datas incompatíveis entre medições, data de inicio da medição deve ser igual.");

            foreach (var file in data._001File)
            {
                foreach (var measurement in file.Measurements)
                {

                    if (measurement.BswManual < 0 || measurement.BswManual > 1)
                        throw new BadRequestException("BSW deve ser um valor entre 0% e 100%");

                    if (measurement.DHA_INICIO_PERIODO_MEDICAO_001 != date001)
                    {
                        throw new BadRequestException("Datas incompatíveis entre medições, data de inicio da medição deve ser igual.");
                    }
                }

                if (file.File is not null)
                {

                    var fileInDatabase = await _measurementHistoryRepository
                        .GetById(file.File.ImportId);

                    if (fileInDatabase is not null)
                        throw new BadRequestException("Recarregue o arquivo");
                }
            }

            foreach (var file in data._002File)
            {
                foreach (var measurement in file.Measurements)
                {
                    if (measurement.DHA_INICIO_PERIODO_MEDICAO_002 != date002)
                    {
                        throw new BadRequestException("Datas incompatíveis entre medições, data de inicio da medição deve ser igual.");
                    }
                }

                if (file.File is not null)
                {

                    var fileInDatabase = await _measurementHistoryRepository
                        .GetById(file.File.ImportId);

                    if (fileInDatabase is not null)
                        throw new BadRequestException("Recarregue o arquivo");
                }
            }

            foreach (var file in data._003File)
            {
                foreach (var measurement in file.Measurements)
                {
                    if (measurement.DHA_INICIO_PERIODO_MEDICAO_003 != date003)
                    {
                        throw new BadRequestException("Datas incompatíveis entre medições, data de inicio da medição deve ser igual.");
                    }
                }
                if (file.File is not null)
                {

                    var fileInDatabase = await _measurementHistoryRepository
                        .GetById(file.File.ImportId);

                    if (fileInDatabase is not null)
                        throw new BadRequestException("Recarregue o arquivo");
                }

            }

            if (data.GasSummary is not null)
            {
                var sumOfDetailedBurnedGas = data.GasSummary.DetailedBurnedGas.WellTestBurn + data.GasSummary.DetailedBurnedGas.LimitOperacionalBurn + data.GasSummary.DetailedBurnedGas.ForCommissioningBurn + data.GasSummary.DetailedBurnedGas.ScheduledStopBurn + data.GasSummary.DetailedBurnedGas.EmergencialBurn + data.GasSummary.DetailedBurnedGas.VentedGas + data.GasSummary.DetailedBurnedGas.OthersBurn;

                if (data.Gas is not null && data.Gas.TotalGasBurnt != sumOfDetailedBurnedGas)
                {
                    throw new BadRequestException($"Somatório do detalhamento de queima deve ser igual ao total de gás de queima: {data.Gas.TotalGasBurnt}");
                }

            }

            var base64HistoryMap = new Dictionary<string, MeasurementHistory>();

            var measurementsAdded = new List<Measurement>();

            DateTime measuredAt = DateTime.UtcNow.AddHours(-3);

            if (data._001File.Count > 0)
                measuredAt = data._001File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_001;

            if (data._002File.Count > 0)
                measuredAt = data._002File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_002;

            if (data._003File.Count > 0)
                measuredAt = data._003File[0].Measurements[0].DHA_INICIO_PERIODO_MEDICAO_003;

            var installation = await _installationRepository.GetByIdAsync(data.InstallationId);

            if (installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());


            var dailyProduction = await _productionRepository.GetExistingByDate(measuredAt);

            dailyProduction ??= new Production
            {
                Id = Guid.NewGuid(),
                CalculatedImportedBy = user,
                CalculatedImportedAt = DateTime.UtcNow.AddHours(-3),
                MeasuredAt = measuredAt,
                Installation = installation,

            };

            if (data.GasSummary is not null && ((dailyProduction.Gas is null) || (dailyProduction.Gas is not null && dailyProduction.Gas.IsActive is false)))
            {
                dailyProduction.Gas = new Gas
                {
                    EmergencialBurn = data.GasSummary.DetailedBurnedGas.EmergencialBurn,
                    LimitOperacionalBurn = data.GasSummary.DetailedBurnedGas.LimitOperacionalBurn,
                    ScheduledStopBurn = data.GasSummary.DetailedBurnedGas.ScheduledStopBurn,
                    ForCommissioningBurn = data.GasSummary.DetailedBurnedGas.ForCommissioningBurn,
                    VentedGas = data.GasSummary.DetailedBurnedGas.VentedGas,
                    WellTestBurn = data.GasSummary.DetailedBurnedGas.WellTestBurn,
                    OthersBurn = data.GasSummary.DetailedBurnedGas.OthersBurn,
                };

            }

            var dividirBsw = 0;

            var totalOilWithBsw = 0m;
            var totalOilWithoutBsw = 0m;
            var totalProduction = 0m;
            var bswAverage = 0m;

            foreach (var file in data._001File)
            {
                foreach (var bodyMeasurement in file.Measurements)
                {
                    var individualOilAfterBsw = 0m;

                    var measurement = _mapper.Map<Client001DTO, Measurement>(bodyMeasurement);

                    var measurementExists = await _repository.GetMeasurementByDate(measurement.DHA_INICIO_PERIODO_MEDICAO_001, XmlUtils.File001);

                    if (measurementExists is not null && measurementExists.IsActive)
                        throw new ConflictException($"Medição na data: {measurement.DHA_INICIO_PERIODO_MEDICAO_001} já cadastrada.");

                    var measuringPoint = await _measuringPointRepository.GetByTagMeasuringPointXML(bodyMeasurement.COD_TAG_PONTO_MEDICAO_001, XmlUtils.File001);

                    if (measuringPoint is null)
                        throw new NotFoundException($"{ErrorMessages.NotFound<MeasuringPoint>()} TAG: {measurement.COD_TAG_PONTO_MEDICAO_001}");

                    bswAverage += bodyMeasurement.BswManual;
                    dividirBsw++;

                    var gasCalculation = await _oilCalculationRepository.GetOilVolumeCalculationByInstallationId(installation.Id);

                    foreach (var tog in gasCalculation.TOGRecoveredOils)
                    {
                        if (tog.IsApplicable && tog.MeasuringPoint.TagPointMeasuring == bodyMeasurement.COD_TAG_PONTO_MEDICAO_001)
                        {
                            totalOilWithBsw += bodyMeasurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001;
                            totalOilWithoutBsw += bodyMeasurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001;
                            individualOilAfterBsw += bodyMeasurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001;
                        }
                    }

                    foreach (var drain in gasCalculation.DrainVolumes)
                    {
                        if (drain.IsApplicable && drain.MeasuringPoint.TagPointMeasuring == bodyMeasurement.COD_TAG_PONTO_MEDICAO_001)
                        {
                            totalOilWithBsw += bodyMeasurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001;
                            totalOilWithoutBsw += bodyMeasurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001;
                            individualOilAfterBsw += bodyMeasurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001;

                        }
                    }

                    foreach (var section in gasCalculation.Sections)
                    {
                        if (section.IsApplicable && section.MeasuringPoint.TagPointMeasuring == bodyMeasurement.COD_TAG_PONTO_MEDICAO_001)
                        {
                            totalOilWithBsw += bodyMeasurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001 * (1 - bodyMeasurement.BswManual);
                            individualOilAfterBsw += bodyMeasurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001 * (1 - bodyMeasurement.BswManual);
                            totalOilWithoutBsw += bodyMeasurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001;

                        }
                    }

                    foreach (var dor in gasCalculation.DORs)
                    {
                        if (dor.IsApplicable && dor.MeasuringPoint.TagPointMeasuring == bodyMeasurement.COD_TAG_PONTO_MEDICAO_001)
                        {
                            totalOilWithBsw -= bodyMeasurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001 * (1 - bodyMeasurement.BswManual);
                            individualOilAfterBsw -= bodyMeasurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001 * (1 - bodyMeasurement.BswManual);
                            totalOilWithoutBsw -= bodyMeasurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001;

                        }
                    }

                    if (bodyMeasurement.ImportId != Guid.Empty)
                    {
                        var fileInfo = new FileBasicInfoDTO
                        {
                            Acronym = XmlUtils.FileAcronym001,
                            Name = bodyMeasurement.FileName,
                            Type = XmlUtils.File001
                        };
                        measurement.User = user;
                        measurement.Id = Guid.NewGuid();

                        measurement.FileType = new FileType
                        {
                            Id = Guid.NewGuid(),
                            Name = fileInfo.Type,
                            Acronym = fileInfo.Acronym,
                            ImportId = bodyMeasurement.ImportId
                        };

                        measurement.Installation = installation;
                        measurement.MeasuringPoint = measuringPoint;
                        measurement.BswManual_001 = bodyMeasurement.BswManual;
                        measurement.StatusMeasuringPoint = bodyMeasurement.Summary.Status;
                        measurement.VolumeAfterManualBsw_001 = individualOilAfterBsw;

                        var path001Xml = Path.GetTempPath() + bodyMeasurement.ImportId + ".xml";

                        if (File.Exists(path001Xml))
                        {
                            var documentXml = XDocument.Load(path001Xml);
                            byte[] xmlBytes;
                            using (var memoryStream = new MemoryStream())
                            using (var xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings { Encoding = Encoding.UTF8 }))
                            {
                                documentXml.Save(xmlWriter);
                                xmlWriter.Flush();
                                xmlBytes = memoryStream.ToArray();
                            }

                            var base64String = "data:@file/xml;base64," + Convert.ToBase64String(xmlBytes);

                            if (base64HistoryMap.TryGetValue(base64String, out var history) is false)
                            {
                                history = await _measurementService.Import(user, fileInfo, base64String, measurement.DHA_INICIO_PERIODO_MEDICAO_001, bodyMeasurement.ImportId);
                                base64HistoryMap.Add(base64String, history);
                            }

                            measurement.MeasurementHistory = history;
                        }

                        measurementsAdded.Add(measurement);
                    }
                }

            }

            if (data.Oil is not null && ((dailyProduction.Oil is not null && dailyProduction.Oil.IsActive is false) || dailyProduction.Oil is null))
            {
                data.Oil.TotalOilProductionM3 = totalOilWithBsw;

                if (dividirBsw > 0)
                {
                    bswAverage = bswAverage / dividirBsw;
                }
                else
                {
                    bswAverage = 0;
                }

                var oil = new Oil
                {
                    StatusOil = true,
                    TotalOil = totalOilWithBsw,
                    Production = dailyProduction,
                    BswAverage = bswAverage,
                    TotalOilWithoutBsw = totalOilWithoutBsw
                };

                dailyProduction.Oil = oil;
            }

            totalProduction += totalOilWithBsw;

            foreach (var file in data._002File)
            {
                foreach (var bodyMeasurement in file.Measurements)
                {
                    var measurement = _mapper.Map<Client002DTO, Measurement>(bodyMeasurement);

                    var measurementExists = await _repository.GetMeasurementByDate(measurement.DHA_INICIO_PERIODO_MEDICAO_002, XmlUtils.File002);

                    if (measurementExists is not null && measurementExists.IsActive)
                        throw new ConflictException($"Medição na data: {measurement.DHA_INICIO_PERIODO_MEDICAO_002} já cadastrada.");

                    var measuringPoint = await _measuringPointRepository.GetByTagMeasuringPointXML(bodyMeasurement.COD_TAG_PONTO_MEDICAO_002, XmlUtils.File002);

                    if (measuringPoint is null)
                        throw new NotFoundException($"{ErrorMessages.NotFound<MeasuringPoint>()} TAG: {measurement.COD_TAG_PONTO_MEDICAO_002}");

                    if (bodyMeasurement.ImportId != Guid.Empty)
                    {
                        var fileInfo = new FileBasicInfoDTO
                        {
                            Acronym = XmlUtils.FileAcronym002,
                            Name = bodyMeasurement.FileName,
                            Type = XmlUtils.File002
                        };
                        measurement.User = user;

                        measurement.Id = Guid.NewGuid();

                        measurement.FileType = new FileType
                        {
                            Id = Guid.NewGuid(),
                            Name = fileInfo.Type,
                            Acronym = fileInfo.Acronym,
                            ImportId = bodyMeasurement.ImportId
                        };

                        measurement.Installation = installation;
                        measurement.MeasuringPoint = measuringPoint;

                        var path002Xml = Path.GetTempPath() + bodyMeasurement.ImportId + ".xml";

                        if (File.Exists(path002Xml))
                        {
                            var documentXml = XDocument.Load(path002Xml);
                            byte[] xmlBytes;
                            using (var memoryStream = new MemoryStream())
                            using (var xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings { Encoding = Encoding.UTF8 }))
                            {
                                documentXml.Save(xmlWriter);
                                xmlWriter.Flush();
                                xmlBytes = memoryStream.ToArray();
                            }

                            var base64String = "data:@file/xml;base64," + Convert.ToBase64String(xmlBytes);

                            if (base64HistoryMap.TryGetValue(base64String, out var history) is false)
                            {
                                history = await _measurementService.Import(user, fileInfo, base64String, measurement.DHA_INICIO_PERIODO_MEDICAO_002, bodyMeasurement.ImportId);
                                base64HistoryMap.Add(base64String, history);
                            }

                            measurement.MeasurementHistory = history;
                        }

                        measurementsAdded.Add(measurement);
                    }
                }

            }

            foreach (var file in data._003File)
            {
                foreach (var bodyMeasurement in file.Measurements)
                {
                    var measurement = _mapper.Map<Client003DTO, Measurement>(bodyMeasurement);

                    var measurementExists = await _repository.GetMeasurementByDate(measurement.DHA_INICIO_PERIODO_MEDICAO_003, XmlUtils.File003);

                    if (measurementExists is not null && measurementExists.IsActive)
                        throw new ConflictException($"Medição na data: {measurement.DHA_INICIO_PERIODO_MEDICAO_003} já cadastrada.");

                    var measuringPoint = await _measuringPointRepository.GetByTagMeasuringPointXML(bodyMeasurement.COD_TAG_PONTO_MEDICAO_003, XmlUtils.File003);

                    if (measuringPoint is null)
                        throw new NotFoundException($"{ErrorMessages.NotFound<MeasuringPoint>()} TAG: {measurement.COD_TAG_PONTO_MEDICAO_003}");

                    if (bodyMeasurement.ImportId != Guid.Empty)
                    {
                        var fileInfo = new FileBasicInfoDTO
                        {
                            Acronym = XmlUtils.FileAcronym003,
                            Name = bodyMeasurement.FileName,
                            Type = XmlUtils.File003
                        };
                        measurement.User = user;
                        measurement.Id = Guid.NewGuid();

                        measurement.FileType = new FileType
                        {
                            Id = Guid.NewGuid(),
                            Name = fileInfo.Type,
                            Acronym = fileInfo.Acronym,
                            ImportId = bodyMeasurement.ImportId
                        };

                        measurement.Installation = installation;
                        measurement.MeasuringPoint = measuringPoint;

                        var path003Xml = Path.GetTempPath() + bodyMeasurement.ImportId + ".xml";

                        if (File.Exists(path003Xml))
                        {
                            var documentXml = XDocument.Load(path003Xml);
                            byte[] xmlBytes;
                            using (var memoryStream = new MemoryStream())
                            using (var xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings { Encoding = Encoding.UTF8 }))
                            {
                                documentXml.Save(xmlWriter);
                                xmlWriter.Flush();
                                xmlBytes = memoryStream.ToArray();
                            }

                            var base64String = "data:@file/xml;base64," + Convert.ToBase64String(xmlBytes);

                            if (base64HistoryMap.TryGetValue(base64String, out var history) is false)
                            {
                                history = await _measurementService.Import(user, fileInfo, base64String, measurement.DHA_INICIO_PERIODO_MEDICAO_003, bodyMeasurement.ImportId);
                                base64HistoryMap.Add(base64String, history);
                            }

                            measurement.MeasurementHistory = history;
                        }

                        measurementsAdded.Add(measurement);

                    }
                }

            }

            if (data.GasSummary is not null)
            {
                if (dailyProduction.GasDiferencial is null && data.GasDiferencial is not null)
                {
                    var gasDiferencial = new GasDiferencial
                    {
                        StatusGas = true,
                        TotalGas = data.GasDiferencial.TotalGas,
                        ExportedGas = data.GasDiferencial.TotalGasExported,
                        ImportedGas = data.GasDiferencial.TotalGasImported,
                        BurntGas = data.GasDiferencial.TotalGasBurnt,
                        FuelGas = data.GasDiferencial.TotalGasFuel,

                    };

                    dailyProduction.GasDiferencial = gasDiferencial;
                    totalProduction += gasDiferencial.TotalGas;

                    dailyProduction.Gas.GasDiferencial = gasDiferencial;
                }

                if (dailyProduction.GasLinear is null && data.GasLinear is not null)
                {
                    var gasLinear = new GasLinear
                    {
                        StatusGas = true,
                        TotalGas = data.GasLinear.TotalGas,
                        ExportedGas = data.GasLinear.TotalGasExported,
                        ImportedGas = data.GasLinear.TotalGasImported,
                        BurntGas = data.GasLinear.TotalGasBurnt,
                        FuelGas = data.GasLinear.TotalGasFuel,
                    };

                    dailyProduction.GasLinear = gasLinear;

                    totalProduction += gasLinear.TotalGas;

                    dailyProduction.Gas.GasLinear = gasLinear;

                }
            }

            dailyProduction.TotalProduction += totalProduction;

            foreach (var measuring in measurementsAdded)
            {
                measuring.Production = dailyProduction;
            }

            var fieldFrViewModel = new FieldFRBodyService
            {
                Oil = data.Oil,
                Gas = data.Gas,
                InstallationId = installation.Id,
                Production = dailyProduction,

            };
            var dateParsed = DateTime.Parse(data.DateProduction);
            await _fieldFRService.ApplyFR(fieldFrViewModel, dateParsed);

            await _productionRepository.AddOrUpdateProduction(dailyProduction);
            await _repository.AddRangeAsync(measurementsAdded);

            await _repository.SaveChangesAsync();

            if (measurementsAdded.Count
                == 0)
                throw new BadRequestException("Nenhuma medição foi adicionada", status: "Error");

            return new ImportResponseDTO { Status = "Success", Message = $"Arquivo importado com sucesso, {measurementsAdded.Count} medições importadas", ProductionId = dailyProduction.Id };
        }

        private bool IsValidAndUniqueDate(string datePortion, List<FileContent> files)
        {
            DateTime parsedDate;
            if (DateTime.TryParseExact(datePortion, "yyyyMMddHHmmss", null, DateTimeStyles.None, out parsedDate))
            {
                var dateToCompare = parsedDate.Date;

                return files
                    .Select(file =>
                    {
                        DateTime otherDate;
                        if (DateTime.TryParseExact(file.FileName.Split('_')[2], "yyyyMMddHHmmss", null, DateTimeStyles.None, out otherDate))
                        {
                            return otherDate.Date;
                        }
                        else
                        {
                            throw new BadRequestException($"Não é possível analisar a data no arquivo: {file.FileName}, formato ANP aceitável: 'yyyyMMddHHmmss'");
                        }
                    })
                    .All(date => date == dateToCompare);
            }

            throw new BadRequestException($"Não é possível analisar a data: {datePortion}, formato ANP aceitável: 'yyyyMMddHHmmss'");
        }
    }
}