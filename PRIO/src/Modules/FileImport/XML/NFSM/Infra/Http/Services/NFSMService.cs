using AutoMapper;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.Dtos;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.FileImport.XML.FileContent;
using PRIO.src.Modules.FileImport.XML.FileContent._039;
using PRIO.src.Modules.FileImport.XML.Infra.Utils;
using PRIO.src.Modules.FileImport.XML.NFSMs.ViewModels;
using PRIO.src.Modules.FileImport.XML.NFSMS.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Utils;
using System.Text;
using System.Web;
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
        private readonly MeasurementService _measurementService;

        public ResponseNFSMDTO _responseResult = new();

        public NFSMService(IMapper mapper, IMeasurementHistoryRepository measurementHistoryRepository, IMeasurementRepository measurementRepository, IInstallationRepository installationRepository, IMeasuringPointRepository measuringPointRepository, IProductionRepository productionRepository, IOilVolumeCalculationRepository oilVolumeCalculation, MeasurementService measurementService, INFSMRepository repository)
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
            var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\.."));
            var relativeSchemaPath = Path.Combine("src", "Modules", "FileImport", "XML", "FileContent", $"_039\\Schema.xsd");
            var importId = Guid.NewGuid();
            var pathXml = Path.GetTempPath() + importId + ".xml";
            var pathSchema = Path.GetFullPath(Path.Combine(projectRoot, relativeSchemaPath));
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
                    ImportedAt = DateTime.UtcNow.ToString("dd/MM/yyyy"),
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
                    var nfsmInDatabase = await _measurementRepository
                        .GetUnique039Async(dadosBasicos.COD_FALHA_039);

                    if (nfsmInDatabase is not null)
                        errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª notificação(DADOS_BASICOS) com código de falha: {dadosBasicos.COD_FALHA_039} já existente.");

                    var installation = await _installationRepository
                              .GetInstallationMeasurementByUepAndAnpCodAsync(dadosBasicos.DHA_COD_INSTALACAO_039, XmlUtils.File039);

                    if (installation is null)
                        errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª medição(DADOS_BASICOS): {ErrorMessages.NotFound<Installation>()}");

                    var measuringPoint = await _measuringPointRepository
                        .GetByTagMeasuringPointXML(dadosBasicos.COD_TAG_PONTO_MEDICAO_039, XmlUtils.File039);

                    if (measuringPoint is null)
                        errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª medição(DADOS_BASICOS), ponto de medição TAG: {dadosBasicos.COD_TAG_PONTO_MEDICAO_039}: {ErrorMessages.NotFound<MeasuringPoint>()}");

                    if (installation is not null && installation.MeasuringPoints is not null)
                    {
                        bool contains = false;

                        foreach (var point in installation.MeasuringPoints)
                            if (measuringPoint is not null && measuringPoint.TagPointMeasuring == point.TagPointMeasuring)
                                contains = true;

                        if (contains is false)
                            errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª medição(DADOS_BASICOS), TAG do ponto de medição não encontrado nessa instalação");
                    }
                    if (errorsInImport.Count == 0 && installation is not null && measuringPoint is not null)
                    {
                        var cleanedDscFalha = CleanString(dadosBasicos.DHA_DSC_FALHA_039);
                        var cleanedDscAcao = CleanString(dadosBasicos.DHA_DSC_ACAO_039);
                        var cleanedDscMetodologia = CleanString(dadosBasicos.DHA_DSC_METODOLOGIA_039);

                        //var inputEncoding = Encoding.GetEncoding("iso-8859-1");
                        var decodedDscFalha = HttpUtility.HtmlDecode(cleanedDscFalha);
                        var decodedDscAcao = HttpUtility.HtmlDecode(cleanedDscAcao);
                        var decodedDscMetodologia = HttpUtility.HtmlDecode(cleanedDscMetodologia);

                        //var text = inputEncoding.GetString(input);
                        //var output = Encoding.UTF8.GetBytes(text);


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
                            DHA_DSC_FALHA_039 = decodedDscFalha,
                            DHA_DSC_ACAO_039 = decodedDscAcao,
                            DHA_DSC_METODOLOGIA_039 = decodedDscMetodologia,
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

                        if (dadosBasicos.LISTA_BSW is not null && measurement.LISTA_BSW is not null)
                            for (var j = 0; j < dadosBasicos.LISTA_BSW.Count; ++j)
                            {
                                var bsw = dadosBasicos.LISTA_BSW[j];
                                var bswElement = dadosBasicosElement?.Elements("LISTA_BSW")?.ElementAt(j)?.Element("BSW");

                                var bswMapped = _mapper.Map<BSW, Bsw>(bsw);
                                bswMapped.DHA_FALHA_BSW_039 = XmlUtils.DateTimeWithoutTimeParser(bsw.DHA_FALHA_BSW_039, errorsInFormat, bswElement?.Element("DHA_FALHA_BSW")?.Name.LocalName);
                                bswMapped.DHA_PCT_BSW_039 = XmlUtils.DecimalParser(bsw.DHA_PCT_BSW_039, errorsInFormat, bswElement?.Element("PCT_BSW")?.Name.LocalName);
                                bswMapped.DHA_PCT_MAXIMO_BSW_039 = XmlUtils.DecimalParser(bsw.DHA_PCT_MAXIMO_BSW_039, errorsInFormat, bswElement?.Element("PCT_MAXIMO_BSW")?.Name.LocalName);

                                measurement.LISTA_BSW.Add(bswMapped);
                            }

                        var measurementsFixed = new List<VolumeFixedNfsm>();
                        if (dadosBasicos.LISTA_VOLUME is not null && measurement.LISTA_VOLUME is not null)
                        {
                            for (var j = 0; j < dadosBasicos.LISTA_VOLUME.Count; ++j)
                            {
                                var volume = dadosBasicos.LISTA_VOLUME[j];
                                var volumeElement = dadosBasicosElement?.Elements("LISTA_VOLUME")?.ElementAt(j)?.Element("VOLUME");

                                var volumeMapped = _mapper.Map<VOLUME, Volume>(volume);
                                volumeMapped.DHA_MEDICAO_039 = XmlUtils.DateTimeWithoutTimeParser(volume.DHA_MEDICAO_039, errorsInFormat, volumeElement?.Element("DHA_MEDICAO")?.Name.LocalName);
                                volumeMapped.DHA_MED_DECLARADO_039 = XmlUtils.DecimalParser(volume.DHA_MED_DECLARADO_039, errorsInFormat, volumeElement?.Element("MED_DECLARADO")?.Name.LocalName);
                                volumeMapped.DHA_MED_REGISTRADO_039 = XmlUtils.DecimalParser(volume.DHA_MED_REGISTRADO_039, errorsInFormat, volumeElement?.Element("MED_REGISTRADO")?.Name.LocalName);

                                var measurementFixed = new VolumeFixedNfsm
                                {
                                    MeasuredAt = volumeMapped.DHA_MEDICAO_039,
                                    VolumeAfter = volumeMapped.DHA_MED_DECLARADO_039,
                                    VolumeBefore = volumeMapped.DHA_MED_REGISTRADO_039
                                };

                                measurement.LISTA_VOLUME.Add(volumeMapped);
                                measurementsFixed.Add(measurementFixed);
                            }
                        }

                        if (dadosBasicos.LISTA_CALIBRACAO is not null && measurement.LISTA_CALIBRACAO is not null)
                            for (var j = 0; j < dadosBasicos.LISTA_CALIBRACAO.Count; ++j)
                            {
                                var calibration = dadosBasicos.LISTA_CALIBRACAO[j];
                                var calibrationElement = dadosBasicosElement?.Elements("LISTA_CALIBRACAO")?.ElementAt(j)?.Element("CALIBRACAO");

                                var calibrationMapped = _mapper.Map<CALIBRACAO, Calibration>(calibration);
                                calibrationMapped.DHA_FALHA_CALIBRACAO_039 = XmlUtils.DateTimeWithoutTimeParser(calibration.DHA_FALHA_CALIBRACAO_039, errorsInFormat, calibrationElement?.Element("DHA_FALHA_CALIBRACAO")?.Name.LocalName);

                                calibrationMapped.DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039 = XmlUtils.DecimalParser(calibration.DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039, errorsInFormat, calibrationElement?.Element("NUM_FATOR_CALIBRACAO_ANTERIOR")?.Name.LocalName);
                                calibrationMapped.DHA_NUM_FATOR_CALIBRACAO_ATUAL_039 = XmlUtils.DecimalParser(calibration.DHA_NUM_FATOR_CALIBRACAO_ATUAL_039, errorsInFormat, calibrationElement?.Element("NUM_FATOR_CALIBRACAO_ATUAL")?.Name.LocalName);

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

        public async Task<ImportResponseDTO> ImportAndFix(ResponseNFSMDTO body, User user)
        {
            foreach (var nfsm in body.NFSMs)
            {
                var installation = await _installationRepository
                    .GetByUEPCod(nfsm.DHA_COD_INSTALACAO_039);

                if (installation is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Installation>());

                var measuringPoint = await _measuringPointRepository
                    .GetByTagMeasuringPoint(nfsm.COD_TAG_PONTO_MEDICAO_039);

                if (measuringPoint is null)
                    throw new NotFoundException(ErrorMessages.NotFound<MeasuringPoint>());

                foreach (var productionInXml in nfsm.LISTA_VOLUME)
                {
                    DateTime productionInXmlDate = productionInXml.DHA_MEDICAO_039 is not null ? productionInXml.DHA_MEDICAO_039.Value : DateTime.MinValue;

                    var productionInDatabase = await _productionRepository.GetExistingByDate(productionInXmlDate);

                    if (productionInDatabase is null)
                        throw new NotFoundException($"Medição não encontrada para esta data: {productionInXmlDate}");

                    if (productionInXml.DHA_MEDICAO_039 > nfsm.DHA_DETECCAO_039)
                        throw new ConflictException("Data da medição não pode ser maior do que a data da detecção TAG: DHA_DETECÇÃO.");

                    if (productionInXml.DHA_MEDICAO_039 > nfsm.DHA_RETORNO_039)
                        throw new ConflictException("Data da medição não pode ser maior do que a data que a falha foi corrigida, TAG: DHA_RETORNO.");

                    //if (Math.Round(productionInDatabase.TotalProduction, 2) != Math.Round(production.DHA_MED_REGISTRADO_039, 2)
                    //    throw new ConflictException($"Valor de produção anterior, difere da MED_REGISTRADO, para data {productionDate}, esperado:{productionInDatabase.TotalProduction} | recebido: {production.DHA_MED_DECLARADO_039}");
                }

                foreach (var productionInXml in nfsm.LISTA_VOLUME)
                {
                    DateTime productionInXmlDate = productionInXml.DHA_MEDICAO_039 is not null ? productionInXml.DHA_MEDICAO_039.Value : DateTime.MinValue;

                    var productionInDatabase = await _productionRepository
                        .GetExistingByDate(productionInXmlDate);

                    decimal totalOil = 0m;
                    decimal totalLinear = 0m;
                    decimal totalDiferencial = 0m;

                    foreach (var measurement in productionInDatabase.Measurements)
                    {
                        if (measurement.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring && measurement.DHA_INICIO_PERIODO_MEDICAO_002 is not null)
                        {
                            measurement.MED_CORRIGIDO_MVMDO_002 = productionInXml.DHA_MED_DECLARADO_039;
                            totalLinear += productionInXml.DHA_MED_DECLARADO_039 ?? 0;
                            _measurementRepository.UpdateMeasurement(measurement);
                        }

                        if (measurement.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring && measurement.DHA_INICIO_PERIODO_MEDICAO_001 is not null)
                        {
                            measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001 = productionInXml.DHA_MED_DECLARADO_039;
                            measurement.MED_VOLUME_LIQUIDO_MVMDO_001 = productionInXml.DHA_MED_DECLARADO_039;

                            totalOil += productionInXml.DHA_MED_DECLARADO_039 ?? 0;

                            _measurementRepository.UpdateMeasurement(measurement);
                        }

                        if (measurement.MeasuringPoint.TagPointMeasuring == measuringPoint.TagPointMeasuring && measurement.DHA_INICIO_PERIODO_MEDICAO_003 is not null)
                        {
                            measurement.MED_CORRIGIDO_MVMDO_003 = productionInXml.DHA_MED_DECLARADO_039;
                            totalDiferencial += productionInXml.DHA_MED_DECLARADO_039 ?? 0;

                            _measurementRepository.UpdateMeasurement(measurement);
                        }

                        productionInDatabase.TotalProduction = totalOil + totalLinear + totalDiferencial;

                        if (productionInDatabase.GasLinear is not null)
                            productionInDatabase.GasLinear.TotalGas = totalLinear;

                        if (productionInDatabase.GasDiferencial is not null)
                            productionInDatabase.GasDiferencial.TotalGas = totalDiferencial;

                        if (productionInDatabase.Oil is not null)
                            productionInDatabase.Oil.TotalOil = totalOil;

                        _productionRepository.Update(productionInDatabase);
                    }

                    if (productionInDatabase.FieldsFR is not null)
                        foreach (var fieldFr in productionInDatabase.FieldsFR)
                        {
                            fieldFr.ProductionInField += fieldFr.FROil is not null ? totalOil * fieldFr.FROil.Value : 0;
                            fieldFr.ProductionInField += fieldFr.FRGas is not null ? (totalDiferencial + totalLinear) * fieldFr.FRGas.Value : 0;

                            _installationRepository.UpdateFr(fieldFr);
                        }
                }


                var measurements = new List<Measurement>();

                //foreach(var measurementToBeFixed in nfsm.Summary.MeasurementsFixed)
                //{
                //    var measurementInDatabase = await _measurementRepository.Get


                //}

                //var createdNfsm = new NFSM
                //{
                //    Id = Guid.NewGuid(),
                //    Action = nfsm.DHA_DSC_ACAO_039,
                //    CodeFailure = nfsm.COD_FALHA_039,
                //    DateOfOcurrence = nfsm.DHA_OCORRENCIA_039,
                //    Methodology = nfsm.DHA_DSC_METODOLOGIA_039,
                //    Measurements = nfsm.Summary.MeasurementsFixed
                //};

                //await _repository.AddAsync(createdNfsm);

                //try
                //{
                //    SendEmail.Send(nfsm);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex);

                //}
            }

            var fileInfo = new FileBasicInfoDTO
            {
                Acronym = XmlUtils.FileAcronym039,
                Type = XmlUtils.File039,
                Name = body.File.FileName
            };

            await _measurementService.Import(user, fileInfo, body.File.FileContent, body.NFSMs[0].DHA_OCORRENCIA_039);

            await _repository.SaveChangesAsync();

            return new ImportResponseDTO { Status = "Success", Message = "Arquivo importado com sucesso, medições corrigidas." };
        }

        //public async Task<NFSMGetAllDto> GetAll()
        //{


        //}

        private string? CleanString(string? input)
        {
            if (input is null)
                return null;

            string cleanedValue = input.Replace("\n", "").Replace("\t", "").Trim();
            return cleanedValue;
        }
    }

}