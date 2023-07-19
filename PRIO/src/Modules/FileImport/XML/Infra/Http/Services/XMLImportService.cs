using AutoMapper;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.Dtos;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.FileImport.XML.FileContent;
using PRIO.src.Modules.FileImport.XML.FileContent._001;
using PRIO.src.Modules.FileImport.XML.FileContent._002;
using PRIO.src.Modules.FileImport.XML.FileContent._003;
using PRIO.src.Modules.FileImport.XML.Infra.Utils;
using PRIO.src.Modules.FileImport.XML.ViewModels;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Interfaces;
using PRIO.src.Modules.Measuring.Measurements.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
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
        private readonly DTOFilesClient _responseResult;
        private readonly MeasurementService _measurementService;
        private readonly IInstallationRepository _installationRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMeasurementRepository _repository;

        [GeneratedRegex("(?<=data:@file/xml;base64,)\\w+")]
        private static partial Regex XmlRegex();
        public XMLImportService(IMapper mapper, IInstallationRepository installationRepository, IMeasurementRepository xMLImportRepository, IEquipmentRepository equipmentRepository, MeasurementService measurementService)
        {
            _mapper = mapper;
            _responseResult = new();
            _installationRepository = installationRepository;
            _repository = xMLImportRepository;
            _equipmentRepository = equipmentRepository;
            _measurementService = measurementService;
        }

        public async Task<DTOFilesClient> Validate(RequestXmlViewModel data, User user)
        {
            #region client side validations
            for (int i = 0; i < data.Files.Count; ++i)
            {
                var isValidExtension = data.Files[i].FileName.ToLower().EndsWith("xml");

                if (isValidExtension is false)
                    throw new BadRequestException($"Formato arquivo inválido, deve ter a extensão xml. Importação falhou arquivo com nome: {data.Files[i].FileName}");

                var fileContent = data.Files[i].ContentBase64.Replace("data:@file/xml;base64,", "");

                if (Decrypt.TryParseBase64String(fileContent, out byte[]? encriptedBytes) is false)
                    throw new BadRequestException("Não é um base64 válido");
                var isValidFileName = new List<string>()
                    {
                        "039",
                        "001",
                        "002",
                        "003"
                    }.Contains(data.Files[i].FileType);

                if (!isValidFileName)
                    throw new BadRequestException($"Deve pertencer a uma das categorias: 001, 002, 003, 039. Importação falhou, arquivo com nome: {data.Files[i].FileName}");
            }

            #endregion

            var errorsInImport = new List<string>();
            for (int i = 0; i < data.Files.Count; ++i)
            {
                var fileContent = data.Files[i].ContentBase64.Replace("data:@file/xml;base64,", "");

                #region pathing
                var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\.."));
                var relativeSchemaPath = Path.Combine("src", "Modules", "FileImport", "XML", "FileContent", $"_{data.Files[i].FileType}\\Schema.xsd");
                var pathXml = Path.GetTempPath() + "xmlImports.xml";
                var pathSchema = Path.GetFullPath(Path.Combine(projectRoot, relativeSchemaPath));
                #endregion

                #region writting, parsing

                await System.IO.File.WriteAllBytesAsync(pathXml, Convert.FromBase64String(fileContent));
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

                for (int k = 0; k < dadosBasicosElements.Count(); ++k)
                {
                    var dadosBasicosElement = dadosBasicosElements.ElementAt(k);

                    switch (data.Files[i].FileType)
                    {
                        #region 039
                        //case "039":
                        //    {
                        //        #region elementos XML
                        //        var dadosBasicos = Functions.DeserializeXml<DADOS_BASICOS_039>(dadosBasicosElement);
                        //        #endregion

                        //        if (dadosBasicos is not null && dadosBasicos.COD_FALHA_039 is not null && dadosBasicos.DHA_COD_INSTALACAO_039 is not null && dadosBasicos.COD_TAG_PONTO_MEDICAO_039 is not null)
                        //        {
                        //            var measurementInDatabase = await _repository
                        //                .GetUnique039Async(dadosBasicos.COD_FALHA_039);

                        //            if (measurementInDatabase is not null)
                        //                //throw new ConflictException($"Medição {XmlUtils.File039} com código de falha: {dadosBasicos.COD_FALHA_039} já existente");
                        //                errorsInImport.Add($"Medição {XmlUtils.File039} com código de falha: {dadosBasicos.COD_FALHA_039} já existente");


                        //            var installation = await _installationRepository
                        //              .GetInstallationMeasurementByUepAndAnpCodAsync(dadosBasicos.DHA_COD_INSTALACAO_039, XmlUtils.FileAcronym039);

                        //            if (installation is null)
                        //                //throw new NotFoundException($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): {ErrorMessages.NotFound<Installation>()}");
                        //                errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS");

                        //            var equipment = await _equipmentRepository.GetByTagMeasuringPoint(dadosBasicos.COD_TAG_PONTO_MEDICAO_039, XmlUtils.File039);

                        //            if (equipment is null)
                        //                //throw new NotFoundException($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS),equipamento com TAG do ponto de medição {dadosBasicos.COD_TAG_PONTO_MEDICAO_039}: {ErrorMessages.NotFound<MeasuringEquipment>()}");
                        //                errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS),equipamento com TAG do ponto de medição {dadosBasicos.COD_TAG_PONTO_MEDICAO_039}: {ErrorMessages.NotFound<MeasuringEquipment>()}");


                        //            if (installation is not null && installation.MeasuringPoints is not null)
                        //            {
                        //                bool contains = false;

                        //                foreach (var point in installation.MeasuringPoints)
                        //                    if (equipment is not null && equipment.TagMeasuringPoint == point.TagPointMeasuring)
                        //                        contains = true;

                        //                if (contains is false)
                        //                    //throw new BadRequestException($"Problema na {k + 1}ª medição do arquivo {data.Files[i].FileName}, TAG do ponto de medição não encontrado nessa instalação");
                        //                    errorsInImport.Add($"Problema na {k + 1}ª medição do arquivo {data.Files[i].FileName}, TAG do ponto de medição não encontrado nessa instalação");

                        //            }

                        //            if (errorsInImport.Count == 0 && installation is not null)
                        //            {
                        //                var measurement = _mapper.Map<Measurement>(dadosBasicos);
                        //                measurement.FileName = data.Files[i].FileName;
                        //                measurement.Id = Guid.NewGuid();
                        //                measurement.FileType = new FileType
                        //                {
                        //                    Name = data.Files[i].FileType,
                        //                    Acronym = XmlUtils.FileAcronym039,

                        //                };

                        //                measurement.User = user;
                        //                measurement.Installation = installation;
                        //                var measurement039DTO = _mapper.Map<Measurement, Client039DTO>(measurement);

                        //                _responseResult._039File ??= new List<Client039DTO>();
                        //                _responseResult._039File?.Add(measurement039DTO);
                        //            }
                        //        }

                        //        break;
                        //    }

                        #endregion

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
                                        var checkDateExists = await _repository.GetAnyByDate(dateBeginningMeasurement, XmlUtils.File001);

                                        if (checkDateExists)
                                            errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS) data: {producao.DHA_INICIO_PERIODO_MEDICAO_001} já existente");
                                    }
                                    else
                                    {
                                        errorsInImport.Add("Formato da tag DHA_INICIO_PERIODO_MEDICAO incorreto deve ser: dd/MM/yyyy HH:mm:ss");
                                    }

                                    var measurementInDatabase = await _repository.GetUnique001Async(dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_001);

                                    if (measurementInDatabase is not null)
                                        //throw new ConflictException($"Medição {XmlUtils.File001} com número de série do elemento primário: {dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_001} já existente");
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS) número de série do elemento primário: {dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_001} já existente");

                                    var installation = await _installationRepository.GetInstallationMeasurementByUepAndAnpCodAsync(dadosBasicos.COD_INSTALACAO_001, XmlUtils.File001);

                                    if (installation is null)
                                        //throw new NotFoundException($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): {ErrorMessages.NotFound<Installation>()}");
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): {ErrorMessages.NotFound<Installation>()}");

                                    var equipment = await _equipmentRepository.GetByTagMeasuringPoint(dadosBasicos.COD_TAG_PONTO_MEDICAO_001, XmlUtils.File001);

                                    if (equipment is null)
                                        //throw new NotFoundException($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), equipamento com TAG do ponto de medição {dadosBasicos.COD_TAG_PONTO_MEDICAO_001}: {ErrorMessages.NotFound<MeasuringEquipment>()}");
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), equipamento com TAG do ponto de medição {dadosBasicos.COD_TAG_PONTO_MEDICAO_001}: {ErrorMessages.NotFound<MeasuringEquipment>()}");


                                    if (installation is not null && installation.MeasuringPoints is not null)
                                    {
                                        bool contains = false;

                                        foreach (var point in installation.MeasuringPoints)
                                            if (equipment is not null && equipment.TagMeasuringPoint == point.TagPointMeasuring)
                                                contains = true;

                                        if (contains is false)
                                            //throw new BadRequestException($"Problema na {k + 1}ª medição do arquivo {data.Files[i].FileName}, TAG do ponto de medição não encontrado nessa instalação");
                                            errorsInImport.Add($"Problema na {k + 1}ª medição do arquivo {data.Files[i].FileName}, TAG do ponto de medição não encontrado nessa instalação");
                                    }
                                    if (errorsInImport.Count == 0 && installation is not null && equipment is not null && equipment.MeasuringPoint is not null)
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
                                            DHA_COLETA_001 = DateTime.TryParseExact(configuracaoCv.DHA_COLETA_001, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dhaColeta) ? dhaColeta : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_TEMPERATURA_001 = double.TryParse(configuracaoCv?.MED_TEMPERATURA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medTemperatura) ? medTemperatura : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRESSAO_ATMSA_001 = double.TryParse(configuracaoCv.MED_PRESSAO_ATMSA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medPressaoAtmsa) ? medPressaoAtmsa : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRESSAO_RFRNA_001 = double.TryParse(configuracaoCv.MED_PRESSAO_RFRNA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medPressaoRfrna) ? medPressaoRfrna : throw new BadRequestException("Modelo dados inválidos."),
                                            DSC_VERSAO_SOFTWARE_001 = configuracaoCv?.DSC_VERSAO_SOFTWARE_001,
                                            #endregion

                                            #region elemento primario
                                            ICE_METER_FACTOR_1_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_1_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceMeterFactor1) ? iceMeterFactor1 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_2_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_2_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_2_001) ? ICE_METER_FACTOR_2_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_3_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_3_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_3_001) ? ICE_METER_FACTOR_3_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_4_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_4_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_4_001) ? ICE_METER_FACTOR_4_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_5_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_5_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_5_001) ? ICE_METER_FACTOR_5_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_6_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_6_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_6_001) ? ICE_METER_FACTOR_6_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_7_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_7_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_7_001) ? ICE_METER_FACTOR_7_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_8_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_8_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_8_001) ? ICE_METER_FACTOR_8_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_9_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_9_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_9_001) ? ICE_METER_FACTOR_9_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_10_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_10_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_10_001) ? ICE_METER_FACTOR_10_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_11_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_11_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_11_001) ? ICE_METER_FACTOR_11_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_12_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_12_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_12_001) ? ICE_METER_FACTOR_12_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_13_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_13_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_13_001) ? ICE_METER_FACTOR_13_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_14_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_14_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_14_001) ? ICE_METER_FACTOR_14_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_15_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_15_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_15_001) ? ICE_METER_FACTOR_15_001 : throw new BadRequestException("Modelo dados inválidos."),

                                            QTD_PULSOS_METER_FACTOR_1_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_1_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_1_001) ? QTD_PULSOS_METER_FACTOR_1_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_2_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_2_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_2_001) ? QTD_PULSOS_METER_FACTOR_2_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_3_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_3_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_3_001) ? QTD_PULSOS_METER_FACTOR_3_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_4_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_4_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_4_001) ? QTD_PULSOS_METER_FACTOR_4_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_5_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_5_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_5_001) ? QTD_PULSOS_METER_FACTOR_5_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_6_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_6_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_6_001) ? QTD_PULSOS_METER_FACTOR_6_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_7_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_7_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_7_001) ? QTD_PULSOS_METER_FACTOR_7_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_8_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_8_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_8_001) ? QTD_PULSOS_METER_FACTOR_8_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_9_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_9_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_9_001) ? QTD_PULSOS_METER_FACTOR_9_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_10_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_10_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_10_001) ? QTD_PULSOS_METER_FACTOR_10_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_11_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_11_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_11_001) ? QTD_PULSOS_METER_FACTOR_11_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_12_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_12_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_12_001) ? QTD_PULSOS_METER_FACTOR_12_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_13_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_13_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_13_001) ? QTD_PULSOS_METER_FACTOR_13_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_14_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_14_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_14_001) ? QTD_PULSOS_METER_FACTOR_14_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_15_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_15_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_15_001) ? QTD_PULSOS_METER_FACTOR_15_001 : throw new BadRequestException("Modelo dados inválidos."),

                                            ICE_K_FACTOR_1_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_1_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor1) ? iceKFactor1 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_2_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_2_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor2) ? iceKFactor2 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_3_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_3_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor3) ? iceKFactor3 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_4_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_4_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor4) ? iceKFactor4 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_5_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_5_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor5) ? iceKFactor5 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_6_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_6_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor6) ? iceKFactor6 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_7_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_7_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor7) ? iceKFactor7 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_8_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_8_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor8) ? iceKFactor8 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_9_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_9_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor9) ? iceKFactor9 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_10_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_10_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor10) ? iceKFactor10 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_11_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_11_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor11) ? iceKFactor11 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_12_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_12_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor12) ? iceKFactor12 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_13_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_13_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_K_FACTOR_13_001) ? ICE_K_FACTOR_13_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_14_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_14_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_K_FACTOR_14_001) ? ICE_K_FACTOR_14_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_15_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_15_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_K_FACTOR_15_001) ? ICE_K_FACTOR_15_001 : throw new BadRequestException("Modelo dados inválidos."),

                                            QTD_PULSOS_K_FACTOR_1_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_1_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor1) ? qtdPulsosKFactor1 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_2_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_2_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor2) ? qtdPulsosKFactor2 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_3_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_3_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor3) ? qtdPulsosKFactor3 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_4_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_4_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor4) ? qtdPulsosKFactor4 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_5_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_5_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor5) ? qtdPulsosKFactor5 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_6_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_6_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor6) ? qtdPulsosKFactor6 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_7_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_7_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor7) ? qtdPulsosKFactor7 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_8_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_8_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor8) ? qtdPulsosKFactor8 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_9_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_9_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor9) ? qtdPulsosKFactor9 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_10_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_10_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor10) ? qtdPulsosKFactor10 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_11_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_11_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor11) ? qtdPulsosKFactor11 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_12_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_12_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_K_FACTOR_12_001) ? QTD_PULSOS_K_FACTOR_12_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_13_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_13_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_K_FACTOR_13_001) ? QTD_PULSOS_K_FACTOR_13_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_14_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_14_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_K_FACTOR_14_001) ? QTD_PULSOS_K_FACTOR_14_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_15_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_15_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_K_FACTOR_15_001) ? QTD_PULSOS_K_FACTOR_15_001 : throw new BadRequestException("Modelo dados inválidos."),

                                            ICE_CUTOFF_001 = double.TryParse(elementoPrimario?.ICE_CUTOFF_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceCutoff) ? iceCutoff : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_LIMITE_SPRR_ALARME_001 = double.TryParse(elementoPrimario?.ICE_LIMITE_SPRR_ALARME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceLimiteSprrAlarme) ? iceLimiteSprrAlarme : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_LIMITE_INFRR_ALARME_001 = double.TryParse(elementoPrimario?.ICE_LIMITE_INFRR_ALARME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceLimiteInfrrAlarme) ? iceLimiteInfrrAlarme : throw new BadRequestException("Modelo dados inválidos."),
                                            IND_HABILITACAO_ALARME_1_001 = elementoPrimario?.IND_HABILITACAO_ALARME_1_001,
                                            #endregion

                                            #region instrumento pressao
                                            NUM_SERIE_1_001 = instrumentoPressao?.NUM_SERIE_1_001,
                                            MED_PRSO_LIMITE_SPRR_ALRME_001 = double.TryParse(instrumentoPressao?.MED_PRSO_LIMITE_SPRR_ALRME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var limiteSprr) ? limiteSprr : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRSO_LMTE_INFRR_ALRME_001 = double.TryParse(instrumentoPressao?.MED_PRSO_LMTE_INFRR_ALRME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var limiteInfrr) ? limiteInfrr : throw new BadRequestException("Modelo dados inválidos."),
                                            IND_HABILITACAO_ALARME_2_001 = instrumentoPressao?.IND_HABILITACAO_ALARME_2_001,
                                            MED_PRSO_ADOTADA_FALHA_001 = double.TryParse(instrumentoPressao?.MED_PRSO_ADOTADA_FALHA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var adotFalha) ? adotFalha : throw new BadRequestException("Modelo dados inválidos."),
                                            DSC_ESTADO_INSNO_CASO_FALHA_001 = instrumentoPressao?.DSC_ESTADO_INSNO_CASO_FALHA_001,
                                            IND_TIPO_PRESSAO_CONSIDERADA_001 = instrumentoPressao?.IND_TIPO_PRESSAO_CONSIDERADA_001,
                                            #endregion

                                            #region instrumento temperatura

                                            NUM_SERIE_2_001 = instrumentoTemperatura?.NUM_SERIE_2_001,
                                            MED_TMPTA_SPRR_ALARME_001 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_SPRR_ALARME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var tmptaAl) ? tmptaAl : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_TMPTA_INFRR_ALRME_001 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_INFRR_ALRME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var infrrAl) ? infrrAl : throw new BadRequestException("Modelo dados inválidos."),
                                            IND_HABILITACAO_ALARME_3_001 = instrumentoTemperatura?.IND_HABILITACAO_ALARME_3_001,
                                            MED_TMPTA_ADTTA_FALHA_001 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_ADTTA_FALHA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var adtta) ? adtta : throw new BadRequestException("Modelo dados inválidos."),
                                            DSC_ESTADO_INSTRUMENTO_FALHA_1_001 = instrumentoTemperatura?.DSC_ESTADO_INSTRUMENTO_FALHA_1_001,

                                            #endregion

                                            #region producao

                                            DHA_INICIO_PERIODO_MEDICAO_001 = DateTime.TryParseExact(producao?.DHA_INICIO_PERIODO_MEDICAO_001, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var inicioPer) ? inicioPer : throw new BadRequestException("Modelo dados inválidos."),
                                            DHA_FIM_PERIODO_MEDICAO_001 = DateTime.TryParseExact(producao?.DHA_FIM_PERIODO_MEDICAO_001, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fimPer) ? fimPer : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_DENSIDADADE_RELATIVA_001 = double.TryParse(producao?.ICE_DENSIDADADE_RELATIVA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_DENSIDADADE_RELATIVA_001) ? ICE_DENSIDADADE_RELATIVA_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            //ICE_CORRECAO_BSW_001 = double.TryParse(producao?.ICE_CORRECAO_BSW_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_CORRECAO_BSW_001) ? ICE_CORRECAO_BSW_001 : 0throw new BadRequestException("Modelo dados inválidos.")
                                            ICE_CORRECAO_PRESSAO_LIQUIDO_001 = double.TryParse(producao?.ICE_CORRECAO_PRESSAO_LIQUIDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var pressaoLiq) ? pressaoLiq : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_CRRCO_TEMPERATURA_LIQUIDO_001 = double.TryParse(producao?.ICE_CRRCO_TEMPERATURA_LIQUIDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var crrcoTemp) ? crrcoTemp : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRESSAO_ESTATICA_001 = double.TryParse(producao?.MED_PRESSAO_ESTATICA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var MED_PRESSAO_ESTATICA_001) ? MED_PRESSAO_ESTATICA_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_TMPTA_FLUIDO_001 = double.TryParse(producao?.MED_TMPTA_FLUIDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var MED_TMPTA_FLUIDO_001) ? MED_TMPTA_FLUIDO_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_VOLUME_BRTO_CRRGO_MVMDO_001 = double.TryParse(producao?.MED_VOLUME_BRTO_CRRGO_MVMDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var MED_VOLUME_BRTO_CRRGO_MVMDO_001) ? MED_VOLUME_BRTO_CRRGO_MVMDO_001 : throw new BadRequestException($"Arquivo: {data.Files[i].FileName} campo: MED_VOLUME_BRTO_CRRGO_MVMDO não está no formato aceitável: 00000,00000, {k + 1} medição(DADOS_BASICOS)ª."),
                                            MED_VOLUME_BRUTO_MVMDO_001 = double.TryParse(producao?.MED_VOLUME_BRUTO_MVMDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var MED_VOLUME_BRUTO_MVMDO_001) ? MED_VOLUME_BRUTO_MVMDO_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            //MED_VOLUME_LIQUIDO_MVMDO_001 = double.TryParse(producao?.MED_VOLUME_LIQUIDO_MVMDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var MED_VOLUME_LIQUIDO_MVMDO_001) ? MED_VOLUME_LIQUIDO_MVMDO_001 : 0throw new BadRequestException("Modelo dados inválidos.")
                                            MED_VOLUME_TTLZO_FIM_PRDO_001 = double.TryParse(producao?.MED_VOLUME_TTLZO_FIM_PRDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var MED_VOLUME_TTLZO_FIM_PRDO_001) ? MED_VOLUME_TTLZO_FIM_PRDO_001 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_VOLUME_TTLZO_INCO_PRDO_001 = double.TryParse(producao?.MED_VOLUME_TTLZO_INCO_PRDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var MED_VOLUME_TTLZO_INCO_PRDO_001) ? MED_VOLUME_TTLZO_INCO_PRDO_001 : throw new BadRequestException("Modelo dados inválidos."),

                                            #endregion

                                            FileName = data.Files[i].FileName,
                                            Installation = installation,
                                            User = user,

                                            FileType = new FileType
                                            {
                                                Name = data.Files[i].FileType,
                                                Acronym = XmlUtils.FileAcronym001,

                                            },

                                        };

                                        var measurement001DTO = _mapper.Map<Measurement, Client001DTO>(measurement);
                                        measurement001DTO.Summary = new ClientInfo
                                        {
                                            Date = dateBeginningMeasurement,
                                            Status = true,
                                            LocationMeasuringPoint = equipment.MeasuringPoint.Name,
                                            TagMeasuringPoint = equipment.TagMeasuringPoint,
                                            Volume = measurement.MED_VOLUME_BRUTO_MVMDO_001,

                                        };
                                        _responseResult._001File ??= new List<Client001DTO>();
                                        _responseResult._001File?.Add(measurement001DTO);
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
                                        var checkDateExists = await _repository.GetAnyByDate(dateBeginningMeasurement, XmlUtils.File002);

                                        if (checkDateExists)
                                            errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS) data: {producao.DHA_INICIO_PERIODO_MEDICAO_002} já existente");
                                    }
                                    else
                                    {
                                        errorsInImport.Add("Formato da tag DHA_INICIO_PERIODO_MEDICAO incorreto deve ser: dd/MM/yyyy HH:mm:ss");
                                    }

                                    var measurementInDatabase = await _repository.GetUnique002Async(dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_002);

                                    if (measurementInDatabase is not null)
                                        //throw new ConflictException($"Medição {XmlUtils.File002} com número de série do elemento primário: {dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_002} já existente");
                                        errorsInImport.Add($"Medição {XmlUtils.File002} com número de série do elemento primário: {dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_002} já existente");

                                    var installation = await _installationRepository.GetInstallationMeasurementByUepAndAnpCodAsync(dadosBasicos.COD_INSTALACAO_002, XmlUtils.File002);

                                    if (installation is null)
                                        //throw new NotFoundException($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): {ErrorMessages.NotFound<Installation>()}");
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): {ErrorMessages.NotFound<Installation>()}");

                                    var equipment = await _equipmentRepository.GetByTagMeasuringPoint(dadosBasicos.COD_TAG_PONTO_MEDICAO_002, XmlUtils.File002);

                                    if (equipment is null)
                                        //throw new NotFoundException($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), equipamento com TAG do ponto de medição {dadosBasicos.COD_TAG_PONTO_MEDICAO_002}: {ErrorMessages.NotFound<MeasuringEquipment>()}");
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), equipamento com TAG do ponto de medição {dadosBasicos.COD_TAG_PONTO_MEDICAO_002}: {ErrorMessages.NotFound<MeasuringEquipment>()}");

                                    if (installation is not null && installation.MeasuringPoints is not null && equipment is not null)
                                    {
                                        bool contains = false;

                                        foreach (var point in installation.MeasuringPoints)
                                            if (equipment.TagMeasuringPoint == point.TagPointMeasuring)
                                                contains = true;

                                        if (contains is false)
                                            //throw new BadRequestException($"Problema na {k + 1}ª medição do arquivo {data.Files[i].FileName}, TAG do ponto de medição não encontrado nessa instalação");
                                            errorsInImport.Add($"Problema na {k + 1}ª medição(DADOS_BASICOS) do arquivo {data.Files[i].FileName}, TAG do ponto de medição: {equipment.TagMeasuringPoint} não encontrado na instalação: {installation.Name}");
                                    }
                                    if (errorsInImport.Count == 0 && installation is not null && equipment is not null && equipment.MeasuringPoint is not null)
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
                                            DHA_COLETA_002 = DateTime.TryParseExact(configuracaoCv?.DHA_COLETA_002, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var DHA_COLETA_002) ? DHA_COLETA_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_TEMPERATURA_1_002 = double.TryParse(configuracaoCv?.MED_TEMPERATURA_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medTemp) ? medTemp : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRESSAO_ATMSA_002 = double.TryParse(configuracaoCv?.MED_PRESSAO_ATMSA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medPressAt) ? medPressAt : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRESSAO_RFRNA_002 = double.TryParse(configuracaoCv?.MED_PRESSAO_RFRNA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medPressRf) ? medPressRf : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_DENSIDADE_RELATIVA_002 = double.TryParse(configuracaoCv?.MED_DENSIDADE_RELATIVA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medDens) ? medDens : throw new BadRequestException("Modelo dados inválidos."),
                                            DSC_NORMA_UTILIZADA_CALCULO_002 = configuracaoCv?.DSC_NORMA_UTILIZADA_CALCULO_002,
                                            PCT_CROMATOGRAFIA_NITROGENIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_NITROGENIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double pctCromaNit) ? pctCromaNit : throw new BadRequestException("Modelo dados inválidos."),

                                            PCT_CROMATOGRAFIA_CO2_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_CO2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double pctCromaCO2) ? pctCromaCO2 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_METANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_METANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_METANO_002) ? PCT_CROMATOGRAFIA_METANO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_ETANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_ETANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_ETANO_002) ? PCT_CROMATOGRAFIA_ETANO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_PROPANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_PROPANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_PROPANO_002) ? PCT_CROMATOGRAFIA_PROPANO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_N_BUTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_N_BUTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_N_BUTANO_002) ? PCT_CROMATOGRAFIA_N_BUTANO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_I_BUTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_I_BUTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_I_BUTANO_002) ? PCT_CROMATOGRAFIA_I_BUTANO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_N_PENTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_N_PENTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_N_PENTANO_002) ? PCT_CROMATOGRAFIA_N_PENTANO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_I_PENTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_I_PENTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_I_PENTANO_002) ? PCT_CROMATOGRAFIA_I_PENTANO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_HEXANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HEXANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HEXANO_002) ? PCT_CROMATOGRAFIA_HEXANO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_HEPTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HEPTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HEPTANO_002) ? PCT_CROMATOGRAFIA_HEPTANO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_OCTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_OCTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_OCTANO_002) ? PCT_CROMATOGRAFIA_OCTANO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_NONANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_NONANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_NONANO_002) ? PCT_CROMATOGRAFIA_NONANO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_DECANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_DECANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_DECANO_002) ? PCT_CROMATOGRAFIA_DECANO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_H2S_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_H2S_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_H2S_002) ? PCT_CROMATOGRAFIA_H2S_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_AGUA_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_AGUA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_AGUA_002) ? PCT_CROMATOGRAFIA_AGUA_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_HELIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HELIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HELIO_002) ? PCT_CROMATOGRAFIA_HELIO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_OXIGENIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_OXIGENIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_OXIGENIO_002) ? PCT_CROMATOGRAFIA_OXIGENIO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_CO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_CO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_CO_002) ? PCT_CROMATOGRAFIA_CO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_HIDROGENIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HIDROGENIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HIDROGENIO_002) ? PCT_CROMATOGRAFIA_HIDROGENIO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_ARGONIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_ARGONIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_ARGONIO_002) ? PCT_CROMATOGRAFIA_ARGONIO_002 : throw new BadRequestException("Modelo dados inválidos."),

                                            DSC_VERSAO_SOFTWARE_002 = configuracaoCv?.DSC_VERSAO_SOFTWARE_002,

                                            #endregion

                                            #region elemento primario
                                            ICE_METER_FACTOR_1_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_1_002) ? ICE_METER_FACTOR_1_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_2_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_2_002) ? ICE_METER_FACTOR_2_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_3_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_3_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_3_002) ? ICE_METER_FACTOR_3_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_4_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_4_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_4_002) ? ICE_METER_FACTOR_4_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_5_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_5_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_5_002) ? ICE_METER_FACTOR_5_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_6_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_6_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_6_002) ? ICE_METER_FACTOR_6_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_7_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_7_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_7_002) ? ICE_METER_FACTOR_7_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_8_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_8_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_8_002) ? ICE_METER_FACTOR_8_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_9_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_9_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_9_002) ? ICE_METER_FACTOR_9_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_10_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_10_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_10_002) ? ICE_METER_FACTOR_10_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_11_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_11_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_11_002) ? ICE_METER_FACTOR_11_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_12_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_12_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_12_002) ? ICE_METER_FACTOR_12_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_13_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_13_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_13_002) ? ICE_METER_FACTOR_13_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_14_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_14_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_14_002) ? ICE_METER_FACTOR_14_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_METER_FACTOR_15_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_15_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_15_002) ? ICE_METER_FACTOR_15_002 : throw new BadRequestException("Modelo dados inválidos."),

                                            QTD_PULSOS_METER_FACTOR_1_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_1_002) ? QTD_PULSOS_METER_FACTOR_1_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_2_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_2_002) ? QTD_PULSOS_METER_FACTOR_2_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_3_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_3_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_3_002) ? QTD_PULSOS_METER_FACTOR_3_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_4_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_4_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_4_002) ? QTD_PULSOS_METER_FACTOR_4_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_5_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_5_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_5_002) ? QTD_PULSOS_METER_FACTOR_5_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_6_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_6_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_6_002) ? QTD_PULSOS_METER_FACTOR_6_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_7_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_7_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_7_002) ? QTD_PULSOS_METER_FACTOR_7_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_8_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_8_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_8_002) ? QTD_PULSOS_METER_FACTOR_8_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_9_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_9_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_9_002) ? QTD_PULSOS_METER_FACTOR_9_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_10_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_10_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_10_002) ? QTD_PULSOS_METER_FACTOR_10_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_11_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_11_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_11_002) ? QTD_PULSOS_METER_FACTOR_11_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_12_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_12_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_12_002) ? QTD_PULSOS_METER_FACTOR_12_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_13_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_13_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_13_002) ? QTD_PULSOS_METER_FACTOR_13_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_14_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_14_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_14_002) ? QTD_PULSOS_METER_FACTOR_14_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_METER_FACTOR_15_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_15_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_15_002) ? QTD_PULSOS_METER_FACTOR_15_002 : throw new BadRequestException("Modelo dados inválidos."),

                                            ICE_K_FACTOR_1_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_1_002) ? ICE_K_FACTOR_1_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_2_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_2_002) ? ICE_K_FACTOR_2_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_3_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_3_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_3_002) ? ICE_K_FACTOR_3_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_4_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_4_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_4_002) ? ICE_K_FACTOR_4_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_5_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_5_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_5_002) ? ICE_K_FACTOR_5_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_6_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_6_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_6_002) ? ICE_K_FACTOR_6_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_7_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_7_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_7_002) ? ICE_K_FACTOR_7_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_8_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_8_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_8_002) ? ICE_K_FACTOR_8_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_9_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_9_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_9_002) ? ICE_K_FACTOR_9_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_10_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_10_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_10_002) ? ICE_K_FACTOR_10_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_11_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_11_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_11_002) ? ICE_K_FACTOR_11_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_12_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_12_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_12_002) ? ICE_K_FACTOR_12_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_13_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_13_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_13_002) ? ICE_K_FACTOR_13_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_14_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_14_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_14_002) ? ICE_K_FACTOR_14_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_K_FACTOR_15_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_15_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_15_002) ? ICE_K_FACTOR_15_002 : throw new BadRequestException("Modelo dados inválidos."),

                                            QTD_PULSOS_K_FACTOR_1_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_1_002) ? QTD_PULSOS_K_FACTOR_1_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_2_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_2_002) ? QTD_PULSOS_K_FACTOR_2_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_3_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_3_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_3_002) ? QTD_PULSOS_K_FACTOR_3_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_4_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_4_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_4_002) ? QTD_PULSOS_K_FACTOR_4_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_5_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_5_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_5_002) ? QTD_PULSOS_K_FACTOR_5_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_6_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_6_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_6_002) ? QTD_PULSOS_K_FACTOR_6_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_7_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_7_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_7_002) ? QTD_PULSOS_K_FACTOR_7_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_8_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_8_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_8_002) ? QTD_PULSOS_K_FACTOR_8_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_9_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_9_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_9_002) ? QTD_PULSOS_K_FACTOR_9_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_10_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_10_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_10_002) ? QTD_PULSOS_K_FACTOR_10_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_11_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_11_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_11_002) ? QTD_PULSOS_K_FACTOR_11_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_12_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_12_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_12_002) ? QTD_PULSOS_K_FACTOR_12_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_13_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_13_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_13_002) ? QTD_PULSOS_K_FACTOR_13_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_14_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_14_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_14_002) ? QTD_PULSOS_K_FACTOR_14_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            QTD_PULSOS_K_FACTOR_15_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_15_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_15_002) ? QTD_PULSOS_K_FACTOR_15_002 : throw new BadRequestException("Modelo dados inválidos."),

                                            ICE_CUTOFF_002 = double.TryParse(elementoPrimario?.ICE_CUTOFF_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_CUTOFF_002) ? ICE_CUTOFF_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_LIMITE_SPRR_ALARME_002 = double.TryParse(elementoPrimario?.ICE_LIMITE_SPRR_ALARME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_LIMITE_SPRR_ALARME_002) ? ICE_LIMITE_SPRR_ALARME_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_LIMITE_INFRR_ALARME_002 = double.TryParse(elementoPrimario?.ICE_LIMITE_INFRR_ALARME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_LIMITE_INFRR_ALARME_002) ? ICE_LIMITE_INFRR_ALARME_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            IND_HABILITACAO_ALARME_1_002 = elementoPrimario?.IND_HABILITACAO_ALARME_1_002,
                                            #endregion

                                            #region instrumento pressao

                                            NUM_SERIE_1_002 = instrumentoPressao?.NUM_SERIE_1_002,
                                            MED_PRSO_LIMITE_SPRR_ALRME_002 = double.TryParse(instrumentoPressao?.MED_PRSO_LIMITE_SPRR_ALRME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LIMITE_SPRR_ALRME_002) ? MED_PRSO_LIMITE_SPRR_ALRME_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRSO_LMTE_INFRR_ALRME_002 = double.TryParse(instrumentoPressao?.MED_PRSO_LMTE_INFRR_ALRME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LMTE_INFRR_ALRME_002) ? MED_PRSO_LMTE_INFRR_ALRME_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            IND_HABILITACAO_ALARME_2_002 = instrumentoPressao?.IND_HABILITACAO_ALARME_2_002,
                                            MED_PRSO_ADOTADA_FALHA_002 = double.TryParse(instrumentoPressao?.MED_PRSO_ADOTADA_FALHA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_ADOTADA_FALHA_002) ? MED_PRSO_ADOTADA_FALHA_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            DSC_ESTADO_INSNO_CASO_FALHA_002 = instrumentoPressao?.DSC_ESTADO_INSNO_CASO_FALHA_002,
                                            IND_TIPO_PRESSAO_CONSIDERADA_002 = instrumentoPressao?.IND_TIPO_PRESSAO_CONSIDERADA_002,

                                            #endregion

                                            #region instrumento temperatura
                                            NUM_SERIE_2_002 = instrumentoTemperatura?.NUM_SERIE_2_002,
                                            MED_TMPTA_SPRR_ALARME_002 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_SPRR_ALARME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TMPTA_SPRR_ALARME_002) ? MED_TMPTA_SPRR_ALARME_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_TMPTA_INFRR_ALRME_002 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_INFRR_ALRME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TMPTA_INFRR_ALRME_002) ? MED_TMPTA_INFRR_ALRME_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            IND_HABILITACAO_ALARME_3_002 = instrumentoTemperatura?.IND_HABILITACAO_ALARME_3_002,
                                            MED_TMPTA_ADTTA_FALHA_002 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_ADTTA_FALHA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TMPTA_ADTTA_FALHA_002) ? MED_TMPTA_ADTTA_FALHA_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            DSC_ESTADO_INSTRUMENTO_FALHA_002 = instrumentoTemperatura?.DSC_ESTADO_INSTRUMENTO_FALHA_002,

                                            #endregion

                                            #region producao
                                            DHA_INICIO_PERIODO_MEDICAO_002 = DateTime.TryParseExact(producao?.DHA_INICIO_PERIODO_MEDICAO_002, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var DHA_INICIO_PERIODO_MEDICAO_002) ? DHA_INICIO_PERIODO_MEDICAO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            DHA_FIM_PERIODO_MEDICAO_002 = DateTime.TryParseExact(producao?.DHA_FIM_PERIODO_MEDICAO_002, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var DHA_FIM_PERIODO_MEDICAO_002) ? DHA_FIM_PERIODO_MEDICAO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_DENSIDADE_RELATIVA_002 = double.TryParse(producao?.ICE_DENSIDADE_RELATIVA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_DENSIDADE_RELATIVA_002) ? ICE_DENSIDADE_RELATIVA_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRESSAO_ESTATICA_002 = double.TryParse(producao?.MED_PRESSAO_ESTATICA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRESSAO_ESTATICA_002) ? MED_PRESSAO_ESTATICA_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_TEMPERATURA_2_002 = double.TryParse(producao?.MED_TEMPERATURA_2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TEMPERATURA_2_002) ? MED_TEMPERATURA_2_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            PRZ_DURACAO_FLUXO_EFETIVO_002 = double.TryParse(producao?.PRZ_DURACAO_FLUXO_EFETIVO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PRZ_DURACAO_FLUXO_EFETIVO_002) ? PRZ_DURACAO_FLUXO_EFETIVO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_BRUTO_MOVIMENTADO_002 = double.TryParse(producao?.MED_BRUTO_MOVIMENTADO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_BRUTO_MOVIMENTADO_002) ? MED_BRUTO_MOVIMENTADO_002 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_CORRIGIDO_MVMDO_002 = double.TryParse(producao?.MED_CORRIGIDO_MVMDO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_CORRIGIDO_MVMDO_002) ? MED_CORRIGIDO_MVMDO_002 : throw new BadRequestException($"Arquivo: {data.Files[i].FileName} campo: MED_CORRIGIDO_MVMDO não está no formato aceitável: 00000,00000, {k + 1} medição(DADOS_BASICOS)ª."),
                                            #endregion

                                            FileName = data.Files[i].FileName,
                                            FileType = new FileType
                                            {
                                                Name = data.Files[i].FileType,
                                                Acronym = XmlUtils.FileAcronym002,

                                            },
                                            User = user,
                                            Installation = installation
                                        };

                                        var measurement002DTO = _mapper.Map<Measurement, Client002DTO>(measurement);
                                        measurement002DTO.Summary = new ClientInfo
                                        {
                                            Date = dateBeginningMeasurement,
                                            Status = true,
                                            LocationMeasuringPoint = equipment.MeasuringPoint.Name,
                                            TagMeasuringPoint = equipment.TagMeasuringPoint,
                                            Volume = measurement.MED_CORRIGIDO_MVMDO_002,

                                        };
                                        _responseResult._002File ??= new List<Client002DTO>();
                                        _responseResult._002File?.Add(measurement002DTO);
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
                                        var checkDateExists = await _repository.GetAnyByDate(dateBeginningMeasurement, XmlUtils.File003);

                                        if (checkDateExists)
                                            errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS) data: {producao.DHA_INICIO_PERIODO_MEDICAO_003} já existente");
                                    }
                                    else
                                    {
                                        errorsInImport.Add("Formato da tag DHA_INICIO_PERIODO_MEDICAO incorreto deve ser: dd/MM/yyyy HH:mm:ss");
                                    }

                                    var measurementInDatabase = await _repository.GetUnique003Async(dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_003);

                                    if (measurementInDatabase is not null)
                                        //throw new ConflictException($"Medição {XmlUtils.File003} com número de série do elemento primário: {dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_003} já existente");
                                        errorsInImport.Add($"Medição {XmlUtils.File003} com número de série do elemento primário: {dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_003} já existente");

                                    var installation = await _installationRepository.GetInstallationMeasurementByUepAndAnpCodAsync(dadosBasicos.COD_INSTALACAO_003, XmlUtils.File003);

                                    if (installation is null)
                                        //throw new NotFoundException($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): {ErrorMessages.NotFound<Installation>()}");
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS): {ErrorMessages.NotFound<Installation>()}");

                                    var equipment = await _equipmentRepository.GetByTagMeasuringPoint(dadosBasicos.COD_TAG_PONTO_MEDICAO_003, XmlUtils.File003);

                                    if (equipment is null)
                                        errorsInImport.Add($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), equipamento com TAG do ponto de medição {dadosBasicos.COD_TAG_PONTO_MEDICAO_003}: {ErrorMessages.NotFound<MeasuringEquipment>()}");
                                    //throw new NotFoundException($"Arquivo {data.Files[i].FileName}, {k + 1}ª medição(DADOS_BASICOS), equipamento com TAG do ponto de medição {dadosBasicos.COD_TAG_PONTO_MEDICAO_003}: {ErrorMessages.NotFound<MeasuringEquipment>()}");

                                    if (installation is not null && installation.MeasuringPoints is not null && equipment is not null)
                                    {
                                        bool contains = false;

                                        foreach (var point in installation.MeasuringPoints)
                                            if (equipment is not null && equipment.TagMeasuringPoint == point.TagPointMeasuring)
                                                contains = true;

                                        if (contains is false)
                                            //throw new BadRequestException($"Problema na {k + 1}ª medição do arquivo {data.Files[i].FileName}, TAG do ponto de medição não encontrado nessa instalação");
                                            errorsInImport.Add($"Problema na {k + 1}ª medição(DADOS_BASICOS) do arquivo {data.Files[i].FileName}, TAG do ponto de medição: {equipment.TagMeasuringPoint} não encontrado na instalação: {installation.Name}");
                                    }
                                    if (errorsInImport.Count == 0 && installation is not null && equipment is not null && equipment.MeasuringPoint is not null)
                                    {

                                        var measurement = new Measurement
                                        {
                                            Id = Guid.NewGuid(),

                                            #region atributos
                                            NUM_SERIE_ELEMENTO_PRIMARIO_003 = dadosBasicos?.NUM_SERIE_ELEMENTO_PRIMARIO_003,
                                            COD_INSTALACAO_003 = dadosBasicos?.COD_INSTALACAO_003,
                                            COD_TAG_PONTO_MEDICAO_003 = dadosBasicos?.COD_TAG_PONTO_MEDICAO_003,
                                            #endregion

                                            #region configuracao cv

                                            NUM_SERIE_COMPUTADOR_VAZAO_003 = configuracaoCv?.NUM_SERIE_COMPUTADOR_VAZAO_003,
                                            DHA_COLETA_003 = DateTime.TryParseExact(configuracaoCv?.DHA_COLETA_003, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var DHA_COLETA_003) ? DHA_COLETA_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_TEMPERATURA_1_003 = double.TryParse(configuracaoCv?.MED_TEMPERATURA_1_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TEMPERATURA_1_003) ? MED_TEMPERATURA_1_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRESSAO_ATMSA_003 = double.TryParse(configuracaoCv?.MED_PRESSAO_ATMSA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRESSAO_ATMSA_003) ? MED_PRESSAO_ATMSA_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRESSAO_RFRNA_003 = double.TryParse(configuracaoCv?.MED_PRESSAO_RFRNA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRESSAO_RFRNA_003) ? MED_PRESSAO_RFRNA_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_DENSIDADE_RELATIVA_003 = double.TryParse(configuracaoCv?.MED_DENSIDADE_RELATIVA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_DENSIDADE_RELATIVA_003) ? MED_DENSIDADE_RELATIVA_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            DSC_NORMA_UTILIZADA_CALCULO_003 = configuracaoCv?.DSC_NORMA_UTILIZADA_CALCULO_003,

                                            PCT_CROMATOGRAFIA_NITROGENIO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_NITROGENIO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_NITROGENIO_003) ? PCT_CROMATOGRAFIA_NITROGENIO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_CO2_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_CO2_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_CO2_003) ? PCT_CROMATOGRAFIA_CO2_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_METANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_METANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_METANO_003) ? PCT_CROMATOGRAFIA_METANO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_ETANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_ETANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_ETANO_003) ? PCT_CROMATOGRAFIA_ETANO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_PROPANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_PROPANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_PROPANO_003) ? PCT_CROMATOGRAFIA_PROPANO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_N_BUTANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_N_BUTANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_N_BUTANO_003) ? PCT_CROMATOGRAFIA_N_BUTANO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_I_BUTANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_I_BUTANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_I_BUTANO_003) ? PCT_CROMATOGRAFIA_I_BUTANO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_N_PENTANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_N_PENTANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_N_PENTANO_003) ? PCT_CROMATOGRAFIA_N_PENTANO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_I_PENTANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_I_PENTANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_I_PENTANO_003) ? PCT_CROMATOGRAFIA_I_PENTANO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_HEXANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HEXANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HEXANO_003) ? PCT_CROMATOGRAFIA_HEXANO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_HEPTANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HEPTANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HEPTANO_003) ? PCT_CROMATOGRAFIA_HEPTANO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_OCTANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_OCTANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_OCTANO_003) ? PCT_CROMATOGRAFIA_OCTANO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_NONANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_NONANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_NONANO_003) ? PCT_CROMATOGRAFIA_NONANO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_DECANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_DECANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_DECANO_003) ? PCT_CROMATOGRAFIA_DECANO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_H2S_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_H2S_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_H2S_003) ? PCT_CROMATOGRAFIA_H2S_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_AGUA_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_AGUA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_AGUA_003) ? PCT_CROMATOGRAFIA_AGUA_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_HELIO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HELIO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HELIO_003) ? PCT_CROMATOGRAFIA_HELIO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_OXIGENIO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_OXIGENIO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_OXIGENIO_003) ? PCT_CROMATOGRAFIA_OXIGENIO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_CO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_CO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_CO_003) ? PCT_CROMATOGRAFIA_CO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_HIDROGENIO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HIDROGENIO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HIDROGENIO_003) ? PCT_CROMATOGRAFIA_HIDROGENIO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PCT_CROMATOGRAFIA_ARGONIO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_ARGONIO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_ARGONIO_003) ? PCT_CROMATOGRAFIA_ARGONIO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            DSC_VERSAO_SOFTWARE_003 = configuracaoCv?.DSC_VERSAO_SOFTWARE_003,

                                            #endregion

                                            #region elemento primario
                                            CE_LIMITE_SPRR_ALARME_003 = double.TryParse(elementoPrimario?.CE_LIMITE_SPRR_ALARME_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double CE_LIMITE_SPRR_ALARME_003) ? CE_LIMITE_SPRR_ALARME_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_LIMITE_INFRR_ALARME_1_003 = double.TryParse(elementoPrimario?.ICE_LIMITE_INFRR_ALARME_1_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_LIMITE_INFRR_ALARME_1_003) ? ICE_LIMITE_INFRR_ALARME_1_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            IND_HABILITACAO_ALARME_1_003 = elementoPrimario?.IND_HABILITACAO_ALARME_1_003,

                                            #endregion

                                            #region instrumento pressao
                                            NUM_SERIE_1_003 = instrumentoPressao?.NUM_SERIE_1_003,
                                            MED_PRSO_LIMITE_SPRR_ALRME_1_003 = double.TryParse(instrumentoPressao?.MED_PRSO_LIMITE_SPRR_ALRME_1_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LIMITE_SPRR_ALRME_1_003) ? MED_PRSO_LIMITE_SPRR_ALRME_1_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRSO_LMTE_INFRR_ALRME_1_003 = double.TryParse(instrumentoPressao?.MED_PRSO_LMTE_INFRR_ALRME_1_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LMTE_INFRR_ALRME_1_003) ? MED_PRSO_LMTE_INFRR_ALRME_1_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRSO_ADOTADA_FALHA_1_003 = double.TryParse(instrumentoPressao?.MED_PRSO_ADOTADA_FALHA_1_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_ADOTADA_FALHA_1_003) ? MED_PRSO_ADOTADA_FALHA_1_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            DSC_ESTADO_INSNO_CASO_FALHA_1_003 = instrumentoPressao?.DSC_ESTADO_INSNO_CASO_FALHA_1_003,
                                            IND_TIPO_PRESSAO_CONSIDERADA_003 = instrumentoPressao?.IND_TIPO_PRESSAO_CONSIDERADA_003,
                                            IND_HABILITACAO_ALARME_2_003 = instrumentoPressao?.IND_HABILITACAO_ALARME_2_003,

                                            #endregion

                                            #region instrumento temperatura
                                            NUM_SERIE_2_003 = instrumentoTemperatura?.NUM_SERIE_2_003,
                                            MED_TMPTA_SPRR_ALARME_003 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_SPRR_ALARME_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TMPTA_SPRR_ALARME_003) ? MED_TMPTA_SPRR_ALARME_003 : 0,
                                            MED_TMPTA_INFRR_ALRME_003 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_INFRR_ALRME_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TMPTA_INFRR_ALRME_003) ? MED_TMPTA_INFRR_ALRME_003 : 0,
                                            IND_HABILITACAO_ALARME_3_003 = instrumentoTemperatura?.IND_HABILITACAO_ALARME_3_003,
                                            MED_TMPTA_ADTTA_FALHA_003 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_ADTTA_FALHA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TMPTA_ADTTA_FALHA_003) ? MED_TMPTA_ADTTA_FALHA_003 : 0,
                                            DSC_ESTADO_INSTRUMENTO_FALHA_003 = instrumentoTemperatura?.DSC_ESTADO_INSTRUMENTO_FALHA_003,
                                            #endregion

                                            #region placa orificio
                                            MED_DIAMETRO_REFERENCIA_003 = double.TryParse(placaOrificio?.MED_DIAMETRO_REFERENCIA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_DIAMETRO_REFERENCIA_003) ? MED_DIAMETRO_REFERENCIA_003 : 0,
                                            MED_TEMPERATURA_RFRNA_003 = double.TryParse(placaOrificio?.MED_TEMPERATURA_RFRNA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TEMPERATURA_RFRNA_003) ? MED_TEMPERATURA_RFRNA_003 : 0,
                                            DSC_MATERIAL_CONTRUCAO_PLACA_003 = placaOrificio?.DSC_MATERIAL_CONTRUCAO_PLACA_003,
                                            MED_DMTRO_INTRO_TRCHO_MDCO_003 = double.TryParse(placaOrificio?.MED_DMTRO_INTRO_TRCHO_MDCO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_DMTRO_INTRO_TRCHO_MDCO_003) ? MED_DMTRO_INTRO_TRCHO_MDCO_003 : 0,
                                            MED_TMPTA_TRCHO_MDCO_003 = double.TryParse(placaOrificio?.MED_TMPTA_TRCHO_MDCO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TMPTA_TRCHO_MDCO_003) ? MED_TMPTA_TRCHO_MDCO_003 : 0,
                                            DSC_MATERIAL_CNSTO_TRCHO_MDCO_003 = placaOrificio?.DSC_MATERIAL_CNSTO_TRCHO_MDCO_003,
                                            DSC_LCLZO_TMDA_PRSO_DFRNL_003 = placaOrificio?.DSC_LCLZO_TMDA_PRSO_DFRNL_003,
                                            IND_TOMADA_PRESSAO_ESTATICA_003 = placaOrificio?.IND_TOMADA_PRESSAO_ESTATICA_003,
                                            #endregion

                                            #region inst diferen pressao alta   
                                            NUM_SERIE_3_003 = instDiferenPressaoAlta?.NUM_SERIE_3_003,
                                            MED_PRSO_LIMITE_SPRR_ALRME_2_003 = double.TryParse(instDiferenPressaoAlta?.MED_PRSO_LIMITE_SPRR_ALRME_2_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LIMITE_SPRR_ALRME_2_003) ? MED_PRSO_LIMITE_SPRR_ALRME_2_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRSO_LMTE_INFRR_ALRME_2_003 = double.TryParse(instDiferenPressaoAlta?.MED_PRSO_LMTE_INFRR_ALRME_2_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LMTE_INFRR_ALRME_2_003) ? MED_PRSO_LMTE_INFRR_ALRME_2_003 : throw new BadRequestException("Modelo dados inválidos."),

                                            #endregion

                                            #region inst diferen pressao media
                                            NUM_SERIE_4_003 = instDiferenPressaoMedia?.NUM_SERIE_4_003,
                                            MED_PRSO_LIMITE_SPRR_ALRME_3_003 = double.TryParse(instDiferenPressaoMedia?.MED_PRSO_LIMITE_SPRR_ALRME_3_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LIMITE_SPRR_ALRME_3_003) ? MED_PRSO_LIMITE_SPRR_ALRME_3_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRSO_LMTE_INFRR_ALRME_3_003 = double.TryParse(instDiferenPressaoMedia?.MED_PRSO_LMTE_INFRR_ALRME_3_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LMTE_INFRR_ALRME_3_003) ? MED_PRSO_LMTE_INFRR_ALRME_3_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            #endregion

                                            #region inst diferen pressao baixa
                                            NUM_SERIE_5_003 = instDiferenPressaoBaixa?.NUM_SERIE_5_003,
                                            MED_PRSO_LIMITE_SPRR_ALRME_4_003 = double.TryParse(instDiferenPressaoBaixa?.MED_PRSO_LIMITE_SPRR_ALRME_4_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LIMITE_SPRR_ALRME_4_003) ? MED_PRSO_LIMITE_SPRR_ALRME_4_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRSO_LMTE_INFRR_ALRME_4_003 = double.TryParse(instDiferenPressaoBaixa?.MED_PRSO_LMTE_INFRR_ALRME_4_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LMTE_INFRR_ALRME_4_003) ? MED_PRSO_LMTE_INFRR_ALRME_4_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            IND_HABILITACAO_ALARME_4_003 = instDiferenPressaoBaixa?.IND_HABILITACAO_ALARME_4_003,
                                            MED_PRSO_ADOTADA_FALHA_2_003 = double.TryParse(instDiferenPressaoBaixa?.MED_PRSO_ADOTADA_FALHA_2_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_ADOTADA_FALHA_2_003) ? MED_PRSO_ADOTADA_FALHA_2_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            DSC_ESTADO_INSNO_CASO_FALHA_2_003 = instDiferenPressaoBaixa?.DSC_ESTADO_INSNO_CASO_FALHA_2_003,
                                            MED_CUTOFF_KPA_1_003 = double.TryParse(instDiferenPressaoBaixa?.MED_CUTOFF_KPA_1_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_CUTOFF_KPA_1_003) ? MED_CUTOFF_KPA_1_003 : throw new BadRequestException("Modelo dados inválidos."),

                                            #endregion

                                            #region inst diferen pressao principal
                                            NUM_SERIE_6_003 = instDiferenPressaoPrincipal?.NUM_SERIE_6_003,
                                            MED_PRSO_LIMITE_SPRR_ALRME_5_003 = double.TryParse(instDiferenPressaoPrincipal?.MED_PRSO_LIMITE_SPRR_ALRME_5_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LIMITE_SPRR_ALRME_5_003) ? MED_PRSO_LIMITE_SPRR_ALRME_5_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRSO_LMTE_INFRR_ALRME_5_003 = double.TryParse(instDiferenPressaoPrincipal?.MED_PRSO_LMTE_INFRR_ALRME_5_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LMTE_INFRR_ALRME_5_003) ? MED_PRSO_LMTE_INFRR_ALRME_5_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            IND_HABILITACAO_ALARME_5_003 = instDiferenPressaoPrincipal?.IND_HABILITACAO_ALARME_5_003,
                                            MED_PRSO_ADOTADA_FALHA_3_003 = double.TryParse(instDiferenPressaoPrincipal?.MED_PRSO_ADOTADA_FALHA_3_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_ADOTADA_FALHA_3_003) ? MED_PRSO_ADOTADA_FALHA_3_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            DSC_ESTADO_INSNO_CASO_FALHA_3_003 = instDiferenPressaoPrincipal?.DSC_ESTADO_INSNO_CASO_FALHA_3_003,
                                            MED_CUTOFF_KPA_2_003 = double.TryParse(instDiferenPressaoPrincipal?.MED_CUTOFF_KPA_2_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_CUTOFF_KPA_2_003) ? MED_CUTOFF_KPA_2_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            #endregion

                                            #region producao
                                            DHA_INICIO_PERIODO_MEDICAO_003 = DateTime.TryParseExact(producao?.DHA_INICIO_PERIODO_MEDICAO_003, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var DHA_INICIO_PERIODO_MEDICAO_003) ? DHA_INICIO_PERIODO_MEDICAO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            DHA_FIM_PERIODO_MEDICAO_003 = DateTime.TryParseExact(producao?.DHA_FIM_PERIODO_MEDICAO_003, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var DHA_FIM_PERIODO_MEDICAO_003) ? DHA_FIM_PERIODO_MEDICAO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            ICE_DENSIDADE_RELATIVA_003 = double.TryParse(producao?.ICE_DENSIDADE_RELATIVA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_DENSIDADE_RELATIVA_003) ? ICE_DENSIDADE_RELATIVA_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_DIFERENCIAL_PRESSAO_003 = double.TryParse(producao?.MED_DIFERENCIAL_PRESSAO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_DIFERENCIAL_PRESSAO_003) ? MED_DIFERENCIAL_PRESSAO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_PRESSAO_ESTATICA_003 = double.TryParse(producao?.MED_PRESSAO_ESTATICA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRESSAO_ESTATICA_003) ? MED_PRESSAO_ESTATICA_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_TEMPERATURA_2_003 = double.TryParse(producao?.MED_TEMPERATURA_2_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TEMPERATURA_2_003) ? MED_TEMPERATURA_2_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            PRZ_DURACAO_FLUXO_EFETIVO_003 = double.TryParse(producao?.PRZ_DURACAO_FLUXO_EFETIVO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PRZ_DURACAO_FLUXO_EFETIVO_003) ? PRZ_DURACAO_FLUXO_EFETIVO_003 : throw new BadRequestException("Modelo dados inválidos."),
                                            MED_CORRIGIDO_MVMDO_003 = double.TryParse(producao?.MED_CORRIGIDO_MVMDO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_CORRIGIDO_MVMDO_003) ? MED_CORRIGIDO_MVMDO_003 : throw new BadRequestException($"Arquivo: {data.Files[i].FileName} campo: MED_CORRIGIDO_MVMDO não está no formato aceitável: 00000,00000, {k + 1} medição(DADOS_BASICOS)ª."),
                                            #endregion

                                            FileName = data.Files[i].FileName,
                                            FileType = new FileType
                                            {
                                                Name = data.Files[i].FileType,
                                                Acronym = XmlUtils.FileAcronym003,

                                            },

                                            Installation = installation,
                                            User = user
                                        };

                                        var measurement003DTO = _mapper.Map<Measurement, Client003DTO>(measurement);
                                        measurement003DTO.Summary = new ClientInfo
                                        {
                                            Date = dateBeginningMeasurement,
                                            Status = true,
                                            LocationMeasuringPoint = equipment.MeasuringPoint.Name,
                                            TagMeasuringPoint = equipment.TagMeasuringPoint,
                                            Volume = measurement.MED_CORRIGIDO_MVMDO_003,

                                        };
                                        _responseResult._003File ??= new List<Client003DTO>();
                                        _responseResult._003File?.Add(measurement003DTO);
                                    }

                                }

                            }

                            break;

                            #endregion
                    }
                }


            }
            if (errorsInImport.Count > 0)
            {
                throw new BadRequestException($"Algum(s) erro(s) ocorreram durante a importação do arquivo:", errors: errorsInImport);
            }

            return _responseResult;
        }

        public async Task<ImportResponseDTO> Import(DTOFiles data, User user)
        {

            foreach (var file in data._001File)
            {
                var measurement = _mapper.Map<_001DTO, Measurement>(file);

                var measurementExists = await _repository.GetAnyAsync(measurement.Id);

                if (measurementExists is true)
                    throw new ConflictException($"Medição com número de série: {measurement.NUM_SERIE_ELEMENTO_PRIMARIO_001} já cadastrada.");

                var installation = await _installationRepository.GetInstallationMeasurementByUepAndAnpCodAsync(file.COD_INSTALACAO_001, XmlUtils.File001);

                if (installation is null)
                    throw new NotFoundException($"{ErrorMessages.NotFound<Installation>()} Código: {measurement.COD_INSTALACAO_001}");

                var fileInfo = new FileBasicInfoDTO
                {
                    Acronym = XmlUtils.FileAcronym001,
                    Name = file.FileName,
                    Type = XmlUtils.File001
                };

                measurement.User = user;

                measurement.FileType = new FileType
                {
                    Name = fileInfo.Type,
                    Acronym = fileInfo.Acronym,
                };

                measurement.Installation = installation;

                await _measurementService.Import(measurement, user, fileInfo);

                await _repository.AddAsync(measurement);
            }

            foreach (var file in data._002File)
            {
                var measurement = _mapper.Map<_002DTO, Measurement>(file);

                var measurementExists = await _repository.GetAnyAsync(measurement.Id);

                if (measurementExists is true)
                    throw new ConflictException($"Medição com número de série: {measurement.NUM_SERIE_ELEMENTO_PRIMARIO_002} já cadastrada.");

                var installation = await _installationRepository.GetInstallationMeasurementByUepAndAnpCodAsync(file.COD_INSTALACAO_002, XmlUtils.File002);

                if (installation is null)
                    throw new NotFoundException($"{ErrorMessages.NotFound<Installation>()} Código: {measurement.COD_INSTALACAO_002}");

                var fileInfo = new FileBasicInfoDTO
                {
                    Acronym = XmlUtils.FileAcronym002,
                    Name = file.FileName,
                    Type = XmlUtils.File002
                };

                measurement.User = user;

                measurement.FileType = new FileType
                {
                    Name = fileInfo.Type,
                    Acronym = fileInfo.Acronym,
                };

                measurement.Installation = installation;

                await _measurementService.Import(measurement, user, fileInfo);
                await _repository.AddAsync(measurement);

            }

            foreach (var file in data._003File)
            {
                var measurement = _mapper.Map<_003DTO, Measurement>(file);
                var measurementExists = await _repository.GetAnyAsync(measurement.Id);

                if (measurementExists is true)
                    throw new ConflictException($"Medição com número de série: {measurement.NUM_SERIE_ELEMENTO_PRIMARIO_003} já cadastrada.");

                var installation = await _installationRepository.GetInstallationMeasurementByUepAndAnpCodAsync(file.COD_INSTALACAO_003, XmlUtils.File003);

                if (installation is null)
                    throw new NotFoundException($"{ErrorMessages.NotFound<Installation>()} Código: {measurement.COD_INSTALACAO_003}");

                var fileInfo = new FileBasicInfoDTO
                {
                    Acronym = XmlUtils.FileAcronym003,
                    Name = file.FileName,
                    Type = XmlUtils.File003
                };

                measurement.User = user;

                measurement.FileType = new FileType
                {
                    Name = fileInfo.Type,
                    Acronym = fileInfo.Acronym,
                };

                measurement.Installation = installation;

                await _measurementService.Import(measurement, user, fileInfo);
                await _repository.AddAsync(measurement);

            }

            foreach (var file in data._039File)
            {
                var measurement = _mapper.Map<_039DTO, Measurement>(file);
                var measurementExists = await _repository.GetAnyAsync(measurement.Id);

                if (measurementExists is true)
                    throw new ConflictException($"Medição com código de falha: {measurement.COD_FALHA_039} já cadastrada.");

                var installation = await _installationRepository.GetInstallationMeasurementByUepAndAnpCodAsync(file.DHA_COD_INSTALACAO_039, XmlUtils.File039);

                if (installation is null)
                    throw new NotFoundException($"{ErrorMessages.NotFound<Installation>()} Código: {measurement.DHA_COD_INSTALACAO_039}");

                var fileInfo = new FileBasicInfoDTO
                {
                    Acronym = XmlUtils.FileAcronym039,
                    Name = file.FileName,
                    Type = XmlUtils.File039
                };

                measurement.User = user;

                measurement.FileType = new FileType
                {
                    Name = fileInfo.Type,
                    Acronym = fileInfo.Acronym,
                };

                measurement.Installation = installation;

                await _measurementService.Import(measurement, user, fileInfo);
                await _repository.AddAsync(measurement);
            }
            await _repository.SaveChangesAsync();

            if (_repository.CountAdded() == 0)
                throw new BadRequestException("Nenhuma medição foi adicionada", status: "Error");


            return new ImportResponseDTO { Status = "Success", Message = $"Arquivo importado com sucesso, {_repository.CountAdded()} medições importadas" };
        }

        public async Task<DTOFilesClient> GetAll(string? acronym, string? name)
        {
            var filesQuery = _repository.FileTypeBuilder();

            if (!string.IsNullOrEmpty(acronym))
            {
                var possibleAcronymValues = new List<string> { "PMO", "PMGL", "PMGD", "EFM" };
                var isValidValue = possibleAcronymValues.Contains(acronym.ToUpper().Trim());
                if (!isValidValue)
                    throw new BadRequestException("Acronym valid values are: PMO, PMGL, PMGD, EFM"
                    );

                filesQuery = _repository.FileTypeBuilderByAcronym(acronym);
            }

            if (!string.IsNullOrEmpty(name))
            {
                var possibleNameValues = new List<string> { "001", "002", "003", "039" };
                var isValidValue = possibleNameValues.Contains(name.ToUpper().Trim());
                if (!isValidValue)
                    throw new BadRequestException("Name valid values are: 001, 002, 003, 039"
                   );

                filesQuery = _repository.FileTypeBuilderByName(name);
            }

            var files = await _repository.FilesToListAsync(filesQuery);
            var measurements = files.SelectMany(file => file.Measurements);

            foreach (var measurement in measurements)
            {
                switch (measurement.FileType?.Name)
                {
                    case "001":
                        _responseResult._001File ??= new List<Client001DTO>();
                        _responseResult._001File.Add(_mapper.Map<Client001DTO>(measurement));
                        break;

                    case "002":
                        _responseResult._002File ??= new List<Client002DTO>();
                        _responseResult._002File.Add(_mapper.Map<Client002DTO>(measurement));
                        break;

                    case "003":
                        _responseResult._003File ??= new List<Client003DTO>();
                        _responseResult._003File.Add(_mapper.Map<Client003DTO>(measurement));
                        break;

                        //case "039":
                        //    _responseResult._039File ??= new List<Client039DTO>();
                        //    _responseResult._039File.Add(_mapper.Map<Client039DTO>(measurement));
                        //    break;
                }
            }

            return _responseResult;
        }

        public async Task<List<string>> DownloadErrors(List<string> errors)
        {
            return errors;
        }
    }
}
