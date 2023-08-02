//using AutoMapper;
//using PRIO.src.Modules.ControlAccess.Users.Dtos;
//using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
//using PRIO.src.Modules.FileImport.XML.Dtos;
//using PRIO.src.Modules.FileImport.XML.FileContent;
//using PRIO.src.Modules.FileImport.XML.FileContent._039;
//using PRIO.src.Modules.FileImport.XML.ViewModels;
//using PRIO.src.Shared.Errors;
//using PRIO.src.Shared.Utils;
//using System.Text;
//using System.Xml;
//using System.Xml.Linq;

//namespace PRIO.src.Modules.FileImport.XML.NFSMS.Infra.Http.Services
//{
//    public class NFSMService
//    {
//        private readonly IMapper _mapper;

//        public NFSMService(IMapper mapper)
//        {
//            _mapper = mapper;
//        }

//        public async Task<ResponseXmlDto> Validate(NFSMViewModel data, User user)
//        {
//            #region client side validations

//            var isValidExtension = data.File.FileName.ToLower().EndsWith(".xml");

//            if (isValidExtension is false)
//                throw new BadRequestException($"Formato arquivo inválido, deve ter a extensão xml. Importação falhou arquivo com nome: {data.File.FileName}");

//            var fileContent = data.File.ContentBase64.Replace("data:@file/xml;base64,", "");

//            if (Decrypt.TryParseBase64String(fileContent, out _) is false)
//                throw new BadRequestException("Não é um base64 válido");

//            var isValidFileName = new List<string>()
//                    {
//                        "039",
//                    }.Contains(data.File.FileType);

//            if (!isValidFileName)
//                throw new BadRequestException($"Deve pertencer a categoria 039. Importação falhou, arquivo com nome: {data.File.FileName}");

//            #endregion

//            var userDto = _mapper.Map<UserDTO>(user);
//            var response = new ResponseXmlDto();

//            #region pathing
//            var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\.."));
//            var relativeSchemaPath = Path.Combine("src", "Modules", "FileImport", "XML", "FileContent", $"_039\\Schema.xsd");
//            var importId = Guid.NewGuid();
//            var pathXml = Path.GetTempPath() + importId + ".xml";
//            var pathSchema = Path.GetFullPath(Path.Combine(projectRoot, relativeSchemaPath));
//            #endregion

//            #region writting, parsing

//            await File.WriteAllBytesAsync(pathXml, Convert.FromBase64String(fileContent));
//            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

//            var parserContext = new XmlParserContext(null, null, null, XmlSpace.None)
//            {
//                Encoding = Encoding.GetEncoding(1252)
//            };

//            using (var r = XmlReader.Create(pathXml, null, parserContext))
//            {
//                var result = Functions.CheckFormat(pathXml, pathSchema);
//                if (result is not null && result.Count > 0)
//                    throw new BadRequestException(string.Join(",", result));
//            }

//            var documentXml = XDocument.Load(pathXml);
//            #endregion

//            #region generic elements and basic validation
//            var rootElement = documentXml.Root;
//            var dadosBasicosElements = rootElement?.Elements("LISTA_DADOS_BASICOS")?.Elements("DADOS_BASICOS");

//            if (dadosBasicosElements is null)
//                throw new BadRequestException("LISTA_DADOS_BASICOS XML element cant be null");
//            #endregion

//            #region response
//            var genericFile = new MeasurementHistoryDto
//            {
//                FileContent = data.File.ContentBase64,
//                FileName = data.File.FileName,
//                FileType = data.File.FileType,
//                ImportedAt = DateTime.UtcNow.ToString("dd/MM/yyyy"),
//                ImportedBy = userDto,
//                ImportId = importId
//            };

//            #endregion

//            for (int k = 0; k < dadosBasicosElements.Count(); ++k)
//            {
//                var dadosBasicosElement = dadosBasicosElements.ElementAt(k);

//                #region elementos XML
//                var dadosBasicos = Functions.DeserializeXml<DADOS_BASICOS_039>(dadosBasicosElement);
//                #endregion

//                if (dadosBasicos is not null && dadosBasicos.COD_FALHA_039 is not null && dadosBasicos.DHA_COD_INSTALACAO_039 is not null && dadosBasicos.COD_TAG_PONTO_MEDICAO_039 is not null)
//                {
//                    var measurementInDatabase = await _repository
//                        .GetUnique039Async(dadosBasicos.COD_FALHA_039);

//                    if (measurementInDatabase is not null)
//                        errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª medição(DADOS_BASICOS) com código de falha: {dadosBasicos.COD_FALHA_039} já existente.");

