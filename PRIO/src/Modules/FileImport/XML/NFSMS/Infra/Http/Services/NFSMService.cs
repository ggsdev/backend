using AutoMapper;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Infra.Http.Services;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.FileImport.XML.FileContent;
using PRIO.src.Modules.FileImport.XML.FileContent._039;
using PRIO.src.Modules.FileImport.XML.Infra.Utils;
using PRIO.src.Modules.FileImport.XML.NFSMS.Dtos;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.NFSMS.Interfaces;
using PRIO.src.Modules.FileImport.XML.NFSMS.ViewModels;
using PRIO.src.Modules.Hierarchy.Installations.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Dtos;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Dtos;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
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
        private readonly IOilVolumeCalculationRepository _oilRepository;
        private readonly UserService _userService;
        private readonly MeasurementService _measurementService;

        public ResponseNFSMDTO _responseResult = new();

        public NFSMService(IMapper mapper, IMeasurementHistoryRepository measurementHistoryRepository, IMeasurementRepository measurementRepository, IInstallationRepository installationRepository, IMeasuringPointRepository measuringPointRepository, IProductionRepository productionRepository, IOilVolumeCalculationRepository oilVolumeCalculation, MeasurementService measurementService, INFSMRepository repository, UserService userService)
        {
            _mapper = mapper;
            _measurementRepository = measurementRepository;
            _measurementHistoryRepository = measurementHistoryRepository;
            _installationRepository = installationRepository;
            _measuringPointRepository = measuringPointRepository;
            _productionRepository = productionRepository;
            _oilRepository = oilVolumeCalculation;
            _measurementService = measurementService;
            _repository = repository;
            _userService = userService;
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
                var result = Functions.CheckFormat(pathXml, pathSchema);
                if (result is not null && result.Count > 0)
                    throw new BadRequestException(string.Join(",", result));
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

                    if (nfsmInDatabase is not null)
                        errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª notificação(DADOS_BASICOS) com código de falha: {dadosBasicos.COD_FALHA_039} já existente.");

                    var installation = await _installationRepository
                              .GetInstallationMeasurementByUepAndAnpCodAsync(dadosBasicos.DHA_COD_INSTALACAO_039, XmlUtils.File039);

                    if (installation is null)
                        errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª notificação(DADOS_BASICOS): {ErrorMessages.NotFound<Installation>()}");

                    var measuringPoint = await _measuringPointRepository
                        .GetByTagMeasuringPointXML(dadosBasicos.COD_TAG_PONTO_MEDICAO_039, XmlUtils.File039);

                    if (measuringPoint is null)
                        errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª notificação(DADOS_BASICOS), ponto de medição TAG: {dadosBasicos.COD_TAG_PONTO_MEDICAO_039}: {ErrorMessages.NotFound<MeasuringPoint>()}");

                    if (installation is not null && installation.MeasuringPoints is not null)
                    {
                        bool contains = false;

                        foreach (var point in installation.MeasuringPoints)
                            if (measuringPoint is not null && measuringPoint.TagPointMeasuring == point.TagPointMeasuring)
                                contains = true;

                        if (contains is false)
                            errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª notificação(DADOS_BASICOS), TAG do ponto de medição não encontrado nessa instalação");
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
                            LISTA_BSW = new(),
                            LISTA_CALIBRACAO = new(),
                            LISTA_VOLUME = new(),
                        };
                        var listaBswElements = dadosBasicosElement?.Elements("LISTA_BSW")?.ToList();

                        var bswsFixed = new List<BswFixedNfsm>();

                        if (dadosBasicos.LISTA_BSW is not null && measurement.LISTA_BSW is not null && listaBswElements is not null)
                            for (var j = 0; j < dadosBasicos.LISTA_BSW.Count; ++j)
                            {
                                var dateString = dadosBasicos.LISTA_BSW[j].DHA_FALHA_BSW_039;

                                if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                                {
                                    var productionInDatabase = await _productionRepository.AnyByDate(date);

                                    if (productionInDatabase is false)
                                        errorsInImport.Add($"BSW não encontrado para esta data: {date}");
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
                                measurement.LISTA_BSW.Add(bswMapped);
                            }

                        var measurementsFixed = new List<VolumeFixedNfsm>();

                        var volumesListElements = dadosBasicosElement?.Elements("LISTA_VOLUME")?.ToList();

                        if (dadosBasicos.LISTA_VOLUME is not null && measurement.LISTA_VOLUME is not null && volumesListElements is not null)
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
                                        errorsInImport.Add($"Notificação de falha não encontrada para data: {date}");
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
                                measurement.LISTA_VOLUME.Add(volumeMapped);
                            }
                        }

                        var calibrationListElements = dadosBasicosElement?.Elements("LISTA_CALIBRACAO")?.ToList();

                        if (dadosBasicos.LISTA_CALIBRACAO is not null && measurement.LISTA_CALIBRACAO is not null && calibrationListElements is not null)
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

                                measurement.LISTA_CALIBRACAO.Add(calibrationMapped);
                            }


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
                            BswsFixed = bswsFixed
                        };

                        responseResult.NFSMs.Add(measurement039DTO);
                    }

                }
            }

            if (errorsInImport.Count > 0)
                throw new BadRequestException($"Algum(s) erro(s) ocorreram durante a validação do arquivo de nome: {data.File.FileName}", errors: errorsInImport);

            if (errorsInFormat.Count > 0)
                throw new BadRequestException($"Algum(s) erro(s) de formatação ocorreram durante a validação do arquivo de nome: {data.File.FileName}", errors: errorsInFormat);

            return responseResult;
        }

        public async Task<NFSMImportResponseDto> ImportAndFix(ResponseNFSMDTO body, User user)
        {
            foreach (var nfsm in body.NFSMs)
            {
                var nfsmInDatabase = await _repository
                .GetOneByCode(nfsm.COD_FALHA_039);

                if (nfsmInDatabase is not null)
                    throw new ConflictException("Notificação de falha já importada");

                var installation = await _installationRepository
                    .GetByUEPCod(nfsm.DHA_COD_INSTALACAO_039);

                if (installation is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Installation>());

                var measuringPoint = await _measuringPointRepository
                    .GetByTagMeasuringPoint(nfsm.COD_TAG_PONTO_MEDICAO_039);

                if (measuringPoint is null)
                    throw new NotFoundException(ErrorMessages.NotFound<MeasuringPoint>());

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

                    decimal totalOil = 0m;
                    decimal totalLinear = 0m;
                    decimal totalDiferencial = 0m;

                    foreach (var measurement in productionInDatabase.Measurements)
                    {
                        if (measurement.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring && measurement.DHA_INICIO_PERIODO_MEDICAO_002 is not null)
                        {
                            //    measurement.MED_CORRIGIDO_MVMDO_002 = productionInXml.DHA_MED_DECLARADO_039;

                            //    totalLinear += productionInXml.DHA_MED_DECLARADO_039 ?? 0;

                            //    _measurementRepository.UpdateMeasurement(measurement);

                            measurementsFixed.Add(measurement);
                        }

                        if (measurement.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring && measurement.DHA_INICIO_PERIODO_MEDICAO_001 is not null)
                        {

                            //measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001 = productionInXml.DHA_MED_DECLARADO_039;
                            //measurement.MED_VOLUME_LIQUIDO_MVMDO_001 = productionInXml.DHA_MED_DECLARADO_039;

                            //totalOil += productionInXml.DHA_MED_DECLARADO_039 ?? 0;

                            //_measurementRepository.UpdateMeasurement(measurement);
                            measurementsFixed.Add(measurement);
                        }

                        if (measurement.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring && measurement.DHA_INICIO_PERIODO_MEDICAO_003 is not null)
                        {
                            //measurement.MED_CORRIGIDO_MVMDO_003 = productionInXml.DHA_MED_DECLARADO_039;
                            //totalDiferencial += productionInXml.DHA_MED_DECLARADO_039 ?? 0;

                            //_measurementRepository.UpdateMeasurement(measurement);
                            measurementsFixed.Add(measurement);
                        }

                        //productionInDatabase.TotalProduction = totalOil + totalLinear + totalDiferencial;

                        //if (productionInDatabase.GasLinear is not null)
                        //    productionInDatabase.GasLinear.TotalGas = totalLinear;

                        //if (productionInDatabase.GasDiferencial is not null)
                        //    productionInDatabase.GasDiferencial.TotalGas = totalDiferencial;

                        //if (productionInDatabase.Oil is not null)
                        //    productionInDatabase.Oil.TotalOil = totalOil;

                        //_productionRepository.Update(productionInDatabase);
                        measurementsFixed.Add(measurement);
                    }

                    //if (productionInDatabase.FieldsFR is not null)
                    //    foreach (var fieldFr in productionInDatabase.FieldsFR)
                    //    {
                    //        fieldFr.ProductionInField += fieldFr.FROil is not null ? totalOil * fieldFr.FROil.Value : 0;
                    //        fieldFr.ProductionInField += fieldFr.FRGas is not null ? (totalDiferencial + totalLinear) * fieldFr.FRGas.Value : 0;

                    //        _installationRepository.UpdateFr(fieldFr);
                    //    }

                    var volumeProduction = new NFSMsProductions
                    {
                        Production = productionInDatabase,
                        MeasuredAt = productionInXmlDate,
                        VolumeAfter = productionInXml.DHA_MED_DECLARADO_039,
                        VolumeBefore = productionInXml.DHA_MED_REGISTRADO_039,
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
                //foreach (var productionInXml in nfsm.LISTA_VOLUME)
                //{
                //    DateTime productionInXmlDate = productionInXml.DHA_MEDICAO_039 is not null ? productionInXml.DHA_MEDICAO_039.Value : DateTime.MinValue;

                //    var productionInDatabase = await _productionRepository
                //        .GetExistingByDate(productionInXmlDate);
                //}

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

                //var users = await _userService.GetAllEncryptedAdminUsers();
                //Parallel.ForEach(users, async admin =>
                //{
                //    try
                //    {

                //        await SendEmail.Send(nfsm, admin);
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex);
                //    }
                //});

                await _repository.SaveChangesAsync();
                return new NFSMImportResponseDto { Id = createdNfsm.Id, Status = "Success", Message = "Arquivo importado com sucesso, medições corrigidas." };
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
                    BswsFixed = bswsFixed
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
                        MaxBsw = measurementFixed.BswMax
                    });

                }

            var nfsmDTO = new NFSMGetAllDto
            {
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
                BswsFixed = bswsFixed
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

        //public FileContentResponse DownloadErrors(List<string> errors)
        //{
        //    using var memoryStream = new MemoryStream();

        //    var pdfDoc = new PdfDocument(new PdfWriter(memoryStream));

        //    var document = new Document(pdfDoc);

        //    var titleParagraph = new Paragraph("< Erros Importação >")
        //        .SetTextAlignment(TextAlignment.CENTER)
        //        .SetFontSize(20);
        //    document.Add(titleParagraph);

        //    foreach (string error in errors)
        //    {
        //        var listItem = new ListItem("=> " + error)
        //            .SetMarginBottom(10);
        //        document.Add(listItem);
        //    }

        //    document.Close();

        //    byte[] pdfBytes = memoryStream.ToArray();

        //    var response = new FileContentResponse
        //    {
        //        ContentBase64 = Convert.ToBase64String(pdfBytes)
        //    };

        //    return response;
        //}


        //private string CleanAndDecode(string input)
        //{
        //    byte[] isoBytes = Encoding.GetEncoding("iso-8859-1").GetBytes(input);
        //    string utf8String = Encoding.UTF8.GetString(isoBytes);

        //    utf8String = CleanString(utf8String);

        //    return utf8String;
        //}

        //private string CleanString(string? input)
        //{
        //    if (input is null)
        //        return string.Empty;

        //    string cleanedValue = input.Replace("\n", "").Replace("\t", "").Trim();
        //    return cleanedValue;
        //}
    }
}