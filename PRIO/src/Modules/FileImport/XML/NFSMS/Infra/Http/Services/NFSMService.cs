using AutoMapper;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Infra.Http.Services;
using PRIO.src.Modules.FileImport.XML.Measuring.Dtos;
using PRIO.src.Modules.FileImport.XML.Measuring.FileContent;
using PRIO.src.Modules.FileImport.XML.Measuring.FileContent._039;
using PRIO.src.Modules.FileImport.XML.Measuring.Infra.Utils;
using PRIO.src.Modules.FileImport.XML.NFSMS.Dtos;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.NFSMS.Interfaces;
using PRIO.src.Modules.FileImport.XML.NFSMS.ViewModels;
using PRIO.src.Modules.Hierarchy.Installations.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Measurements.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Dtos;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Dtos;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Utils;
using PRIO.src.Modules.Measuring.WellProductions.Infra.Http.Services;
using PRIO.src.Modules.Measuring.WellProductions.Infra.Utils;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Utils;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace PRIO.src.Modules.FileImport.XML.NFSMS.Infra.Http.Services
{
    public class NFSMService
    {
        private readonly IMapper _mapper;
        private readonly IMeasurementRepository _measurementRepository;
        private readonly INFSMRepository _repository;
        private readonly IMeasuringPointRepository _measuringPointRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IProductionRepository _productionRepository;
        private readonly IMeasurementHistoryRepository _measurementHistoryRepository;
        private readonly IOilVolumeCalculationRepository _oilCalculationRepository;
        private readonly IGasVolumeCalculationRepository _gasCalculationRepository;
        private readonly WellProductionService _wellProductionService;
        private readonly UserService _userService;
        private readonly MeasurementService _measurementService;

        public ResponseNFSMDTO _responseResult = new();

        public NFSMService(IMapper mapper, IMeasurementHistoryRepository measurementHistoryRepository, IMeasurementRepository measurementRepository, IInstallationRepository installationRepository, IMeasuringPointRepository measuringPointRepository, IProductionRepository productionRepository, IOilVolumeCalculationRepository oilVolumeCalculation, MeasurementService measurementService, INFSMRepository repository, UserService userService, WellProductionService wellProductionService, IOilVolumeCalculationRepository oilVolumeCalculationRepository, IGasVolumeCalculationRepository gasVolumeCalculationRepository)
        {
            _wellProductionService = wellProductionService;
            _mapper = mapper;
            _measurementRepository = measurementRepository;
            _measurementHistoryRepository = measurementHistoryRepository;
            _installationRepository = installationRepository;
            _measuringPointRepository = measuringPointRepository;
            _productionRepository = productionRepository;
            _measurementService = measurementService;
            _repository = repository;
            _userService = userService;
            _oilCalculationRepository = oilVolumeCalculationRepository;
            _gasCalculationRepository = gasVolumeCalculationRepository;
        }

        public async Task<ResponseNFSMDTO> Validate(NFSMImportViewModel data, User user)
        {
            #region client side validations

            var isValidExtension = data.File.FileName.ToLower().EndsWith(".xml");

            if (isValidExtension is false)
                throw new BadRequestException($"Formato arquivo inválido, deve ter a extensão xml. Importação falhou arquivo com nome: {data.File.FileName}");

            var fileContent = data.File.ContentBase64.Replace("data:@file/xml;base64,", "");

            if (Decrypt.TryParseBase64String(fileContent, out _) is false)
                throw new BadRequestException("Não é um base64 válido");

            var isValidFileType = XmlUtils.File039 == data.File.FileType;

            if (isValidFileType is false)
                throw new BadRequestException($"Deve pertencer a categoria 039. Importação falhou, arquivo com nome: {data.File.FileName}");

            #endregion

            var userDto = _mapper.Map<UserDTO>(user);
            var errorsInImport = new List<string>();
            var errorsInFormat = new List<string>();

            #region pathing
            //var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\.."));
            var relativeSchemaPath = Path.Combine("schemasXsd", "039.xsd");
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
                var result = Functions.CheckFormat(pathXml, pathSchema, errorsInFormat, data.File.FileName);
                if (errorsInFormat.Count > 0)
                    throw new BadRequestException($"Algum(s) erro(s) de formatação ocorreram durante a validação do arquivo de nome: {data.File.FileName}", errors: errorsInFormat);
                if (result is not null && result.Count > 0)
                    throw new BadRequestException(string.Join(",", errorsInFormat));
            }

            var documentXml = XDocument.Load(pathXml);
            #endregion

            #region generic elements and basic validation
            var rootElement = documentXml.Root;
            var dadosBasicosElements = rootElement?.Elements("LISTA_DADOS_BASICOS")?.Elements("DADOS_BASICOS");

            if (dadosBasicosElements is null)
                throw new BadRequestException("LISTA_DADOS_BASICOS XML element cant be null");
            #endregion

            var responseResult = new ResponseNFSMDTO
            {
                File = new MeasurementHistoryDto
                {

                    FileContent = data.File.ContentBase64,
                    FileName = data.File.FileName,
                    FileType = data.File.FileType,
                    ImportedAt = DateTime.UtcNow.AddHours(-3).ToString("dd/MM/yyyy"),
                    ImportedBy = userDto,
                    ImportId = importId
                },

            };

            for (int k = 0; k < dadosBasicosElements.Count(); ++k)
            {
                var dadosBasicosElement = dadosBasicosElements.ElementAt(k);

                #region elementos XML
                var dadosBasicos = Functions.DeserializeXml<DADOS_BASICOS_039>(dadosBasicosElement);
                #endregion

                if (dadosBasicos is not null && dadosBasicos.COD_FALHA_039 is not null && dadosBasicos.DHA_COD_INSTALACAO_039 is not null && dadosBasicos.COD_TAG_PONTO_MEDICAO_039 is not null)
                {
                    var nfsmInDatabase = await _repository
                        .GetOneByCode(dadosBasicos.COD_FALHA_039);

                    if (nfsmInDatabase is not null && nfsmInDatabase.IsActive)
                        errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª notificação(DADOS_BASICOS) com código de falha: {dadosBasicos.COD_FALHA_039} já existente.");

                    var installation = await _installationRepository
                              .GetInstallationMeasurementByUepAndAnpCodAsync(dadosBasicos.DHA_COD_INSTALACAO_039, XmlUtils.File039);

                    if (installation is null)
                        errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª notificação(DADOS_BASICOS): {ErrorMessages.NotFound<Installation>()}");

                    if (installation is not null && installation.IsActive is false)
                        errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª notificação(DADOS_BASICOS), instalação: {installation.Name} está inativa.");

                    var measuringPoint = await _measuringPointRepository
                        .GetByTagMeasuringPointXML(dadosBasicos.COD_TAG_PONTO_MEDICAO_039, XmlUtils.File039);

                    if (measuringPoint is null)
                        errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª notificação(DADOS_BASICOS), ponto de medição TAG: {dadosBasicos.COD_TAG_PONTO_MEDICAO_039}: {ErrorMessages.NotFound<MeasuringPoint>()}");

                    if (measuringPoint is not null && measuringPoint.IsActive is false)
                        errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª notificação(DADOS_BASICOS), ponto de medição TAG: {dadosBasicos.COD_TAG_PONTO_MEDICAO_039} está inativo.");

                    if (installation is not null && installation.MeasuringPoints is not null && installation.IsActive && measuringPoint is not null)
                    {
                        bool contains = false;

                        foreach (var point in installation.MeasuringPoints)
                            if (measuringPoint.TagPointMeasuring == point.TagPointMeasuring && measuringPoint.IsActive)
                                contains = true;

                        if (contains is false && measuringPoint.IsActive)
                            errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª notificação(DADOS_BASICOS), TAG do ponto de medição não encontrado nessa instalação");
                    }

                    //if (errorsInImport.Count == 0 && installation is not null && measuringPoint is not null)
                    //{

                    var listaBswElements = dadosBasicosElement?.Elements("LISTA_BSW")?.ToList();

                    var bswsFixed = new List<BswFixedNfsm>();

                    var bswList = new List<Bsw>();
                    var calibrationList = new List<Calibration>();
                    var volumeList = new List<Volume>();

                    if (dadosBasicos.LISTA_BSW is not null && listaBswElements is not null)
                        for (var j = 0; j < dadosBasicos.LISTA_BSW.Count; ++j)
                        {
                            var dateString = dadosBasicos.LISTA_BSW[j].DHA_FALHA_BSW_039;

                            if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                            {
                                var productionInDatabase = await _productionRepository.AnyByDate(date);

                                if (productionInDatabase is false)
                                    errorsInImport.Add($"Produção TAG(DHA_FALHA_BSW) não encontrada para esta data: {date}");
                            }
                            else
                            {
                                errorsInImport.Add("Formato de data da medição do bsw(DHA_FALHA_BSW) inválido, deve ser: dd/MM/yyyy");
                            }

                            var bsw = dadosBasicos.LISTA_BSW[j];
                            //var bswElement = listaBswElements[j]?.Element("BSW");

                            var bswMapped = _mapper.Map<BSW, Bsw>(bsw);

                            bswMapped.DHA_FALHA_BSW_039 = XmlUtils.DateTimeWithoutTimeParser(bsw.DHA_FALHA_BSW_039, errorsInFormat, "");
                            bswMapped.DHA_PCT_MAXIMO_BSW_039 = XmlUtils.DecimalParser(bsw.DHA_PCT_MAXIMO_BSW_039, errorsInFormat, "");
                            bswMapped.DHA_PCT_BSW_039 = XmlUtils.DecimalParser(bsw.DHA_PCT_BSW_039, errorsInFormat, "");

                            var bswFixed = new BswFixedNfsm
                            {
                                Bsw = bswMapped.DHA_PCT_BSW_039,
                                Date = bswMapped.DHA_FALHA_BSW_039,
                                MaxBsw = bswMapped.DHA_PCT_MAXIMO_BSW_039
                            };

                            bswsFixed.Add(bswFixed);
                            bswList.Add(bswMapped);
                            //measurement.LISTA_BSW.Add(bswMapped);
                        }

                    var measurementsFixed = new List<VolumeFixedNfsm>();

                    var volumesListElements = dadosBasicosElement?.Elements("LISTA_VOLUME")?.ToList();

                    if (dadosBasicos.LISTA_VOLUME is not null && /*measurement.LISTA_VOLUME is not null &&*/ volumesListElements is not null)
                    {
                        for (var j = 0; j < dadosBasicos.LISTA_VOLUME.Count; ++j)
                        {

                            var dateString = dadosBasicos.LISTA_VOLUME[j].DHA_MEDICAO_039;

                            //var dateElement = volumesListElements.ElementAt(j).Element("DHA_MEDICAO");

                            //var dateElement = volumesListElements.ElementAt(j).Element("DHA_MEDICAO");

                            if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                            {
                                var productionInDatabase = await _productionRepository.AnyByDate(date);

                                if (productionInDatabase is false)
                                    errorsInImport.Add($"Produção, TAG(DHA_MEDICAO) não encontrada para data: {date}");
                            }
                            else
                            {
                                errorsInImport.Add("Formato de data da notificação de falha(DHA_MEDIÇÂO) inválido, deve ser: dd/MM/yyyy");
                            }

                            var volume = dadosBasicos.LISTA_VOLUME[j];

                            var volumeMapped = _mapper.Map<VOLUME, Volume>(volume);
                            volumeMapped.DHA_MEDICAO_039 = XmlUtils.DateTimeWithoutTimeParser(volume.DHA_MEDICAO_039, errorsInFormat, "");
                            volumeMapped.DHA_MED_DECLARADO_039 = XmlUtils.DecimalParser(volume.DHA_MED_DECLARADO_039, errorsInFormat, "");
                            volumeMapped.DHA_MED_REGISTRADO_039 = XmlUtils.DecimalParser(volume.DHA_MED_REGISTRADO_039, errorsInFormat, "");

                            var measurementFixed = new VolumeFixedNfsm
                            {
                                MeasuredAt = volumeMapped.DHA_MEDICAO_039,
                                VolumeAfter = volumeMapped.DHA_MED_DECLARADO_039,
                                VolumeBefore = volumeMapped.DHA_MED_REGISTRADO_039
                            };

                            measurementsFixed.Add(measurementFixed);
                            volumeList.Add(volumeMapped);
                            //measurement.LISTA_VOLUME.Add(volumeMapped);
                        }
                    }

                    var calibrationListElements = dadosBasicosElement?.Elements("LISTA_CALIBRACAO")?.ToList();

                    if (dadosBasicos.LISTA_CALIBRACAO is not null /*&& measurement.LISTA_CALIBRACAO is not null*/ && calibrationListElements is not null)
                        for (var j = 0; j < dadosBasicos.LISTA_CALIBRACAO.Count; ++j)
                        {
                            //var dateString = dadosBasicos.LISTA_CALIBRACAO[j].;

                            //if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                            //{
                            //    var productionInDatabase = await _productionRepository.AnyByDate(date);

                            //    if (productionInDatabase is false)
                            //        throw new NotFoundException($"Medição não encontrada para esta data: {date}");
                            //}
                            //else
                            //{
                            //    throw new BadRequestException("Formato de data da medição(DHA_MEDIÇÂO) inválido, deve ser: dd/MM/yyyy");
                            //}

                            var calibration = dadosBasicos.LISTA_CALIBRACAO[j];
                            //var calibrationElement = calibrationListElements[j]?.Element("CALIBRACAO");

                            var calibrationMapped = _mapper.Map<CALIBRACAO, Calibration>(calibration);
                            calibrationMapped.DHA_FALHA_CALIBRACAO_039 = XmlUtils.DateTimeWithoutTimeParser(calibration.DHA_FALHA_CALIBRACAO_039, errorsInFormat, string.Empty);

                            calibrationMapped.DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039 = XmlUtils.DecimalParser(calibration.DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039, errorsInFormat, string.Empty);
                            calibrationMapped.DHA_NUM_FATOR_CALIBRACAO_ATUAL_039 = XmlUtils.DecimalParser(calibration.DHA_NUM_FATOR_CALIBRACAO_ATUAL_039, errorsInFormat, string.Empty);

                            calibrationList.Add(calibrationMapped);

                            //measurement.LISTA_CALIBRACAO.Add(calibrationMapped);
                        }

                    if (errorsInImport.Count == 0 && installation is not null && measuringPoint is not null)
                    {
                        var measurement = new Measurement
                        {
                            Id = Guid.NewGuid(),
                            COD_FALHA_039 = dadosBasicos.COD_FALHA_039,
                            COD_TAG_PONTO_MEDICAO_039 = dadosBasicos.COD_TAG_PONTO_MEDICAO_039,
                            DHA_COD_INSTALACAO_039 = dadosBasicos.DHA_COD_INSTALACAO_039,
                            COD_TAG_EQUIPAMENTO_039 = dadosBasicos.COD_TAG_EQUIPAMENTO_039,
                            COD_FALHA_SUPERIOR_039 = dadosBasicos.COD_FALHA_SUPERIOR_039,
                            DSC_TIPO_FALHA_039 = XmlUtils.ShortParser(dadosBasicos.DSC_TIPO_FALHA_039, errorsInFormat, dadosBasicosElement.Name.LocalName),
                            IND_TIPO_NOTIFICACAO_039 = dadosBasicos.IND_TIPO_NOTIFICACAO_039,
                            DHA_OCORRENCIA_039 = XmlUtils.DateTimeParser(dadosBasicos.DHA_OCORRENCIA_039, errorsInFormat, dadosBasicosElement?.Element("DHA_OCORRENCIA")?.Name.LocalName),
                            DHA_DETECCAO_039 = XmlUtils.DateTimeParser(dadosBasicos.DHA_DETECCAO_039, errorsInFormat, dadosBasicosElement?.Element("DHA_DETECCAO")?.Name.LocalName),
                            DHA_RETORNO_039 = XmlUtils.DateTimeParser(dadosBasicos.DHA_RETORNO_039, errorsInFormat, dadosBasicosElement?.Element("DHA_RETORNO")?.Name.LocalName),
                            DHA_NUM_PREVISAO_RETORNO_DIAS_039 = dadosBasicos.DHA_NUM_PREVISAO_RETORNO_DIAS_039,
                            DHA_DSC_FALHA_039 = dadosBasicos.DHA_DSC_FALHA_039,
                            DHA_DSC_ACAO_039 = dadosBasicos.DHA_DSC_ACAO_039,
                            DHA_DSC_METODOLOGIA_039 = dadosBasicos.DHA_DSC_METODOLOGIA_039,
                            DHA_NOM_RESPONSAVEL_RELATO_039 = dadosBasicos.DHA_NOM_RESPONSAVEL_RELATO_039,
                            DHA_NUM_SERIE_EQUIPAMENTO_039 = dadosBasicos.DHA_NUM_SERIE_EQUIPAMENTO_039,
                            FileName = data.File.FileName,
                            FileType = new FileType
                            {
                                Name = data.File.FileType,
                                Acronym = XmlUtils.FileAcronym039,

                            },
                            User = user,
                            MeasuringPoint = measuringPoint,
                            Installation = installation,
                            LISTA_BSW = bswList,
                            LISTA_CALIBRACAO = calibrationList,
                            LISTA_VOLUME = volumeList,
                        };

                        var measurement039DTO = _mapper.Map<Measurement, Client039DTO>(measurement);

                        measurement039DTO.Summary = new SummaryNfsmDto
                        {
                            UepName = installation.UepName,
                            MeasuringPoint = measurement039DTO.COD_TAG_PONTO_MEDICAO_039,
                            Equipment = measurement039DTO.COD_TAG_EQUIPAMENTO_039,
                            TypeOfFailure = measurement039DTO.DSC_TIPO_FALHA_039,
                            CodeFailure = measurement039DTO.COD_FALHA_039,
                            DateOfOcurrence = measurement039DTO.DHA_OCORRENCIA_039,
                            DetectionDate = measurement039DTO.DHA_DETECCAO_039,
                            ReturnDate = measurement039DTO.DHA_RETORNO_039,
                            DescriptionFailure = measurement039DTO.DHA_DSC_FALHA_039,
                            Action = measurement039DTO.DHA_DSC_ACAO_039,
                            Methodology = measurement039DTO.DHA_DSC_METODOLOGIA_039,
                            MeasurementsFixed = measurementsFixed,
                            ResponsibleReport = measurement039DTO.DHA_NOM_RESPONSAVEL_RELATO_039,
                            TypeOfNotification = measurement039DTO.IND_TIPO_NOTIFICACAO_039,
                            BswsFixed = bswsFixed,
                        };

                        responseResult.NFSMs.Add(measurement039DTO);
                    }

                }

                //}
            }

            if (errorsInImport.Count > 0)
                throw new BadRequestException($"Algum(s) erro(s) ocorreram durante a validação do arquivo de nome: {data.File.FileName}", errors: errorsInImport);

            if (errorsInFormat.Count > 0)
                throw new BadRequestException($"Algum(s) erro(s) de formatação ocorreram durante a validação do arquivo de nome: {data.File.FileName}", errors: errorsInFormat);

            return responseResult;
        }

        public async Task<NFSMImportResponseDto> Import(ResponseNFSMDTO body, User user)
        {
            foreach (var nfsm in body.NFSMs)
            {
                var nfsmInDatabase = await _repository
                .GetOneByCode(nfsm.COD_FALHA_039);

                if (nfsmInDatabase is not null && nfsmInDatabase.IsActive is true)
                    throw new ConflictException("Notificação de falha já importada");

                var installation = await _installationRepository
                    .GetByUEPCod(nfsm.DHA_COD_INSTALACAO_039);

                if (installation is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Installation>());

                var measuringPoint = await _measuringPointRepository
                    .GetByTagMeasuringPoint(nfsm.COD_TAG_PONTO_MEDICAO_039)
                    ?? throw new NotFoundException(ErrorMessages.NotFound<MeasuringPoint>());

                var fluid = WellProductionUtils.fluidGas;

                var oilCalculation = await _oilCalculationRepository
                    .GetOilVolumeCalculationByInstallationUEP(installation.UepCod);

                if (oilCalculation is not null)
                {
                    foreach (var section in oilCalculation.Sections)
                    {
                        if (section.MeasuringPoint is not null && section.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                            fluid = WellProductionUtils.fluidOil;
                    }

                    foreach (var tog in oilCalculation.TOGRecoveredOils)
                    {
                        if (tog.MeasuringPoint is not null && tog.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                            fluid = WellProductionUtils.fluidOil;
                    }

                    foreach (var dor in oilCalculation.DORs)
                    {
                        if (dor.MeasuringPoint is not null && dor.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                            fluid = WellProductionUtils.fluidOil;
                    }

                    foreach (var drain in oilCalculation.DrainVolumes)
                    {
                        if (drain.MeasuringPoint is not null && drain.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring)
                            fluid = WellProductionUtils.fluidOil;
                    }

                }

                var measurementsFixed = new List<Measurement>();
                var nfsmsProductionList = new List<NFSMsProductions>();

                for (var i = 0; i < nfsm.LISTA_VOLUME.Count; i++)
                {
                    var productionInXml = nfsm.LISTA_VOLUME[i];

                    DateTime productionInXmlDate = productionInXml.DHA_MEDICAO_039 is not null ? productionInXml.DHA_MEDICAO_039.Value : DateTime.MinValue;

                    var productionInDatabase = await _productionRepository.GetExistingByDate(productionInXmlDate);

                    if (productionInDatabase is null)
                        throw new NotFoundException($"Medição não encontrada para esta data: {productionInXmlDate}");

                    if (productionInXml.DHA_MEDICAO_039 > nfsm.DHA_DETECCAO_039)
                        throw new ConflictException("Data da medição não pode ser maior do que a data da detecção TAG: DHA_DETECÇÃO.");



                    //if (productionInXml.DHA_MEDICAO_039 > nfsm.DHA_RETORNO_039)
                    //    throw new ConflictException("Data da medição não pode ser maior do que a data que a falha foi corrigida, TAG: DHA_RETORNO.");

                    //if (Math.Round(productionInDatabase.TotalProduction, 2) != Math.Round(production.DHA_MED_REGISTRADO_039, 2)
                    //    throw new ConflictException($"Valor de produção anterior, difere da MED_REGISTRADO, para data {productionDate}, esperado:{productionInDatabase.TotalProduction} | recebido: {production.DHA_MED_DECLARADO_039}");

                    foreach (var measurement in productionInDatabase.Measurements)
                    {
                        measurementsFixed.Add(measurement);
                    }

                    var volumeProduction = new NFSMsProductions
                    {
                        Production = productionInDatabase,
                        MeasuredAt = productionInXmlDate,
                        VolumeAfter = fluid == WellProductionUtils.fluidOil ? productionInXml.DHA_MED_DECLARADO_039 : productionInXml.DHA_MED_DECLARADO_039 * 1000,
                        VolumeBefore = fluid == WellProductionUtils.fluidOil ? productionInXml.DHA_MED_REGISTRADO_039 : productionInXml.DHA_MED_REGISTRADO_039 * 1000,
                    };

                    if (nfsm.LISTA_VOLUME.Count == nfsm.LISTA_BSW.Count)
                        if (nfsm.LISTA_BSW[i].DHA_FALHA_BSW_039 == nfsm.LISTA_VOLUME[i].DHA_MEDICAO_039)
                        {
                            var bsw = nfsm.LISTA_BSW[i];

                            volumeProduction.Bsw = bsw.DHA_PCT_BSW_039;
                            volumeProduction.BswMax = bsw.DHA_PCT_MAXIMO_BSW_039;
                        }

                    nfsmsProductionList.Add(volumeProduction);
                }

                var fileInfo = new FileBasicInfoDTO
                {
                    Acronym = XmlUtils.FileAcronym039,
                    Type = XmlUtils.File039,
                    Name = body.File.FileName
                };

                var importHistory = await CreateNfsmFileHistory(user, fileInfo, body.File.FileContent, body.NFSMs[0].DHA_DETECCAO_039);

                var createdNfsm = new NFSM
                {
                    Id = Guid.NewGuid(),
                    Action = nfsm.DHA_DSC_ACAO_039,
                    CodeFailure = nfsm.COD_FALHA_039,
                    DescriptionFailure = nfsm.DHA_DSC_FALHA_039,
                    TypeOfFailure = nfsm.DSC_TIPO_FALHA_039,
                    DateOfOcurrence = nfsm.DHA_OCORRENCIA_039,
                    Methodology = nfsm.DHA_DSC_METODOLOGIA_039,
                    Measurements = measurementsFixed,
                    Installation = installation,
                    MeasuringPoint = measuringPoint,
                    Productions = nfsmsProductionList,
                    ImportHistory = importHistory,
                    ReponsibleReport = nfsm.DHA_NOM_RESPONSAVEL_RELATO_039,
                    DetectionDate = nfsm.DHA_DETECCAO_039,
                    ReturnDate = nfsm.DHA_RETORNO_039,
                    TypeOfNotification = nfsm.IND_TIPO_NOTIFICACAO_039,
                };


                nfsmsProductionList.ForEach(nfsmsProduction => nfsmsProduction.NFSM = createdNfsm);

                await _repository.AddAsync(createdNfsm);
                await _repository.AddRangeNFSMsProductionsAsync(nfsmsProductionList);



                await _repository.SaveChangesAsync();
                return new NFSMImportResponseDto { Id = createdNfsm.Id, Status = "Success", Message = "Arquivo importado com sucesso." };
            }

            return new NFSMImportResponseDto { Id = Guid.Empty, Status = "Error", Message = "Nenhum dado foi importado." };
        }

        public async Task<NFSMHistory> CreateNfsmFileHistory(User user, FileBasicInfoDTO file, string base64, DateTime dateDetected)
        {
            DateTime result;

            if (!DateTime.TryParseExact(dateDetected.ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                throw new BadRequestException("Formato de data de detecção de falha inválido, formato aceitável: dd/MM/yyyy HH:mm:ss");
            }

            var history = new NFSMHistory
            {
                Id = Guid.NewGuid(),
                TypeOperation = HistoryColumns.Import,
                ImportedBy = user,
                ImportedAt = DateTime.UtcNow.AddHours(-3),
                FileAcronym = file.Acronym,
                FileName = file.Name,
                FileType = file.Type,
                FileContent = base64,
                MeasuredAt = result,
            };

            await _repository.AddHistoryAsync(history);

            return history;
        }

        public async Task<List<NFSMGetAllDto>> GetAll()
        {
            var nfsms = await _repository.GetAll();

            var nfsmsDTO = new List<NFSMGetAllDto>();

            foreach (var nfsm in nfsms)
            {
                var measurementsFixed = new List<NFSMsProductionsDto>();
                var bswsFixed = new List<BswFixedNfsm>();

                if (nfsm.Productions is not null)
                    foreach (var measurementFixed in nfsm.Productions)
                    {
                        measurementsFixed.Add(new NFSMsProductionsDto
                        {
                            Id = measurementFixed.Id,
                            MeasuredAt = measurementFixed.MeasuredAt.ToString("dd/MM/yyyy"),
                            VolumeAfter = measurementFixed.VolumeAfter,
                            VolumeBefore = measurementFixed.VolumeBefore,
                        });

                        bswsFixed.Add(new BswFixedNfsm
                        {
                            Bsw = measurementFixed.Bsw,
                            Date = measurementFixed.MeasuredAt,
                            MaxBsw = measurementFixed.BswMax
                        });
                    }

                var nfsmDTO = new NFSMGetAllDto
                {
                    Id = nfsm.Id,
                    Action = nfsm.Action,
                    CodeFailure = nfsm.CodeFailure,
                    DateOfOcurrence = nfsm.DateOfOcurrence,
                    DescriptionFailure = nfsm.DescriptionFailure,
                    Methodology = nfsm.Methodology,
                    TypeOfFailure = nfsm.TypeOfFailure,
                    Installation = _mapper.Map<CreateUpdateInstallationDTO>(nfsm.Installation),
                    MeasuringPoint = _mapper.Map<MeasuringPointWithoutInstallationDTO>(nfsm.MeasuringPoint),
                    MeasurementsFixed = measurementsFixed,
                    File = new FailureNotificationFilesDto
                    {
                        FileId = nfsm.ImportHistory.Id,
                        FileName = nfsm.ImportHistory.FileName,
                        FileType = nfsm.ImportHistory.FileType,
                        ImportedAt = nfsm.ImportHistory.ImportedAt
                    },
                    DetectionDate = nfsm.DetectionDate,
                    ReturnDateDetected = nfsm.ReturnDate,
                    ResponsibleReport = nfsm.ReponsibleReport,
                    TypeOfNotification = nfsm.TypeOfNotification,
                    BswsFixed = bswsFixed,
                    IsApplied = nfsm.IsApplied,
                };

                nfsmsDTO.Add(nfsmDTO);
            }

            return nfsmsDTO;
        }

        public async Task<NFSMGetAllDto> GetOne(Guid id)
        {
            var nfsm = await _repository.GetOneById(id);

            if (nfsm is null)
                throw new NotFoundException("NFSM não encontrada");

            var measurementsFixed = new List<NFSMsProductionsDto>();
            var bswsFixed = new List<BswFixedNfsm>();

            if (nfsm.Productions is not null)
                foreach (var measurementFixed in nfsm.Productions)
                {
                    measurementsFixed.Add(new NFSMsProductionsDto
                    {
                        Id = measurementFixed.Id,
                        MeasuredAt = measurementFixed.MeasuredAt.ToString("dd/MM/yyyy"),
                        VolumeAfter = measurementFixed.VolumeAfter,
                        VolumeBefore = measurementFixed.VolumeBefore,
                    });

                    bswsFixed.Add(new BswFixedNfsm
                    {
                        Bsw = measurementFixed.Bsw,
                        Date = measurementFixed.MeasuredAt,
                        MaxBsw = measurementFixed.BswMax,
                    });

                }

            var nfsmDTO = new NFSMGetAllDto
            {
                Id = nfsm.Id,
                Action = nfsm.Action,
                CodeFailure = nfsm.CodeFailure,
                DateOfOcurrence = nfsm.DateOfOcurrence,
                DescriptionFailure = nfsm.DescriptionFailure,
                Methodology = nfsm.Methodology,
                TypeOfFailure = nfsm.TypeOfFailure,
                Installation = _mapper.Map<CreateUpdateInstallationDTO>(nfsm.Installation),
                MeasuringPoint = _mapper.Map<MeasuringPointWithoutInstallationDTO>(nfsm.MeasuringPoint),
                MeasurementsFixed = measurementsFixed,
                File = new FailureNotificationFilesDto
                {
                    FileId = nfsm.ImportHistory.Id,
                    FileName = nfsm.ImportHistory.FileName,
                    FileType = nfsm.ImportHistory.FileType,
                    ImportedAt = nfsm.ImportHistory.ImportedAt
                },
                DetectionDate = nfsm.DetectionDate,
                ReturnDateDetected = nfsm.ReturnDate,
                ResponsibleReport = nfsm.ReponsibleReport,
                TypeOfNotification = nfsm.TypeOfNotification,
                BswsFixed = bswsFixed,
                IsApplied = nfsm.IsApplied
            };
            return nfsmDTO;

        }

        public async Task<ProductionFilesDtoWithBase64> DownloadNfsm(Guid id)
        {
            var nfsm = await _repository.GetOneById(id);

            if (nfsm is null)
                throw new NotFoundException("NFSM não encontrada");

            if (nfsm.ImportHistory is null)
                throw new NotFoundException("Histórico dessa NFSM não encontrada");

            var result = new ProductionFilesDtoWithBase64
            {
                Base64 = nfsm.ImportHistory.FileContent,
                FileId = nfsm.ImportHistory.Id,
                FileName = nfsm.ImportHistory.FileName,
                FileType = nfsm.ImportHistory.FileType,
                ImportedAt = nfsm.ImportHistory.ImportedAt
            };

            return result;
        }

        public async Task<NFSMImportResponseDto> ApplyNfsm(Guid id)
        {
            var nfsmInDatabase = await _repository
                .GetOneById(id)
                ?? throw new NotFoundException(ErrorMessages.NotFound<NFSM>());

            if (nfsmInDatabase.IsApplied)
                throw new ConflictException("Notificação de falha já foi aplicada anteriormente.");

            if (nfsmInDatabase.MeasuringPoint.IsActive is false)
                throw new ConflictException($"Não foi possível aplicar a notificação de falha, ponto de medição: {nfsmInDatabase.MeasuringPoint.TagPointMeasuring} está inativo.");

            //if (nfsmInDatabase.DateOfOcurrence > nfsmInDatabase.Da)
            //    throw new ConflictException("Data da medição não pode ser maior do que a data que a falha foi corrigida, TAG: DHA_RETORNO.");
            foreach (var measurementCorrected in nfsmInDatabase.Productions)
            {
                var productionInDatabase = await _productionRepository
                    .GetExistingByDate(measurementCorrected.MeasuredAt)
                    ?? throw new NotFoundException(ErrorMessages.NotFound<Production>());

                if (productionInDatabase.StatusProduction.ToLower() == ProductionUtils.openStatus)
                    throw new ConflictException("Produção precisa ter sido fechada para ser corrigida.");

                if (productionInDatabase.Measurements.Any(x => x.MeasuringPoint.TagPointMeasuring == nfsmInDatabase.MeasuringPoint.TagPointMeasuring) is false)
                    throw new NotFoundException($"Nenhuma medição com esse ponto de medição encontrada: {nfsmInDatabase.MeasuringPoint.TagPointMeasuring}");
            }

            foreach (var measurementCorrected in nfsmInDatabase.Productions)
            {
                var productionInDatabase = await _productionRepository
                    .GetExistingByDate(measurementCorrected.MeasuredAt);

                var originalTotalOil = productionInDatabase!.Oil?.TotalOil ?? 0;
                var originalTotalGasDiferencial = productionInDatabase.GasDiferencial?.TotalGas ?? 0;
                var originalTotalGasLinear = productionInDatabase.GasLinear?.TotalGas ?? 0;

                var originalGasBurned = productionInDatabase.Gas?.LimitOperacionalBurn + productionInDatabase.Gas?.ScheduledStopBurn + productionInDatabase.Gas?.ForCommissioningBurn + productionInDatabase.Gas?.VentedGas + productionInDatabase.Gas?.WellTestBurn + productionInDatabase.Gas?.EmergencialBurn + productionInDatabase.Gas?.OthersBurn;

                var totalLinear = 0m;
                var totalOil = 0m;
                var totalDiferencial = 0m;
                var totalGasBurnedDiferencial = 0m;
                var totalGasBurnedLinear = 0m;

                var oilChanged = false;
                var gasDiferencialChanged = false;
                var gasLinearChanged = false;

                foreach (var measurement in productionInDatabase.Measurements)
                {
                    if (measurement.DHA_INICIO_PERIODO_MEDICAO_003 is not null)
                    {
                        if (measurement.MeasuringPoint.TagPointMeasuring == nfsmInDatabase.MeasuringPoint.TagPointMeasuring && measurementCorrected.VolumeAfter is not null)
                        {
                            measurement.MED_CORRIGIDO_MVMDO_003 = measurementCorrected.VolumeAfter;

                            _measurementRepository.UpdateMeasurement(measurement);

                            //totalDiferencial += measurementCorrected.VolumeAfter.Value;

                            gasDiferencialChanged = true;
                        }

                    }

                    if (measurement.DHA_INICIO_PERIODO_MEDICAO_002 is not null)
                    {
                        if (measurement.MeasuringPoint.TagPointMeasuring == nfsmInDatabase.MeasuringPoint.TagPointMeasuring && measurementCorrected.VolumeAfter is not null)
                        {
                            measurement.MED_CORRIGIDO_MVMDO_002 = measurementCorrected.VolumeAfter;

                            _measurementRepository.UpdateMeasurement(measurement);

                            //totalLinear += measurementCorrected.VolumeAfter.Value;

                            gasLinearChanged = true;

                        }

                    }
                    if (measurement.DHA_INICIO_PERIODO_MEDICAO_001 is not null)
                    {

                        if (measurement.MeasuringPoint.TagPointMeasuring == nfsmInDatabase.MeasuringPoint.TagPointMeasuring && measurementCorrected.VolumeAfter is not null)
                        {
                            //measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001 = measurementCorrected.VolumeAfter;
                            measurement.VolumeAfterManualBsw_001 = measurementCorrected.VolumeAfter;
                            _measurementRepository.UpdateMeasurement(measurement);
                            //totalOil += measurementCorrected.VolumeAfter.Value;

                            oilChanged = true;
                        }
                    }
                }

                foreach (var measurement in productionInDatabase.Measurements)
                {
                    if (measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                    {
                        totalDiferencial += measurement.MED_CORRIGIDO_MVMDO_003.Value;
                    }

                    if (measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                    {
                        totalLinear += measurement.MED_CORRIGIDO_MVMDO_002.Value;
                    }

                    if (measurement.VolumeAfterManualBsw_001 is not null)
                    {
                        totalOil += measurement.VolumeAfterManualBsw_001.Value;
                    }
                }

                var isGasBurned = false;

                var gasCalculation = await _gasCalculationRepository
                    .GetGasVolumeCalculationByInstallationId(nfsmInDatabase.Installation.Id);

                foreach (var measurement in productionInDatabase.Measurements)
                {
                    if (gasCalculation is not null)
                    {
                        foreach (var hpFlare in gasCalculation.HPFlares)
                        {
                            if (hpFlare.MeasuringPoint is not null && nfsmInDatabase.MeasuringPoint.TagPointMeasuring == hpFlare.MeasuringPoint.TagPointMeasuring)
                            {
                                if ((hpFlare.MeasuringPoint is not null && (hpFlare.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_002 || hpFlare.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_003)))
                                {
                                    isGasBurned = true;
                                }

                                if (hpFlare.MeasuringPoint is not null && hpFlare.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_003 && measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                                {
                                    totalGasBurnedDiferencial += measurement.MED_CORRIGIDO_MVMDO_003.Value;

                                }

                                if (hpFlare.MeasuringPoint is not null && hpFlare.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_002 && measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                                {
                                    totalGasBurnedLinear += measurement.MED_CORRIGIDO_MVMDO_002.Value;

                                }
                            }

                        }

                        foreach (var lpFlare in gasCalculation.LPFlares)
                        {
                            if (lpFlare.MeasuringPoint is not null && nfsmInDatabase.MeasuringPoint.TagPointMeasuring == lpFlare.MeasuringPoint.TagPointMeasuring)
                            {
                                if (lpFlare.MeasuringPoint is not null && (lpFlare.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_002 || lpFlare.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_003))
                                    isGasBurned = true;

                                if (lpFlare.MeasuringPoint is not null && lpFlare.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_003 && measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                                {
                                    totalGasBurnedDiferencial += measurement.MED_CORRIGIDO_MVMDO_003.Value;

                                }

                                if (lpFlare.MeasuringPoint is not null && lpFlare.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_002 && measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                                {

                                    totalGasBurnedLinear += measurement.MED_CORRIGIDO_MVMDO_002.Value;
                                }
                            }
                        }

                        foreach (var assistance in gasCalculation.AssistanceGases)
                        {
                            if (assistance.MeasuringPoint is not null && nfsmInDatabase.MeasuringPoint.TagPointMeasuring == assistance.MeasuringPoint.TagPointMeasuring)
                            {
                                if (assistance.MeasuringPoint is not null && (assistance.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_002 || assistance.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_003))
                                    isGasBurned = true;

                                if (assistance.MeasuringPoint is not null && assistance.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_003 && measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                                {

                                    totalGasBurnedDiferencial += measurement.MED_CORRIGIDO_MVMDO_003.Value;

                                }

                                if (assistance.MeasuringPoint is not null && assistance.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_002 && measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                                {

                                    totalGasBurnedLinear += measurement.MED_CORRIGIDO_MVMDO_002.Value;
                                }
                            }
                        }

                        foreach (var pilot in gasCalculation.PilotGases)
                        {
                            if (pilot.MeasuringPoint is not null && nfsmInDatabase.MeasuringPoint.TagPointMeasuring == pilot.MeasuringPoint.TagPointMeasuring)
                            {
                                if (pilot.MeasuringPoint is not null && (pilot.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_002 || pilot.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_003))
                                    isGasBurned = true;

                                if (pilot.MeasuringPoint is not null && pilot.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_003 && measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                                {

                                    totalGasBurnedDiferencial -= measurement.MED_CORRIGIDO_MVMDO_003.Value;
                                }

                                if (pilot.MeasuringPoint is not null && pilot.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_002 && measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                                {

                                    totalGasBurnedLinear -= measurement.MED_CORRIGIDO_MVMDO_002.Value;
                                }
                            }
                        }

                        foreach (var purge in gasCalculation.PurgeGases)
                        {
                            if (purge.MeasuringPoint is not null && nfsmInDatabase.MeasuringPoint.TagPointMeasuring == purge.MeasuringPoint.TagPointMeasuring)
                            {
                                if (purge.MeasuringPoint is not null && (purge.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_002 || purge.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_003))
                                    isGasBurned = true;

                                if (purge.MeasuringPoint is not null && purge.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_003 && measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                                {

                                    totalGasBurnedDiferencial += measurement.MED_CORRIGIDO_MVMDO_003.Value;
                                }

                                if (purge.MeasuringPoint is not null && purge.MeasuringPoint.TagPointMeasuring == measurement.COD_TAG_PONTO_MEDICAO_002 && measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                                {

                                    totalGasBurnedLinear += measurement.MED_CORRIGIDO_MVMDO_002.Value;
                                }
                            }
                        }
                    }
                }

                if (oilChanged && productionInDatabase.Oil is not null)
                {
                    productionInDatabase.Oil.TotalOil = totalOil;
                    productionInDatabase.Oil.TotalOilWithoutBsw = totalOil;
                }

                if (gasDiferencialChanged && productionInDatabase.GasDiferencial is not null)
                {
                    productionInDatabase.GasDiferencial.TotalGas = totalDiferencial;
                    productionInDatabase.GasDiferencial.BurntGas = totalGasBurnedDiferencial;
                }

                if (gasLinearChanged && productionInDatabase.GasLinear is not null)
                {
                    productionInDatabase.GasLinear.BurntGas = totalGasBurnedLinear;
                    productionInDatabase.GasLinear.TotalGas = totalLinear;
                }
                var totalGasBurned = totalGasBurnedLinear + totalGasBurnedDiferencial;

                if (isGasBurned && productionInDatabase.Gas is not null && originalGasBurned != totalGasBurned)
                {
                    productionInDatabase.CanDetailGasBurned = true;
                    productionInDatabase.Gas.OthersBurn = totalGasBurned;

                    productionInDatabase.Gas.EmergencialBurn = 0;
                    productionInDatabase.Gas.ForCommissioningBurn = 0;
                    productionInDatabase.Gas.LimitOperacionalBurn = 0;
                    productionInDatabase.Gas.ScheduledStopBurn = 0;
                    productionInDatabase.Gas.VentedGas = 0;
                    productionInDatabase.Gas.WellTestBurn = 0;
                }

                productionInDatabase.TotalProduction = (productionInDatabase.Oil?.TotalOil ?? 0) +
                                     (productionInDatabase.GasDiferencial?.TotalGas ?? 0) +
                                     (productionInDatabase.GasLinear?.TotalGas ?? 0);


                Console.WriteLine(oilChanged);
                Console.WriteLine(gasDiferencialChanged);
                Console.WriteLine(gasLinearChanged);
                if (oilChanged || gasDiferencialChanged || gasLinearChanged)
                {
                    if (productionInDatabase.FieldsFR is not null)
                        foreach (var fieldFr in productionInDatabase.FieldsFR)
                        {
                            fieldFr.OilProductionInField = fieldFr.FROil is not null ? totalOil * fieldFr.FROil.Value : 0;
                            fieldFr.GasProductionInField = fieldFr.FRGas is not null ? (totalDiferencial + totalLinear) * fieldFr.FRGas.Value : 0;
                            fieldFr.TotalProductionInField = (fieldFr.FRGas is not null ? (totalDiferencial + totalLinear) * fieldFr.FRGas.Value : 0) + (fieldFr.FROil is not null ? totalOil * fieldFr.FROil.Value : 0);

                            fieldFr.ProductionInFieldAsPercentage = fieldFr.TotalProductionInField / productionInDatabase.TotalProduction;

                            _installationRepository.UpdateFr(fieldFr);
                        }


                    //var users = await _userService.GetAllEncryptedAdminUsers();
                    //Parallel.ForEach(users, async admin =>
                    //{
                    //    try
                    //    {

                    //await SendEmail.Send(nfsm, admin);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Console.WriteLine(ex);
                    //    }
                    //});

                    await _wellProductionService.ReAppropriateWithNfsm(productionInDatabase.Id);

                    productionInDatabase.StatusProduction = ProductionUtils.fixedStatus;

                    nfsmInDatabase.IsApplied = true;

                    _repository.Update(nfsmInDatabase);

                    _productionRepository.Update(productionInDatabase);

                }
            }

            if (nfsmInDatabase.IsApplied is false)
                throw new BadRequestException("Notificação de falha não aplicada, nenhuma produção foi corrigida.");

            await _repository.SaveChangesAsync();

            return new NFSMImportResponseDto { Id = id, Status = "Success", Message = "Produções corrigida(s)." };
        }
    }
}