//                    var installation = await _installationRepository
//                      .GetInstallationMeasurementByUepAndAnpCodAsync(dadosBasicos.DHA_COD_INSTALACAO_039, XmlUtils.File039);

//                    if (installation is null)
//                        errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª medição(DADOS_BASICOS): {ErrorMessages.NotFound<Installation>()}");

//                    var measuringPoint = await _measuringPointRepository
//                        .GetByTagMeasuringPointXML(dadosBasicos.COD_TAG_PONTO_MEDICAO_039, XmlUtils.File039);

//                    if (measuringPoint is null)
//                        errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª medição(DADOS_BASICOS), ponto de medição TAG: {dadosBasicos.COD_TAG_PONTO_MEDICAO_039}: {ErrorMessages.NotFound<MeasuringPoint>()}");

//                    if (installation is not null && installation.MeasuringPoints is not null)
//                    {
//                        bool contains = false;

//                        foreach (var point in installation.MeasuringPoints)
//                            if (measuringPoint is not null && measuringPoint.TagPointMeasuring == point.TagPointMeasuring)
//                                contains = true;

//                        if (contains is false)
//                            errorsInImport.Add($"Arquivo {data.File.FileName}, {k + 1}ª medição(DADOS_BASICOS), TAG do ponto de medição não encontrado nessa instalação");
//                    }

//                    if (errorsInImport.Count == 0 && installation is not null && measuringPoint is not null)
//                    {
//                        var measurement = new Measurement
//                        {
//                            Id = Guid.NewGuid(),
//                            COD_FALHA_039 = dadosBasicos.COD_FALHA_039,
//                            COD_TAG_PONTO_MEDICAO_039 = dadosBasicos.COD_TAG_PONTO_MEDICAO_039,
//                            DHA_COD_INSTALACAO_039 = dadosBasicos.DHA_COD_INSTALACAO_039,
//                            COD_TAG_EQUIPAMENTO_039 = dadosBasicos.COD_TAG_EQUIPAMENTO_039,
//                            COD_FALHA_SUPERIOR_039 = dadosBasicos.COD_FALHA_SUPERIOR_039,
//                            DSC_TIPO_FALHA_039 = XmlUtils.ShortParser(dadosBasicos.DSC_TIPO_FALHA_039, errorsInFormat, dadosBasicosElement.Name.LocalName),
//                            IND_TIPO_NOTIFICACAO_039 = dadosBasicos.IND_TIPO_NOTIFICACAO_039,
//                            DHA_OCORRENCIA_039 = XmlUtils.DateTimeParser(dadosBasicos.DHA_OCORRENCIA_039, errorsInFormat, dadosBasicosElement?.Element("DHA_OCORRENCIA")?.Name.LocalName),
//                            DHA_DETECCAO_039 = XmlUtils.DateTimeParser(dadosBasicos.DHA_DETECCAO_039, errorsInFormat, dadosBasicosElement?.Element("DHA_DETECCAO")?.Name.LocalName),
//                            DHA_RETORNO_039 = XmlUtils.DateTimeParser(dadosBasicos.DHA_RETORNO_039, errorsInFormat, dadosBasicosElement?.Element("DHA_RETORNO")?.Name.LocalName),
//                            DHA_NUM_PREVISAO_RETORNO_DIAS_039 = dadosBasicos.DHA_NUM_PREVISAO_RETORNO_DIAS_039,
//                            DHA_DSC_FALHA_039 = dadosBasicos.DHA_DSC_FALHA_039,
//                            DHA_DSC_ACAO_039 = dadosBasicos.DHA_DSC_ACAO_039,
//                            DHA_DSC_METODOLOGIA_039 = dadosBasicos.DHA_DSC_METODOLOGIA_039,
//                            DHA_NOM_RESPONSAVEL_RELATO_039 = dadosBasicos.DHA_NOM_RESPONSAVEL_RELATO_039,
//                            DHA_NUM_SERIE_EQUIPAMENTO_039 = dadosBasicos.DHA_NUM_SERIE_EQUIPAMENTO_039,
//                            FileName = data.File.FileName,
//                            FileType = new FileType
//                            {
//                                Name = data.File.FileType,
//                                Acronym = XmlUtils.FileAcronym039,

//                            },
//                            User = user,
//                            MeasuringPoint = measuringPoint,
//                            Installation = installation,
//                            LISTA_BSW = new(),
//                            LISTA_CALIBRACAO = new(),
//                            LISTA_VOLUME = new(),
//                        };

