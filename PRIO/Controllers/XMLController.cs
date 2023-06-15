using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS.FileImportDTOS.XMLFilesDTOS;
using PRIO.DTOS.GlobalDTOS;
using PRIO.Files.XML;
using PRIO.Files.XML._001;
using PRIO.Files.XML._002;
using PRIO.Files.XML._003;
using PRIO.Files.XML._039;
using PRIO.Filters;
using PRIO.Models.MeasurementModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.ViewModels.Files;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("measurements")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public partial class XMLController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DTOFiles _responseResult;

        [GeneratedRegex("(?<=data:@file/xml;base64,)\\w+")]
        private static partial Regex XmlRegex();
        public XMLController(IMapper mapper)
        {
            _mapper = mapper;
            _responseResult = new();
        }

        [HttpPost]
        public async Task<ActionResult> PostBase64Files([FromBody] RequestXmlViewModel data, [FromServices] DataContext context)
        {
            var user = HttpContext.Items["User"] as User;

            for (int i = 0; i < data.Files.Count; ++i)
            {
                #region validations
                var match = XmlRegex().Match(data.Files[i].ContentBase64);
                if (!match.Success)
                    return BadRequest(new ErrorResponseDTO
                    {
                        Message = $"One file has a non-XML extension. Failed to post file position in the list: {i} with file name: {data.Files[i].FileName}"

                    });

                var fileContent = data.Files[i].ContentBase64.Replace("data:@file/xml;base64,", "");

                var isValidFileName = new List<string>()
                    {
                        "039",
                        "001",
                        "002",
                        "003"
                    }.Contains(data.Files[i].FileName);

                if (!isValidFileName)
                    return BadRequest(new ErrorResponseDTO
                    {
                        Message = $"Invalid file name. Supported names are: 001, 002, 003, 039. Failed to post file position in the list: {i} with the file name: {data.Files[i].FileName}"
                    });
                #endregion

                #region pathing
                var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\.."));
                var relativeSchemaPath = Path.Combine("Files", "XML", $"_{data.Files[i].FileName}\\Schema.xsd");
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
                        return BadRequest(new { Message = result });
                }

                var documentXml = XDocument.Load(pathXml);
                #endregion

                #region generic elements and basic validation
                var rootElement = documentXml.Root;
                var dadosBasicosElements = rootElement?.Elements("LISTA_DADOS_BASICOS")?.Elements("DADOS_BASICOS");

                if (dadosBasicosElements is null)
                    return BadRequest(new ErrorResponseDTO
                    {
                        Message = "LISTA_DADOS_BASICOS XML element cant be null"
                    });
                #endregion

                for (int k = 0; k < dadosBasicosElements.Count(); ++k)
                {
                    var dadosBasicosElement = dadosBasicosElements.ElementAt(k);

                    switch (data.Files[i].FileName)
                    {
                        #region 039
                        case "039":
                            {
                                #region elementos XML
                                var dadosBasicos = dadosBasicosElement is not null ? Functions.DeserializeXml<DADOS_BASICOS_039>(dadosBasicosElement) : null;
                                #endregion

                                try
                                {
                                    var measurement = _mapper.Map<Measurement>(dadosBasicos);
                                    measurement.FileType = new FileType
                                    {
                                        Name = data.Files[i].FileName,
                                        Acronym = "EFM",

                                    };
                                    measurement.User = user;
                                    await context.Measurements.AddAsync(measurement);

                                    var measurement039DTO = _mapper.Map<Measurement, _039DTO>(measurement);
                                    _responseResult._039File ??= new List<_039DTO>();
                                    _responseResult._039File?.Add(measurement039DTO);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                    return BadRequest(new ErrorResponseDTO
                                    {
                                        Message = $"Something went wrong: {ex.Message}"
                                    });
                                }
                                break;
                            }
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


                                try
                                {
                                    var measurement = new Measurement
                                    {
                                        #region atributos dados basicos
                                        NUM_SERIE_ELEMENTO_PRIMARIO_001 = dadosBasicos?.NUM_SERIE_ELEMENTO_PRIMARIO_001,
                                        COD_INSTALACAO_001 = dadosBasicos?.COD_INSTALACAO_001,
                                        COD_TAG_PONTO_MEDICAO_001 = dadosBasicos?.COD_TAG_PONTO_MEDICAO_001,
                                        #endregion

                                        #region configuracao cv
                                        NUM_SERIE_COMPUTADOR_VAZAO_001 = configuracaoCv?.NUM_SERIE_COMPUTADOR_VAZAO_001,
                                        DHA_COLETA_001 = DateTime.TryParseExact(configuracaoCv?.DHA_COLETA_001, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dhaColeta) ? dhaColeta : null,
                                        MED_TEMPERATURA_001 = double.TryParse(configuracaoCv?.MED_TEMPERATURA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medTemperatura) ? medTemperatura : 0,
                                        MED_PRESSAO_ATMSA_001 = double.TryParse(configuracaoCv?.MED_PRESSAO_ATMSA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medPressaoAtmsa) ? medPressaoAtmsa : 0,
                                        MED_PRESSAO_RFRNA_001 = double.TryParse(configuracaoCv?.MED_PRESSAO_RFRNA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medPressaoRfrna) ? medPressaoRfrna : 0,
                                        DSC_VERSAO_SOFTWARE_001 = configuracaoCv?.DSC_VERSAO_SOFTWARE_001,
                                        #endregion

                                        #region elemento primario
                                        ICE_METER_FACTOR_1_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_1_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceMeterFactor1) ? iceMeterFactor1 : 0,
                                        ICE_METER_FACTOR_2_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_2_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_2_001) ? ICE_METER_FACTOR_2_001 : 0,
                                        ICE_METER_FACTOR_3_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_3_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_3_001) ? ICE_METER_FACTOR_3_001 : 0,
                                        ICE_METER_FACTOR_4_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_4_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_4_001) ? ICE_METER_FACTOR_4_001 : 0,
                                        ICE_METER_FACTOR_5_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_5_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_5_001) ? ICE_METER_FACTOR_5_001 : 0,
                                        ICE_METER_FACTOR_6_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_6_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_6_001) ? ICE_METER_FACTOR_6_001 : 0,
                                        ICE_METER_FACTOR_7_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_7_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_7_001) ? ICE_METER_FACTOR_7_001 : 0,
                                        ICE_METER_FACTOR_8_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_8_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_8_001) ? ICE_METER_FACTOR_8_001 : 0,
                                        ICE_METER_FACTOR_9_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_9_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_9_001) ? ICE_METER_FACTOR_9_001 : 0,
                                        ICE_METER_FACTOR_10_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_10_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_10_001) ? ICE_METER_FACTOR_10_001 : 0,
                                        ICE_METER_FACTOR_11_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_11_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_11_001) ? ICE_METER_FACTOR_11_001 : 0,
                                        ICE_METER_FACTOR_12_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_12_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_12_001) ? ICE_METER_FACTOR_12_001 : 0,
                                        ICE_METER_FACTOR_13_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_13_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_13_001) ? ICE_METER_FACTOR_13_001 : 0,
                                        ICE_METER_FACTOR_14_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_14_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_14_001) ? ICE_METER_FACTOR_14_001 : 0,
                                        ICE_METER_FACTOR_15_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_15_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_METER_FACTOR_15_001) ? ICE_METER_FACTOR_15_001 : 0,

                                        QTD_PULSOS_METER_FACTOR_1_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_1_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_1_001) ? QTD_PULSOS_METER_FACTOR_1_001 : 0,
                                        QTD_PULSOS_METER_FACTOR_2_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_2_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_2_001) ? QTD_PULSOS_METER_FACTOR_2_001 : 0,
                                        QTD_PULSOS_METER_FACTOR_3_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_3_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_3_001) ? QTD_PULSOS_METER_FACTOR_3_001 : 0,
                                        QTD_PULSOS_METER_FACTOR_4_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_4_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_4_001) ? QTD_PULSOS_METER_FACTOR_4_001 : 0,
                                        QTD_PULSOS_METER_FACTOR_5_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_5_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_5_001) ? QTD_PULSOS_METER_FACTOR_5_001 : 0,
                                        QTD_PULSOS_METER_FACTOR_6_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_6_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_6_001) ? QTD_PULSOS_METER_FACTOR_6_001 : 0,
                                        QTD_PULSOS_METER_FACTOR_7_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_7_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_7_001) ? QTD_PULSOS_METER_FACTOR_7_001 : 0,
                                        QTD_PULSOS_METER_FACTOR_8_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_8_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_8_001) ? QTD_PULSOS_METER_FACTOR_8_001 : 0,
                                        QTD_PULSOS_METER_FACTOR_9_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_9_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_9_001) ? QTD_PULSOS_METER_FACTOR_9_001 : 0,
                                        QTD_PULSOS_METER_FACTOR_10_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_10_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_10_001) ? QTD_PULSOS_METER_FACTOR_10_001 : 0,
                                        QTD_PULSOS_METER_FACTOR_11_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_11_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_11_001) ? QTD_PULSOS_METER_FACTOR_11_001 : 0,
                                        QTD_PULSOS_METER_FACTOR_12_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_12_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_12_001) ? QTD_PULSOS_METER_FACTOR_12_001 : 0,
                                        QTD_PULSOS_METER_FACTOR_13_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_13_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_13_001) ? QTD_PULSOS_METER_FACTOR_13_001 : 0,
                                        QTD_PULSOS_METER_FACTOR_14_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_14_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_14_001) ? QTD_PULSOS_METER_FACTOR_14_001 : 0,
                                        QTD_PULSOS_METER_FACTOR_15_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_15_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_METER_FACTOR_15_001) ? QTD_PULSOS_METER_FACTOR_15_001 : 0,

                                        ICE_K_FACTOR_1_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_1_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor1) ? iceKFactor1 : 0,
                                        ICE_K_FACTOR_2_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_2_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor2) ? iceKFactor2 : 0,
                                        ICE_K_FACTOR_3_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_3_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor3) ? iceKFactor3 : 0,
                                        ICE_K_FACTOR_4_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_4_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor4) ? iceKFactor4 : 0,
                                        ICE_K_FACTOR_5_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_5_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor5) ? iceKFactor5 : 0,
                                        ICE_K_FACTOR_6_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_6_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor6) ? iceKFactor6 : 0,
                                        ICE_K_FACTOR_7_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_7_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor7) ? iceKFactor7 : 0,
                                        ICE_K_FACTOR_8_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_8_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor8) ? iceKFactor8 : 0,
                                        ICE_K_FACTOR_9_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_9_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor9) ? iceKFactor9 : 0,
                                        ICE_K_FACTOR_10_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_10_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor10) ? iceKFactor10 : 0,
                                        ICE_K_FACTOR_11_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_11_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor11) ? iceKFactor11 : 0,
                                        ICE_K_FACTOR_12_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_12_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceKFactor12) ? iceKFactor12 : 0,
                                        ICE_K_FACTOR_13_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_13_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_K_FACTOR_13_001) ? ICE_K_FACTOR_13_001 : 0,
                                        ICE_K_FACTOR_14_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_14_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_K_FACTOR_14_001) ? ICE_K_FACTOR_14_001 : 0,
                                        ICE_K_FACTOR_15_001 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_15_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_K_FACTOR_15_001) ? ICE_K_FACTOR_15_001 : 0,

                                        QTD_PULSOS_K_FACTOR_1_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_1_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor1) ? qtdPulsosKFactor1 : 0,
                                        QTD_PULSOS_K_FACTOR_2_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_2_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor2) ? qtdPulsosKFactor2 : 0,
                                        QTD_PULSOS_K_FACTOR_3_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_3_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor3) ? qtdPulsosKFactor3 : 0,
                                        QTD_PULSOS_K_FACTOR_4_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_4_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor4) ? qtdPulsosKFactor4 : 0,
                                        QTD_PULSOS_K_FACTOR_5_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_5_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor5) ? qtdPulsosKFactor5 : 0,
                                        QTD_PULSOS_K_FACTOR_6_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_6_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor6) ? qtdPulsosKFactor6 : 0,
                                        QTD_PULSOS_K_FACTOR_7_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_7_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor7) ? qtdPulsosKFactor7 : 0,
                                        QTD_PULSOS_K_FACTOR_8_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_8_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor8) ? qtdPulsosKFactor8 : 0,
                                        QTD_PULSOS_K_FACTOR_9_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_9_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor9) ? qtdPulsosKFactor9 : 0,
                                        QTD_PULSOS_K_FACTOR_10_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_10_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor10) ? qtdPulsosKFactor10 : 0,
                                        QTD_PULSOS_K_FACTOR_11_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_11_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor11) ? qtdPulsosKFactor11 : 0,
                                        QTD_PULSOS_K_FACTOR_12_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_12_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_K_FACTOR_12_001) ? QTD_PULSOS_K_FACTOR_12_001 : 0,
                                        QTD_PULSOS_K_FACTOR_13_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_13_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_K_FACTOR_13_001) ? QTD_PULSOS_K_FACTOR_13_001 : 0,
                                        QTD_PULSOS_K_FACTOR_14_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_14_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_K_FACTOR_14_001) ? QTD_PULSOS_K_FACTOR_14_001 : 0,
                                        QTD_PULSOS_K_FACTOR_15_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_15_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var QTD_PULSOS_K_FACTOR_15_001) ? QTD_PULSOS_K_FACTOR_15_001 : 0,

                                        ICE_CUTOFF_001 = double.TryParse(elementoPrimario?.ICE_CUTOFF_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceCutoff) ? iceCutoff : 0,
                                        ICE_LIMITE_SPRR_ALARME_001 = double.TryParse(elementoPrimario?.ICE_LIMITE_SPRR_ALARME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceLimiteSprrAlarme) ? iceLimiteSprrAlarme : 0,
                                        ICE_LIMITE_INFRR_ALARME_001 = double.TryParse(elementoPrimario?.ICE_LIMITE_INFRR_ALARME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceLimiteInfrrAlarme) ? iceLimiteInfrrAlarme : 0,
                                        IND_HABILITACAO_ALARME_1_001 = elementoPrimario?.IND_HABILITACAO_ALARME_1_001,
                                        #endregion

                                        #region instrumento pressao
                                        NUM_SERIE_1_001 = instrumentoPressao?.NUM_SERIE_1_001,
                                        MED_PRSO_LIMITE_SPRR_ALRME_001 = double.TryParse(instrumentoPressao?.MED_PRSO_LIMITE_SPRR_ALRME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var limiteSprr) ? limiteSprr : 0,
                                        MED_PRSO_LMTE_INFRR_ALRME_001 = double.TryParse(instrumentoPressao?.MED_PRSO_LMTE_INFRR_ALRME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var limiteInfrr) ? limiteInfrr : 0,
                                        IND_HABILITACAO_ALARME_2_001 = instrumentoPressao?.IND_HABILITACAO_ALARME_2_001,
                                        MED_PRSO_ADOTADA_FALHA_001 = double.TryParse(instrumentoPressao?.MED_PRSO_ADOTADA_FALHA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var adotFalha) ? adotFalha : 0,
                                        DSC_ESTADO_INSNO_CASO_FALHA_001 = instrumentoPressao?.DSC_ESTADO_INSNO_CASO_FALHA_001,
                                        IND_TIPO_PRESSAO_CONSIDERADA_001 = instrumentoPressao?.IND_TIPO_PRESSAO_CONSIDERADA_001,
                                        #endregion

                                        #region instrumento temperatura

                                        NUM_SERIE_2_001 = instrumentoTemperatura?.NUM_SERIE_2_001,
                                        MED_TMPTA_SPRR_ALARME_001 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_SPRR_ALARME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var tmptaAl) ? tmptaAl : 0,
                                        MED_TMPTA_INFRR_ALRME_001 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_INFRR_ALRME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var infrrAl) ? infrrAl : 0,
                                        IND_HABILITACAO_ALARME_3_001 = instrumentoTemperatura?.IND_HABILITACAO_ALARME_3_001,
                                        MED_TMPTA_ADTTA_FALHA_001 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_ADTTA_FALHA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var adtta) ? adtta : 0,
                                        DSC_ESTADO_INSTRUMENTO_FALHA_1_001 = instrumentoTemperatura?.DSC_ESTADO_INSTRUMENTO_FALHA_1_001,

                                        #endregion

                                        #region producao

                                        DHA_INICIO_PERIODO_MEDICAO_001 = DateTime.TryParseExact(producao?.DHA_INICIO_PERIODO_MEDICAO_001, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var inicioPer) ? inicioPer : null,
                                        DHA_FIM_PERIODO_MEDICAO_001 = DateTime.TryParseExact(producao?.DHA_FIM_PERIODO_MEDICAO_001, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fimPer) ? fimPer : null,
                                        ICE_DENSIDADADE_RELATIVA_001 = double.TryParse(producao?.ICE_DENSIDADADE_RELATIVA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_DENSIDADADE_RELATIVA_001) ? ICE_DENSIDADADE_RELATIVA_001 : 0,
                                        ICE_CORRECAO_BSW_001 = double.TryParse(producao?.ICE_CORRECAO_BSW_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var ICE_CORRECAO_BSW_001) ? ICE_CORRECAO_BSW_001 : 0,
                                        ICE_CORRECAO_PRESSAO_LIQUIDO_001 = double.TryParse(producao?.ICE_CORRECAO_PRESSAO_LIQUIDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var pressaoLiq) ? pressaoLiq : 0,
                                        ICE_CRRCO_TEMPERATURA_LIQUIDO_001 = double.TryParse(producao?.ICE_CRRCO_TEMPERATURA_LIQUIDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var crrcoTemp) ? crrcoTemp : 0,
                                        MED_PRESSAO_ESTATICA_001 = double.TryParse(producao?.MED_PRESSAO_ESTATICA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var MED_PRESSAO_ESTATICA_001) ? MED_PRESSAO_ESTATICA_001 : 0,
                                        MED_TMPTA_FLUIDO_001 = double.TryParse(producao?.MED_TMPTA_FLUIDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var MED_TMPTA_FLUIDO_001) ? MED_TMPTA_FLUIDO_001 : 0,
                                        MED_VOLUME_BRTO_CRRGO_MVMDO_001 = double.TryParse(producao?.MED_VOLUME_BRTO_CRRGO_MVMDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var MED_VOLUME_BRTO_CRRGO_MVMDO_001) ? MED_VOLUME_BRTO_CRRGO_MVMDO_001 : 0,
                                        MED_VOLUME_BRUTO_MVMDO_001 = double.TryParse(producao?.MED_VOLUME_BRUTO_MVMDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var MED_VOLUME_BRUTO_MVMDO_001) ? MED_VOLUME_BRUTO_MVMDO_001 : 0,
                                        MED_VOLUME_LIQUIDO_MVMDO_001 = double.TryParse(producao?.MED_VOLUME_LIQUIDO_MVMDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var MED_VOLUME_LIQUIDO_MVMDO_001) ? MED_VOLUME_LIQUIDO_MVMDO_001 : 0,
                                        MED_VOLUME_TTLZO_FIM_PRDO_001 = double.TryParse(producao?.MED_VOLUME_TTLZO_FIM_PRDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var MED_VOLUME_TTLZO_FIM_PRDO_001) ? MED_VOLUME_TTLZO_FIM_PRDO_001 : 0,
                                        MED_VOLUME_TTLZO_INCO_PRDO_001 = double.TryParse(producao?.MED_VOLUME_TTLZO_INCO_PRDO_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var MED_VOLUME_TTLZO_INCO_PRDO_001) ? MED_VOLUME_TTLZO_INCO_PRDO_001 : 0,

                                        #endregion

                                        #region FileType relation
                                        FileType = new FileType
                                        {
                                            Name = data.Files[i].FileName,
                                            Acronym = "PMO",

                                        },
                                        #endregion

                                        #region User relation
                                        User = user,

                                        #endregion

                                    };
                                    await context.Measurements.AddAsync(measurement);

                                    var measurement001DTO = _mapper.Map<Measurement, _001DTO>(measurement);
                                    _responseResult._001File ??= new List<_001DTO>();
                                    _responseResult._001File?.Add(measurement001DTO);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                    return BadRequest(new ErrorResponseDTO
                                    {
                                        Message = $"Something went wrong: {ex.Message}"
                                    });
                                }

                            }
                            break;
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

                                try
                                {

                                    var measurement = new Measurement()
                                    {
                                        #region atributos dados basicos
                                        NUM_SERIE_ELEMENTO_PRIMARIO_002 = dadosBasicos?.NUM_SERIE_ELEMENTO_PRIMARIO_002,
                                        COD_INSTALACAO_002 = dadosBasicos?.COD_INSTALACAO_002,
                                        COD_TAG_PONTO_MEDICAO_002 = dadosBasicos?.COD_TAG_PONTO_MEDICAO_002,
                                        #endregion

                                        #region configuracao cv
                                        NUM_SERIE_COMPUTADOR_VAZAO_002 = configuracaoCv?.NUM_SERIE_COMPUTADOR_VAZAO_002,
                                        DHA_COLETA_002 = DateTime.TryParseExact(configuracaoCv?.DHA_COLETA_002, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var DHA_COLETA_002) ? DHA_COLETA_002 : null,
                                        MED_TEMPERATURA_1_002 = double.TryParse(configuracaoCv?.MED_TEMPERATURA_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medTemp) ? medTemp : 0,
                                        MED_PRESSAO_ATMSA_002 = double.TryParse(configuracaoCv?.MED_PRESSAO_ATMSA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medPressAt) ? medPressAt : 0,
                                        MED_PRESSAO_RFRNA_002 = double.TryParse(configuracaoCv?.MED_PRESSAO_RFRNA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medPressRf) ? medPressRf : 0,
                                        MED_DENSIDADE_RELATIVA_002 = double.TryParse(configuracaoCv?.MED_DENSIDADE_RELATIVA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medDens) ? medDens : 0,
                                        DSC_NORMA_UTILIZADA_CALCULO_002 = configuracaoCv?.DSC_NORMA_UTILIZADA_CALCULO_002,
                                        PCT_CROMATOGRAFIA_NITROGENIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_NITROGENIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double pctCromaNit) ? pctCromaNit : 0,

                                        PCT_CROMATOGRAFIA_CO2_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_CO2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double pctCromaCO2) ? pctCromaCO2 : 0,
                                        PCT_CROMATOGRAFIA_METANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_METANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_METANO_002) ? PCT_CROMATOGRAFIA_METANO_002 : 0,
                                        PCT_CROMATOGRAFIA_ETANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_ETANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_ETANO_002) ? PCT_CROMATOGRAFIA_ETANO_002 : 0,
                                        PCT_CROMATOGRAFIA_PROPANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_PROPANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_PROPANO_002) ? PCT_CROMATOGRAFIA_PROPANO_002 : 0,
                                        PCT_CROMATOGRAFIA_N_BUTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_N_BUTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_N_BUTANO_002) ? PCT_CROMATOGRAFIA_N_BUTANO_002 : 0,
                                        PCT_CROMATOGRAFIA_I_BUTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_I_BUTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_I_BUTANO_002) ? PCT_CROMATOGRAFIA_I_BUTANO_002 : 0,
                                        PCT_CROMATOGRAFIA_N_PENTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_N_PENTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_N_PENTANO_002) ? PCT_CROMATOGRAFIA_N_PENTANO_002 : 0,
                                        PCT_CROMATOGRAFIA_I_PENTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_I_PENTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_I_PENTANO_002) ? PCT_CROMATOGRAFIA_I_PENTANO_002 : 0,
                                        PCT_CROMATOGRAFIA_HEXANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HEXANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HEXANO_002) ? PCT_CROMATOGRAFIA_HEXANO_002 : 0,
                                        PCT_CROMATOGRAFIA_HEPTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HEPTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HEPTANO_002) ? PCT_CROMATOGRAFIA_HEPTANO_002 : 0,
                                        PCT_CROMATOGRAFIA_OCTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_OCTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_OCTANO_002) ? PCT_CROMATOGRAFIA_OCTANO_002 : 0,
                                        PCT_CROMATOGRAFIA_NONANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_NONANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_NONANO_002) ? PCT_CROMATOGRAFIA_NONANO_002 : 0,
                                        PCT_CROMATOGRAFIA_DECANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_DECANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_DECANO_002) ? PCT_CROMATOGRAFIA_DECANO_002 : 0,
                                        PCT_CROMATOGRAFIA_H2S_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_H2S_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_H2S_002) ? PCT_CROMATOGRAFIA_H2S_002 : 0,
                                        PCT_CROMATOGRAFIA_AGUA_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_AGUA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_AGUA_002) ? PCT_CROMATOGRAFIA_AGUA_002 : 0,
                                        PCT_CROMATOGRAFIA_HELIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HELIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HELIO_002) ? PCT_CROMATOGRAFIA_HELIO_002 : 0,
                                        PCT_CROMATOGRAFIA_OXIGENIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_OXIGENIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_OXIGENIO_002) ? PCT_CROMATOGRAFIA_OXIGENIO_002 : 0,
                                        PCT_CROMATOGRAFIA_CO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_CO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_CO_002) ? PCT_CROMATOGRAFIA_CO_002 : 0,
                                        PCT_CROMATOGRAFIA_HIDROGENIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HIDROGENIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HIDROGENIO_002) ? PCT_CROMATOGRAFIA_HIDROGENIO_002 : 0,
                                        PCT_CROMATOGRAFIA_ARGONIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_ARGONIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_ARGONIO_002) ? PCT_CROMATOGRAFIA_ARGONIO_002 : 0,

                                        DSC_VERSAO_SOFTWARE_002 = configuracaoCv?.DSC_VERSAO_SOFTWARE_002,

                                        #endregion

                                        #region elemento primario
                                        ICE_METER_FACTOR_1_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_1_002) ? ICE_METER_FACTOR_1_002 : 0,
                                        ICE_METER_FACTOR_2_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_2_002) ? ICE_METER_FACTOR_2_002 : 0,
                                        ICE_METER_FACTOR_3_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_3_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_3_002) ? ICE_METER_FACTOR_3_002 : 0,
                                        ICE_METER_FACTOR_4_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_4_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_4_002) ? ICE_METER_FACTOR_4_002 : 0,
                                        ICE_METER_FACTOR_5_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_5_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_5_002) ? ICE_METER_FACTOR_5_002 : 0,
                                        ICE_METER_FACTOR_6_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_6_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_6_002) ? ICE_METER_FACTOR_6_002 : 0,
                                        ICE_METER_FACTOR_7_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_7_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_7_002) ? ICE_METER_FACTOR_7_002 : 0,
                                        ICE_METER_FACTOR_8_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_8_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_8_002) ? ICE_METER_FACTOR_8_002 : 0,
                                        ICE_METER_FACTOR_9_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_9_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_9_002) ? ICE_METER_FACTOR_9_002 : 0,
                                        ICE_METER_FACTOR_10_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_10_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_10_002) ? ICE_METER_FACTOR_10_002 : 0,
                                        ICE_METER_FACTOR_11_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_11_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_11_002) ? ICE_METER_FACTOR_11_002 : 0,
                                        ICE_METER_FACTOR_12_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_12_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_12_002) ? ICE_METER_FACTOR_12_002 : 0,
                                        ICE_METER_FACTOR_13_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_13_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_13_002) ? ICE_METER_FACTOR_13_002 : 0,
                                        ICE_METER_FACTOR_14_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_14_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_14_002) ? ICE_METER_FACTOR_14_002 : 0,
                                        ICE_METER_FACTOR_15_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_15_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_15_002) ? ICE_METER_FACTOR_15_002 : 0,

                                        QTD_PULSOS_METER_FACTOR_1_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_1_002) ? QTD_PULSOS_METER_FACTOR_1_002 : 0,
                                        QTD_PULSOS_METER_FACTOR_2_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_2_002) ? QTD_PULSOS_METER_FACTOR_2_002 : 0,
                                        QTD_PULSOS_METER_FACTOR_3_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_3_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_3_002) ? QTD_PULSOS_METER_FACTOR_3_002 : 0,
                                        QTD_PULSOS_METER_FACTOR_4_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_4_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_4_002) ? QTD_PULSOS_METER_FACTOR_4_002 : 0,
                                        QTD_PULSOS_METER_FACTOR_5_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_5_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_5_002) ? QTD_PULSOS_METER_FACTOR_5_002 : 0,
                                        QTD_PULSOS_METER_FACTOR_6_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_6_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_6_002) ? QTD_PULSOS_METER_FACTOR_6_002 : 0,
                                        QTD_PULSOS_METER_FACTOR_7_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_7_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_7_002) ? QTD_PULSOS_METER_FACTOR_7_002 : 0,
                                        QTD_PULSOS_METER_FACTOR_8_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_8_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_8_002) ? QTD_PULSOS_METER_FACTOR_8_002 : 0,
                                        QTD_PULSOS_METER_FACTOR_9_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_9_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_9_002) ? QTD_PULSOS_METER_FACTOR_9_002 : 0,
                                        QTD_PULSOS_METER_FACTOR_10_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_10_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_10_002) ? QTD_PULSOS_METER_FACTOR_10_002 : 0,
                                        QTD_PULSOS_METER_FACTOR_11_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_11_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_11_002) ? QTD_PULSOS_METER_FACTOR_11_002 : 0,
                                        QTD_PULSOS_METER_FACTOR_12_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_12_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_12_002) ? QTD_PULSOS_METER_FACTOR_12_002 : 0,
                                        QTD_PULSOS_METER_FACTOR_13_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_13_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_13_002) ? QTD_PULSOS_METER_FACTOR_13_002 : 0,
                                        QTD_PULSOS_METER_FACTOR_14_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_14_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_14_002) ? QTD_PULSOS_METER_FACTOR_14_002 : 0,
                                        QTD_PULSOS_METER_FACTOR_15_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_15_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_15_002) ? QTD_PULSOS_METER_FACTOR_15_002 : 0,

                                        ICE_K_FACTOR_1_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_1_002) ? ICE_K_FACTOR_1_002 : 0,
                                        ICE_K_FACTOR_2_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_2_002) ? ICE_K_FACTOR_2_002 : 0,
                                        ICE_K_FACTOR_3_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_3_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_3_002) ? ICE_K_FACTOR_3_002 : 0,
                                        ICE_K_FACTOR_4_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_4_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_4_002) ? ICE_K_FACTOR_4_002 : 0,
                                        ICE_K_FACTOR_5_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_5_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_5_002) ? ICE_K_FACTOR_5_002 : 0,
                                        ICE_K_FACTOR_6_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_6_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_6_002) ? ICE_K_FACTOR_6_002 : 0,
                                        ICE_K_FACTOR_7_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_7_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_7_002) ? ICE_K_FACTOR_7_002 : 0,
                                        ICE_K_FACTOR_8_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_8_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_8_002) ? ICE_K_FACTOR_8_002 : 0,
                                        ICE_K_FACTOR_9_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_9_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_9_002) ? ICE_K_FACTOR_9_002 : 0,
                                        ICE_K_FACTOR_10_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_10_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_10_002) ? ICE_K_FACTOR_10_002 : 0,
                                        ICE_K_FACTOR_11_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_11_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_11_002) ? ICE_K_FACTOR_11_002 : 0,
                                        ICE_K_FACTOR_12_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_12_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_12_002) ? ICE_K_FACTOR_12_002 : 0,
                                        ICE_K_FACTOR_13_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_13_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_13_002) ? ICE_K_FACTOR_13_002 : 0,
                                        ICE_K_FACTOR_14_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_14_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_14_002) ? ICE_K_FACTOR_14_002 : 0,
                                        ICE_K_FACTOR_15_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_15_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_15_002) ? ICE_K_FACTOR_15_002 : 0,

                                        QTD_PULSOS_K_FACTOR_1_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_1_002) ? QTD_PULSOS_K_FACTOR_1_002 : 0,
                                        QTD_PULSOS_K_FACTOR_2_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_2_002) ? QTD_PULSOS_K_FACTOR_2_002 : 0,
                                        QTD_PULSOS_K_FACTOR_3_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_3_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_3_002) ? QTD_PULSOS_K_FACTOR_3_002 : 0,
                                        QTD_PULSOS_K_FACTOR_4_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_4_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_4_002) ? QTD_PULSOS_K_FACTOR_4_002 : 0,
                                        QTD_PULSOS_K_FACTOR_5_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_5_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_5_002) ? QTD_PULSOS_K_FACTOR_5_002 : 0,
                                        QTD_PULSOS_K_FACTOR_6_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_6_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_6_002) ? QTD_PULSOS_K_FACTOR_6_002 : 0,
                                        QTD_PULSOS_K_FACTOR_7_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_7_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_7_002) ? QTD_PULSOS_K_FACTOR_7_002 : 0,
                                        QTD_PULSOS_K_FACTOR_8_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_8_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_8_002) ? QTD_PULSOS_K_FACTOR_8_002 : 0,
                                        QTD_PULSOS_K_FACTOR_9_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_9_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_9_002) ? QTD_PULSOS_K_FACTOR_9_002 : 0,
                                        QTD_PULSOS_K_FACTOR_10_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_10_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_10_002) ? QTD_PULSOS_K_FACTOR_10_002 : 0,
                                        QTD_PULSOS_K_FACTOR_11_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_11_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_11_002) ? QTD_PULSOS_K_FACTOR_11_002 : 0,
                                        QTD_PULSOS_K_FACTOR_12_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_12_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_12_002) ? QTD_PULSOS_K_FACTOR_12_002 : 0,
                                        QTD_PULSOS_K_FACTOR_13_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_13_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_13_002) ? QTD_PULSOS_K_FACTOR_13_002 : 0,
                                        QTD_PULSOS_K_FACTOR_14_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_14_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_14_002) ? QTD_PULSOS_K_FACTOR_14_002 : 0,
                                        QTD_PULSOS_K_FACTOR_15_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_15_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_15_002) ? QTD_PULSOS_K_FACTOR_15_002 : 0,

                                        ICE_CUTOFF_002 = double.TryParse(elementoPrimario?.ICE_CUTOFF_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_CUTOFF_002) ? ICE_CUTOFF_002 : 0,
                                        ICE_LIMITE_SPRR_ALARME_002 = double.TryParse(elementoPrimario?.ICE_LIMITE_SPRR_ALARME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_LIMITE_SPRR_ALARME_002) ? ICE_LIMITE_SPRR_ALARME_002 : 0,
                                        ICE_LIMITE_INFRR_ALARME_002 = double.TryParse(elementoPrimario?.ICE_LIMITE_INFRR_ALARME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_LIMITE_INFRR_ALARME_002) ? ICE_LIMITE_INFRR_ALARME_002 : 0,
                                        IND_HABILITACAO_ALARME_1_002 = elementoPrimario?.IND_HABILITACAO_ALARME_1_002,
                                        #endregion

                                        #region instrumento pressao

                                        NUM_SERIE_1_002 = instrumentoPressao?.NUM_SERIE_1_002,
                                        MED_PRSO_LIMITE_SPRR_ALRME_002 = double.TryParse(instrumentoPressao?.MED_PRSO_LIMITE_SPRR_ALRME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LIMITE_SPRR_ALRME_002) ? MED_PRSO_LIMITE_SPRR_ALRME_002 : 0,
                                        MED_PRSO_LMTE_INFRR_ALRME_002 = double.TryParse(instrumentoPressao?.MED_PRSO_LMTE_INFRR_ALRME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LMTE_INFRR_ALRME_002) ? MED_PRSO_LMTE_INFRR_ALRME_002 : 0,
                                        IND_HABILITACAO_ALARME_2_002 = instrumentoPressao?.IND_HABILITACAO_ALARME_2_002,
                                        MED_PRSO_ADOTADA_FALHA_002 = double.TryParse(instrumentoPressao?.MED_PRSO_ADOTADA_FALHA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_ADOTADA_FALHA_002) ? MED_PRSO_ADOTADA_FALHA_002 : 0,
                                        DSC_ESTADO_INSNO_CASO_FALHA_002 = instrumentoPressao?.DSC_ESTADO_INSNO_CASO_FALHA_002,
                                        IND_TIPO_PRESSAO_CONSIDERADA_002 = instrumentoPressao?.IND_TIPO_PRESSAO_CONSIDERADA_002,

                                        #endregion

                                        #region instrumento temperatura
                                        NUM_SERIE_2_002 = instrumentoTemperatura?.NUM_SERIE_2_002,
                                        MED_TMPTA_SPRR_ALARME_002 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_SPRR_ALARME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TMPTA_SPRR_ALARME_002) ? MED_TMPTA_SPRR_ALARME_002 : 0,
                                        MED_TMPTA_INFRR_ALRME_002 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_INFRR_ALRME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TMPTA_INFRR_ALRME_002) ? MED_TMPTA_INFRR_ALRME_002 : 0,
                                        IND_HABILITACAO_ALARME_3_002 = instrumentoTemperatura?.IND_HABILITACAO_ALARME_3_002,
                                        MED_TMPTA_ADTTA_FALHA_002 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_ADTTA_FALHA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TMPTA_ADTTA_FALHA_002) ? MED_TMPTA_ADTTA_FALHA_002 : 0,
                                        DSC_ESTADO_INSTRUMENTO_FALHA_002 = instrumentoTemperatura?.DSC_ESTADO_INSTRUMENTO_FALHA_002,

                                        #endregion

                                        #region producao
                                        DHA_INICIO_PERIODO_MEDICAO_002 = DateTime.TryParseExact(producao?.DHA_INICIO_PERIODO_MEDICAO_002, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var DHA_INICIO_PERIODO_MEDICAO_002) ? DHA_INICIO_PERIODO_MEDICAO_002 : null,
                                        DHA_FIM_PERIODO_MEDICAO_002 = DateTime.TryParseExact(producao?.DHA_FIM_PERIODO_MEDICAO_002, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var DHA_FIM_PERIODO_MEDICAO_002) ? DHA_FIM_PERIODO_MEDICAO_002 : null,
                                        ICE_DENSIDADE_RELATIVA_002 = double.TryParse(producao?.ICE_DENSIDADE_RELATIVA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_DENSIDADE_RELATIVA_002) ? ICE_DENSIDADE_RELATIVA_002 : 0,
                                        MED_PRESSAO_ESTATICA_002 = double.TryParse(producao?.MED_PRESSAO_ESTATICA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRESSAO_ESTATICA_002) ? MED_PRESSAO_ESTATICA_002 : 0,
                                        MED_TEMPERATURA_2_002 = double.TryParse(producao?.MED_TEMPERATURA_2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TEMPERATURA_2_002) ? MED_TEMPERATURA_2_002 : 0,
                                        PRZ_DURACAO_FLUXO_EFETIVO_002 = double.TryParse(producao?.PRZ_DURACAO_FLUXO_EFETIVO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PRZ_DURACAO_FLUXO_EFETIVO_002) ? PRZ_DURACAO_FLUXO_EFETIVO_002 : 0,
                                        MED_BRUTO_MOVIMENTADO_002 = double.TryParse(producao?.MED_BRUTO_MOVIMENTADO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_BRUTO_MOVIMENTADO_002) ? MED_BRUTO_MOVIMENTADO_002 : 0,
                                        MED_CORRIGIDO_MVMDO_002 = double.TryParse(producao?.MED_CORRIGIDO_MVMDO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_CORRIGIDO_MVMDO_002) ? MED_CORRIGIDO_MVMDO_002 : 0,
                                        #endregion

                                        #region FileType relation
                                        FileType = new FileType
                                        {
                                            Name = data.Files[i].FileName,
                                            Acronym = "PMGL",

                                        },
                                        #endregion

                                        #region User relation
                                        User = user,

                                        #endregion


                                    };
                                    await context.AddAsync(measurement);
                                    var measurement002DTO = _mapper.Map<Measurement, _002DTO>(measurement);
                                    _responseResult._002File ??= new List<_002DTO>();
                                    _responseResult._002File?.Add(measurement002DTO);

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                    return BadRequest(new ErrorResponseDTO
                                    {
                                        Message = $"Something went wrong: {ex.Message}"
                                    });
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

                                try
                                {
                                    var measurement = new Measurement
                                    {
                                        #region atributos
                                        NUM_SERIE_ELEMENTO_PRIMARIO_003 = dadosBasicos?.NUM_SERIE_ELEMENTO_PRIMARIO_003,
                                        COD_INSTALACAO_003 = dadosBasicos?.COD_INSTALACAO_003,
                                        COD_TAG_PONTO_MEDICAO_003 = dadosBasicos?.COD_TAG_PONTO_MEDICAO_003,
                                        #endregion

                                        #region configuracao cv

                                        NUM_SERIE_COMPUTADOR_VAZAO_003 = configuracaoCv?.NUM_SERIE_COMPUTADOR_VAZAO_003,
                                        DHA_COLETA_003 = DateTime.TryParseExact(configuracaoCv?.DHA_COLETA_003, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var DHA_COLETA_003) ? DHA_COLETA_003 : null,
                                        MED_TEMPERATURA_1_003 = double.TryParse(configuracaoCv?.MED_TEMPERATURA_1_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TEMPERATURA_1_003) ? MED_TEMPERATURA_1_003 : 0,
                                        MED_PRESSAO_ATMSA_003 = double.TryParse(configuracaoCv?.MED_PRESSAO_ATMSA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRESSAO_ATMSA_003) ? MED_PRESSAO_ATMSA_003 : 0,
                                        MED_PRESSAO_RFRNA_003 = double.TryParse(configuracaoCv?.MED_PRESSAO_RFRNA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRESSAO_RFRNA_003) ? MED_PRESSAO_RFRNA_003 : 0,
                                        MED_DENSIDADE_RELATIVA_003 = double.TryParse(configuracaoCv?.MED_DENSIDADE_RELATIVA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_DENSIDADE_RELATIVA_003) ? MED_DENSIDADE_RELATIVA_003 : 0,
                                        DSC_NORMA_UTILIZADA_CALCULO_003 = configuracaoCv?.DSC_NORMA_UTILIZADA_CALCULO_003,

                                        PCT_CROMATOGRAFIA_NITROGENIO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_NITROGENIO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_NITROGENIO_003) ? PCT_CROMATOGRAFIA_NITROGENIO_003 : 0,
                                        PCT_CROMATOGRAFIA_CO2_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_CO2_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_CO2_003) ? PCT_CROMATOGRAFIA_CO2_003 : 0,
                                        PCT_CROMATOGRAFIA_METANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_METANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_METANO_003) ? PCT_CROMATOGRAFIA_METANO_003 : 0,
                                        PCT_CROMATOGRAFIA_ETANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_ETANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_ETANO_003) ? PCT_CROMATOGRAFIA_ETANO_003 : 0,
                                        PCT_CROMATOGRAFIA_PROPANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_PROPANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_PROPANO_003) ? PCT_CROMATOGRAFIA_PROPANO_003 : 0,
                                        PCT_CROMATOGRAFIA_N_BUTANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_N_BUTANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_N_BUTANO_003) ? PCT_CROMATOGRAFIA_N_BUTANO_003 : 0,
                                        PCT_CROMATOGRAFIA_I_BUTANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_I_BUTANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_I_BUTANO_003) ? PCT_CROMATOGRAFIA_I_BUTANO_003 : 0,
                                        PCT_CROMATOGRAFIA_N_PENTANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_N_PENTANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_N_PENTANO_003) ? PCT_CROMATOGRAFIA_N_PENTANO_003 : 0,
                                        PCT_CROMATOGRAFIA_I_PENTANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_I_PENTANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_I_PENTANO_003) ? PCT_CROMATOGRAFIA_I_PENTANO_003 : 0,
                                        PCT_CROMATOGRAFIA_HEXANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HEXANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HEXANO_003) ? PCT_CROMATOGRAFIA_HEXANO_003 : 0,
                                        PCT_CROMATOGRAFIA_HEPTANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HEPTANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HEPTANO_003) ? PCT_CROMATOGRAFIA_HEPTANO_003 : 0,
                                        PCT_CROMATOGRAFIA_OCTANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_OCTANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_OCTANO_003) ? PCT_CROMATOGRAFIA_OCTANO_003 : 0,
                                        PCT_CROMATOGRAFIA_NONANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_NONANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_NONANO_003) ? PCT_CROMATOGRAFIA_NONANO_003 : 0,
                                        PCT_CROMATOGRAFIA_DECANO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_DECANO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_DECANO_003) ? PCT_CROMATOGRAFIA_DECANO_003 : 0,
                                        PCT_CROMATOGRAFIA_H2S_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_H2S_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_H2S_003) ? PCT_CROMATOGRAFIA_H2S_003 : 0,
                                        PCT_CROMATOGRAFIA_AGUA_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_AGUA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_AGUA_003) ? PCT_CROMATOGRAFIA_AGUA_003 : 0,
                                        PCT_CROMATOGRAFIA_HELIO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HELIO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HELIO_003) ? PCT_CROMATOGRAFIA_HELIO_003 : 0,
                                        PCT_CROMATOGRAFIA_OXIGENIO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_OXIGENIO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_OXIGENIO_003) ? PCT_CROMATOGRAFIA_OXIGENIO_003 : 0,
                                        PCT_CROMATOGRAFIA_CO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_CO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_CO_003) ? PCT_CROMATOGRAFIA_CO_003 : 0,
                                        PCT_CROMATOGRAFIA_HIDROGENIO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HIDROGENIO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HIDROGENIO_003) ? PCT_CROMATOGRAFIA_HIDROGENIO_003 : 0,
                                        PCT_CROMATOGRAFIA_ARGONIO_003 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_ARGONIO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_ARGONIO_003) ? PCT_CROMATOGRAFIA_ARGONIO_003 : 0,
                                        DSC_VERSAO_SOFTWARE_003 = configuracaoCv?.DSC_VERSAO_SOFTWARE_003,

                                        #endregion

                                        #region elemento primario
                                        CE_LIMITE_SPRR_ALARME_003 = double.TryParse(elementoPrimario?.CE_LIMITE_SPRR_ALARME_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double CE_LIMITE_SPRR_ALARME_003) ? CE_LIMITE_SPRR_ALARME_003 : 0,
                                        ICE_LIMITE_INFRR_ALARME_1_003 = double.TryParse(elementoPrimario?.ICE_LIMITE_INFRR_ALARME_1_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_LIMITE_INFRR_ALARME_1_003) ? ICE_LIMITE_INFRR_ALARME_1_003 : 0,
                                        IND_HABILITACAO_ALARME_1_003 = elementoPrimario?.IND_HABILITACAO_ALARME_1_003,

                                        #endregion

                                        #region instrumento pressao
                                        NUM_SERIE_1_003 = instrumentoPressao?.NUM_SERIE_1_003,
                                        MED_PRSO_LIMITE_SPRR_ALRME_1_003 = double.TryParse(instrumentoPressao?.MED_PRSO_LIMITE_SPRR_ALRME_1_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LIMITE_SPRR_ALRME_1_003) ? MED_PRSO_LIMITE_SPRR_ALRME_1_003 : 0,
                                        MED_PRSO_LMTE_INFRR_ALRME_1_003 = double.TryParse(instrumentoPressao?.MED_PRSO_LMTE_INFRR_ALRME_1_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LMTE_INFRR_ALRME_1_003) ? MED_PRSO_LMTE_INFRR_ALRME_1_003 : 0,
                                        MED_PRSO_ADOTADA_FALHA_1_003 = double.TryParse(instrumentoPressao?.MED_PRSO_ADOTADA_FALHA_1_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_ADOTADA_FALHA_1_003) ? MED_PRSO_ADOTADA_FALHA_1_003 : 0,
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
                                        MED_PRSO_LIMITE_SPRR_ALRME_2_003 = double.TryParse(instDiferenPressaoAlta?.MED_PRSO_LIMITE_SPRR_ALRME_2_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LIMITE_SPRR_ALRME_2_003) ? MED_PRSO_LIMITE_SPRR_ALRME_2_003 : 0,
                                        MED_PRSO_LMTE_INFRR_ALRME_2_003 = double.TryParse(instDiferenPressaoAlta?.MED_PRSO_LMTE_INFRR_ALRME_2_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LMTE_INFRR_ALRME_2_003) ? MED_PRSO_LMTE_INFRR_ALRME_2_003 : 0,

                                        #endregion

                                        #region inst diferen pressao media
                                        NUM_SERIE_4_003 = instDiferenPressaoMedia?.NUM_SERIE_4_003,
                                        MED_PRSO_LIMITE_SPRR_ALRME_3_003 = double.TryParse(instDiferenPressaoMedia?.MED_PRSO_LIMITE_SPRR_ALRME_3_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LIMITE_SPRR_ALRME_3_003) ? MED_PRSO_LIMITE_SPRR_ALRME_3_003 : 0,
                                        MED_PRSO_LMTE_INFRR_ALRME_3_003 = double.TryParse(instDiferenPressaoMedia?.MED_PRSO_LMTE_INFRR_ALRME_3_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LMTE_INFRR_ALRME_3_003) ? MED_PRSO_LMTE_INFRR_ALRME_3_003 : 0,
                                        #endregion

                                        #region inst diferen pressao baixa
                                        NUM_SERIE_5_003 = instDiferenPressaoBaixa?.NUM_SERIE_5_003,
                                        MED_PRSO_LIMITE_SPRR_ALRME_4_003 = double.TryParse(instDiferenPressaoBaixa?.MED_PRSO_LIMITE_SPRR_ALRME_4_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LIMITE_SPRR_ALRME_4_003) ? MED_PRSO_LIMITE_SPRR_ALRME_4_003 : 0,
                                        MED_PRSO_LMTE_INFRR_ALRME_4_003 = double.TryParse(instDiferenPressaoBaixa?.MED_PRSO_LMTE_INFRR_ALRME_4_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LMTE_INFRR_ALRME_4_003) ? MED_PRSO_LMTE_INFRR_ALRME_4_003 : 0,
                                        IND_HABILITACAO_ALARME_4_003 = instDiferenPressaoBaixa?.IND_HABILITACAO_ALARME_4_003,
                                        MED_PRSO_ADOTADA_FALHA_2_003 = double.TryParse(instDiferenPressaoBaixa?.MED_PRSO_ADOTADA_FALHA_2_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_ADOTADA_FALHA_2_003) ? MED_PRSO_ADOTADA_FALHA_2_003 : 0,
                                        DSC_ESTADO_INSNO_CASO_FALHA_2_003 = instDiferenPressaoBaixa?.DSC_ESTADO_INSNO_CASO_FALHA_2_003,
                                        MED_CUTOFF_KPA_1_003 = double.TryParse(instDiferenPressaoBaixa?.MED_CUTOFF_KPA_1_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_CUTOFF_KPA_1_003) ? MED_CUTOFF_KPA_1_003 : 0,

                                        #endregion

                                        #region inst diferen pressao principal
                                        NUM_SERIE_6_003 = instDiferenPressaoPrincipal?.NUM_SERIE_6_003,
                                        MED_PRSO_LIMITE_SPRR_ALRME_5_003 = double.TryParse(instDiferenPressaoPrincipal?.MED_PRSO_LIMITE_SPRR_ALRME_5_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LIMITE_SPRR_ALRME_5_003) ? MED_PRSO_LIMITE_SPRR_ALRME_5_003 : 0,
                                        MED_PRSO_LMTE_INFRR_ALRME_5_003 = double.TryParse(instDiferenPressaoPrincipal?.MED_PRSO_LMTE_INFRR_ALRME_5_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LMTE_INFRR_ALRME_5_003) ? MED_PRSO_LMTE_INFRR_ALRME_5_003 : 0,
                                        IND_HABILITACAO_ALARME_5_003 = instDiferenPressaoPrincipal?.IND_HABILITACAO_ALARME_5_003,
                                        MED_PRSO_ADOTADA_FALHA_3_003 = double.TryParse(instDiferenPressaoPrincipal?.MED_PRSO_ADOTADA_FALHA_3_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_ADOTADA_FALHA_3_003) ? MED_PRSO_ADOTADA_FALHA_3_003 : 0,
                                        DSC_ESTADO_INSNO_CASO_FALHA_3_003 = instDiferenPressaoPrincipal?.DSC_ESTADO_INSNO_CASO_FALHA_3_003,
                                        MED_CUTOFF_KPA_2_003 = double.TryParse(instDiferenPressaoPrincipal?.MED_CUTOFF_KPA_2_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_CUTOFF_KPA_2_003) ? MED_CUTOFF_KPA_2_003 : 0,
                                        #endregion

                                        #region producao
                                        DHA_INICIO_PERIODO_MEDICAO_003 = DateTime.TryParseExact(producao?.DHA_INICIO_PERIODO_MEDICAO_003, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var DHA_INICIO_PERIODO_MEDICAO_003) ? DHA_INICIO_PERIODO_MEDICAO_003 : null,
                                        DHA_FIM_PERIODO_MEDICAO_003 = DateTime.TryParseExact(producao?.DHA_FIM_PERIODO_MEDICAO_003, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var DHA_FIM_PERIODO_MEDICAO_003) ? DHA_FIM_PERIODO_MEDICAO_003 : null,
                                        ICE_DENSIDADE_RELATIVA_003 = double.TryParse(producao?.ICE_DENSIDADE_RELATIVA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_DENSIDADE_RELATIVA_003) ? ICE_DENSIDADE_RELATIVA_003 : 0,
                                        MED_DIFERENCIAL_PRESSAO_003 = double.TryParse(producao?.MED_DIFERENCIAL_PRESSAO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_DIFERENCIAL_PRESSAO_003) ? MED_DIFERENCIAL_PRESSAO_003 : 0,
                                        MED_PRESSAO_ESTATICA_003 = double.TryParse(producao?.MED_PRESSAO_ESTATICA_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRESSAO_ESTATICA_003) ? MED_PRESSAO_ESTATICA_003 : 0,
                                        MED_TEMPERATURA_2_003 = double.TryParse(producao?.MED_TEMPERATURA_2_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TEMPERATURA_2_003) ? MED_TEMPERATURA_2_003 : 0,
                                        PRZ_DURACAO_FLUXO_EFETIVO_003 = double.TryParse(producao?.PRZ_DURACAO_FLUXO_EFETIVO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PRZ_DURACAO_FLUXO_EFETIVO_003) ? PRZ_DURACAO_FLUXO_EFETIVO_003 : 0,
                                        MED_CORRIGIDO_MVMDO_003 = double.TryParse(producao?.MED_CORRIGIDO_MVMDO_003?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_CORRIGIDO_MVMDO_003) ? MED_CORRIGIDO_MVMDO_003 : 0,
                                        #endregion

                                        #region FileType relation
                                        FileType = new FileType
                                        {
                                            Name = data.Files[i].FileName,
                                            Acronym = "PMGD",

                                        },
                                        #endregion

                                        #region User relation
                                        User = user,

                                        #endregion

                                    };

                                    await context.Measurements.AddAsync(measurement);
                                    var measurement003DTO = _mapper.Map<Measurement, _003DTO>(measurement);
                                    _responseResult._003File ??= new List<_003DTO>();
                                    _responseResult._003File?.Add(measurement003DTO);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                    return BadRequest(new ErrorResponseDTO
                                    {
                                        Message = $"Something went wrong: {ex.Message}"
                                    });
                                }
                            }
                            break;

                            #endregion
                    }
                }
            }
            await context.SaveChangesAsync();

            return Created("measurements", _responseResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context, [FromQuery] string? acronym, [FromQuery] string? name)
        {
            var filesQuery = context.FileTypes.Include(x => x.Measurements).ThenInclude(m => m.User);

            if (!string.IsNullOrEmpty(acronym))
            {
                var possibleAcronymValues = new List<string> { "PMO", "PMGL", "PMGD", "EFM" };
                var isValidValue = possibleAcronymValues.Contains(acronym.ToUpper().Trim());
                if (!isValidValue)
                    return BadRequest(new ErrorResponseDTO
                    {
                        Message = $"Acronym valid values are: PMO, PMGL, PMGD, EFM"
                    });

                filesQuery = context.FileTypes
                   .Where(x => x.Acronym == acronym)
                   .Include(x => x.Measurements)
                   .ThenInclude(m => m.User);
            }

            if (!string.IsNullOrEmpty(name))
            {
                var possibleNameValues = new List<string> { "001", "002", "003", "039" };
                var isValidValue = possibleNameValues.Contains(name.ToUpper().Trim());
                if (!isValidValue)
                    return BadRequest(new ErrorResponseDTO
                    {
                        Message = "Name valid values are: 001, 002, 003, 039"
                    });

                filesQuery = context.FileTypes
                    .Where(x => x.Name == name)
                    .Include(x => x.Measurements)
                    .ThenInclude(m => m.User);
            }

            var files = await filesQuery.ToListAsync();
            var measurements = files.SelectMany(file => file.Measurements);

            foreach (var measurement in measurements)
            {
                switch (measurement.FileType.Name)
                {
                    case "001":
                        _responseResult._001File ??= new List<_001DTO>();
                        _responseResult._001File.Add(_mapper.Map<_001DTO>(measurement));
                        break;

                    case "002":
                        _responseResult._002File ??= new List<_002DTO>();
                        _responseResult._002File.Add(_mapper.Map<_002DTO>(measurement));
                        break;

                    case "003":
                        _responseResult._003File ??= new List<_003DTO>();
                        _responseResult._003File.Add(_mapper.Map<_003DTO>(measurement));
                        break;

                    case "039":
                        _responseResult._039File ??= new List<_039DTO>();
                        _responseResult._039File.Add(_mapper.Map<_039DTO>(measurement));
                        break;
                }
            }

            return Ok(_responseResult);
        }

    }
}