//                        if (dadosBasicos.LISTA_BSW is not null && measurement.LISTA_BSW is not null)
//                            for (var j = 0; j < dadosBasicos.LISTA_BSW.Count; ++j)
//                            {
//                                var bsw = dadosBasicos.LISTA_BSW[j];
//                                var bswElement = dadosBasicosElement?.Elements("LISTA_BSW")?.ElementAt(j)?.Element("BSW");

//                                var bswMapped = _mapper.Map<BSW, Bsw>(bsw);
//                                bswMapped.DHA_FALHA_BSW_039 = XmlUtils.DateTimeWithoutTimeParser(bsw.DHA_FALHA_BSW_039, errorsInFormat, bswElement?.Element("DHA_FALHA_BSW")?.Name.LocalName);
//                                bswMapped.DHA_PCT_BSW_039 = XmlUtils.DecimalParser(bsw.DHA_PCT_BSW_039, errorsInFormat, bswElement?.Element("PCT_BSW")?.Name.LocalName);
//                                bswMapped.DHA_PCT_MAXIMO_BSW_039 = XmlUtils.DecimalParser(bsw.DHA_PCT_MAXIMO_BSW_039, errorsInFormat, bswElement?.Element("PCT_MAXIMO_BSW")?.Name.LocalName);

//                                measurement.LISTA_BSW.Add(bswMapped);
//                            }

//                        if (dadosBasicos.LISTA_VOLUME is not null && measurement.LISTA_VOLUME is not null)
//                            for (var j = 0; j < dadosBasicos.LISTA_VOLUME.Count; ++j)
//                            {
//                                var volume = dadosBasicos.LISTA_VOLUME[j];
//                                var volumeElement = dadosBasicosElement?.Elements("LISTA_VOLUME")?.ElementAt(j)?.Element("VOLUME");

//                                var volumeMapped = _mapper.Map<VOLUME, Volume>(volume);
//                                volumeMapped.DHA_MEDICAO_039 = XmlUtils.DateTimeWithoutTimeParser(volume.DHA_MEDICAO_039, errorsInFormat, volumeElement?.Element("DHA_MEDICAO")?.Name.LocalName);
//                                volumeMapped.DHA_MED_DECLARADO_039 = XmlUtils.DecimalParser(volume.DHA_MED_DECLARADO_039, errorsInFormat, volumeElement?.Element("MED_DECLARADO")?.Name.LocalName);
//                                volumeMapped.DHA_MED_REGISTRADO_039 = XmlUtils.DecimalParser(volume.DHA_MED_REGISTRADO_039, errorsInFormat, volumeElement?.Element("MED_REGISTRADO")?.Name.LocalName);

//                                measurement.LISTA_VOLUME.Add(volumeMapped);
//                            }

//                        if (dadosBasicos.LISTA_CALIBRACAO is not null && measurement.LISTA_CALIBRACAO is not null)
//                            for (var j = 0; j < dadosBasicos.LISTA_CALIBRACAO.Count; ++j)
//                            {
//                                var calibration = dadosBasicos.LISTA_CALIBRACAO[j];
//                                var calibrationElement = dadosBasicosElement?.Elements("LISTA_CALIBRACAO")?.ElementAt(j)?.Element("CALIBRACAO");

//                                var calibrationMapped = _mapper.Map<CALIBRACAO, Calibration>(calibration);
//                                calibrationMapped.DHA_FALHA_CALIBRACAO_039 = XmlUtils.DateTimeWithoutTimeParser(calibration.DHA_FALHA_CALIBRACAO_039, errorsInFormat, calibrationElement?.Element("DHA_FALHA_CALIBRACAO")?.Name.LocalName);

//                                calibrationMapped.DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039 = XmlUtils.DecimalParser(calibration.DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039, errorsInFormat, calibrationElement?.Element("NUM_FATOR_CALIBRACAO_ANTERIOR")?.Name.LocalName);
//                                calibrationMapped.DHA_NUM_FATOR_CALIBRACAO_ATUAL_039 = XmlUtils.DecimalParser(calibration.DHA_NUM_FATOR_CALIBRACAO_ATUAL_039, errorsInFormat, calibrationElement?.Element("NUM_FATOR_CALIBRACAO_ATUAL")?.Name.LocalName);

//                                measurement.LISTA_CALIBRACAO.Add(calibrationMapped);
//                            }

//                        var measurement039DTO = _mapper.Map<Measurement, Client039DTO>(measurement);

//                        _responseResult._039File ??= new List<Client039DTO>();
//                        _responseResult._039File?.Add(measurement039DTO);
//                    }
//                }
//            }
//        }
//    }
//}