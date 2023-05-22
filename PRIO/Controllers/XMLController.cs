﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Files._001;
using PRIO.Files._002;
using PRIO.Files._039;
using PRIO.Models;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PRIO.Controllers
{
    [ApiController]
    public class XMLController : ControllerBase
    {

        private readonly IMapper _mapper;

        public XMLController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("xml")]
        [AllowAnonymous]
        //receber como base64
        public async Task<ActionResult> XMLTeste([FromForm] IFormFile file, [FromServices] DataContext context)
        {
            var fileName = file.FileName.Substring(0, 3);

            #region validação tipo de arquivo
            var typeOfFiles = new List<string>()
            {
                "039",
                "001",
                "002",
                "003"
            };
            var validInput = typeOfFiles.Contains(fileName);

            if (!validInput)
                return BadRequest(new ErrorResponseDTO
                {
                    Message = "File type invalid, choose between: 001,002,003,039"
                });
            #endregion

            #region pathing
            var basePath = "C:\\Users\\gabri\\source\\repos\\PrioANP\\backend\\PRIO\\PRIO\\Files\\";
            var tempPath = Path.GetTempPath();
            var formattedDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            var pathXml = Path.Combine(tempPath, $"{fileName}_03255266_{formattedDateTime}_eeeeeeeeeeeeeee(campo_opcional).xml");
            var pathSchema = basePath + $"\\_{fileName}\\Schema.xsd";
            #endregion

            var stream = System.IO.File.Create(pathXml);

            using (stream)
            {
                await file.CopyToAsync(stream);
            }

            var result = Functions.CheckFormat(pathXml, pathSchema);
            if (result is not null && result.Count > 0)
                return BadRequest(new { Message = result });

            var documentXml = XDocument.Load(pathXml);

            #region 039
            if (fileName == "039")
            {
                var rootElement = documentXml.Root;
                var listaDadosBasicosElement = rootElement?.Element("LISTA_DADOS_BASICOS");
                var dadosBasicosElement = listaDadosBasicosElement?.Element("DADOS_BASICOS");

                var serializer = new XmlSerializer(typeof(DADOS_BASICOS_039));
                using XmlReader reader = dadosBasicosElement.CreateReader();
                var dadosBasicos = (DADOS_BASICOS_039)serializer.Deserialize(reader);
                var measurement = _mapper.Map<Measurement>(dadosBasicos);

                await context.Measurements.AddAsync(measurement);

                await context.SaveChangesAsync();

                var measurement039DTO = _mapper.Map<_039DTO>(measurement);
                return Ok(measurement039DTO);

            }
            #endregion

            #region 001
            if (fileName == "001")
            {
                var rootElement = documentXml.Root;

                var dadosBasicosElement = rootElement?.Element("LISTA_DADOS_BASICOS")?.Element("DADOS_BASICOS");
                var serializerDadosBasicos = new XmlSerializer(typeof(DADOS_BASICOS_001));
                var dadosBasicos = (DADOS_BASICOS_001)serializerDadosBasicos.Deserialize(dadosBasicosElement.CreateReader());

                var configuracaoCvElement = dadosBasicosElement?.Element("LISTA_CONFIGURACAO_CV")?.Element("CONFIGURACAO_CV");
                var serializerConfiguracaoCv = new XmlSerializer(typeof(CONFIGURACAO_CV_001));
                var configuracaoCv = (CONFIGURACAO_CV_001)serializerConfiguracaoCv.Deserialize(configuracaoCvElement.CreateReader());

                var elementoPrimarioElement = dadosBasicosElement?.Element("LISTA_ELEMENTO_PRIMARIO")?.Element("ELEMENTO_PRIMARIO");
                var serializerElementoPrimario = new XmlSerializer(typeof(ELEMENTO_PRIMARIO_001));
                var elementoPrimario = (ELEMENTO_PRIMARIO_001)serializerElementoPrimario.Deserialize(elementoPrimarioElement.CreateReader());

                var instrumentoPressaoElement = dadosBasicosElement?.Element("LISTA_INSTRUMENTO_PRESSAO")?.Element("INSTRUMENTO_PRESSAO");
                var serializerInstrumentoPressao = new XmlSerializer(typeof(INSTRUMENTO_PRESSAO_001));
                var instrumentoPressao = (INSTRUMENTO_PRESSAO_001)serializerInstrumentoPressao.Deserialize(instrumentoPressaoElement.CreateReader());

                var instrumentoTemperaturaElement = dadosBasicosElement?.Element("LISTA_INSTRUMENTO_TEMPERATURA")?.Element("INSTRUMENTO_TEMPERATURA");
                var serializerInstrumentoTemperatura = new XmlSerializer(typeof(INSTRUMENTO_TEMPERATURA_001));
                var instrumentoTemperatura = (INSTRUMENTO_TEMPERATURA_001)serializerInstrumentoTemperatura.Deserialize(instrumentoTemperaturaElement.CreateReader());

                var producaoElement = dadosBasicosElement?.Element("LISTA_PRODUCAO")?.Element("PRODUCAO");
                var serializerProducao = new XmlSerializer(typeof(PRODUCAO_001));
                var producao = (PRODUCAO_001)serializerProducao.Deserialize(producaoElement.CreateReader());

                var measurement = new Measurement()
                {
                    #region atributos dados basicos
                    NUM_SERIE_ELEMENTO_PRIMARIO_001 = dadosBasicos?.NUM_SERIE_ELEMENTO_PRIMARIO_001,
                    COD_INSTALACAO_001 = dadosBasicos?.COD_INSTALACAO_001,
                    COD_TAG_PONTO_MEDICAO_001 = dadosBasicos?.COD_TAG_PONTO_MEDICAO_001,
                    #endregion

                    #region configuracao cv
                    NUM_SERIE_COMPUTADOR_VAZAO_001 = configuracaoCv?.NUM_SERIE_COMPUTADOR_VAZAO_001,
                    DHA_COLETA_001 = DateTime.TryParseExact(configuracaoCv?.DHA_COLETA_001, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dhaColeta) ? dhaColeta : null,
                    MED_TEMPERATURA_001 = double.TryParse(configuracaoCv?.MED_TEMPERATURA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medTemperatura) ? medTemperatura : null,
                    MED_PRESSAO_ATMSA_001 = double.TryParse(configuracaoCv?.MED_PRESSAO_ATMSA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medPressaoAtmsa) ? medPressaoAtmsa : null,
                    MED_PRESSAO_RFRNA_001 = double.TryParse(configuracaoCv?.MED_PRESSAO_RFRNA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medPressaoRfrna) ? medPressaoRfrna : null,
                    DSC_VERSAO_SOFTWARE_001 = configuracaoCv?.DSC_VERSAO_SOFTWARE_001,
                    #endregion

                    #region elemento primario
                    ICE_METER_FACTOR_1_001 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_1_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceMeterFactor1) ? iceMeterFactor1 : 0,
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
                    QTD_PULSOS_K_FACTOR_12_001 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_12_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtdPulsosKFactor12) ? qtdPulsosKFactor12 : 0,
                    ICE_CUTOFF_001 = double.TryParse(elementoPrimario?.ICE_CUTOFF_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceCutoff) ? iceCutoff : 0,
                    ICE_LIMITE_SPRR_ALARME_001 = double.TryParse(elementoPrimario?.ICE_LIMITE_SPRR_ALARME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceLimiteSprrAlarme) ? iceLimiteSprrAlarme : 0,
                    ICE_LIMITE_INFRR_ALARME_001 = double.TryParse(elementoPrimario?.ICE_LIMITE_INFRR_ALARME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var iceLimiteInfrrAlarme) ? iceLimiteInfrrAlarme : 0,
                    IND_HABILITACAO_ALARME_1_001 = elementoPrimario?.IND_HABILITACAO_ALARME_1_001,
                    #endregion

                    #region instrumento pressao
                    NUM_SERIE_1_001 = instrumentoPressao?.NUM_SERIE_1_001,
                    MED_PRSO_LIMITE_SPRR_ALRME_001 = double.TryParse(instrumentoPressao?.MED_PRSO_LIMITE_SPRR_ALRME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var limiteSprr) ? limiteSprr : 0,
                    MED_PRSO_LMTE_INFRR_ALRME_001 = double.TryParse(instrumentoPressao?.MED_PRSO_LMTE_INFRR_ALRME_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var limiteInfrr) ? limiteInfrr : 0,
                    IND_HABILITACAO_ALARME_2_001 = instrumentoPressao.IND_HABILITACAO_ALARME_2_001,
                    MED_PRSO_ADOTADA_FALHA_001 = double.TryParse(instrumentoPressao?.MED_PRSO_ADOTADA_FALHA_001?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var adotFalha) ? adotFalha : 0,
                    DSC_ESTADO_INSNO_CASO_FALHA_001 = instrumentoPressao.DSC_ESTADO_INSNO_CASO_FALHA_001,
                    IND_TIPO_PRESSAO_CONSIDERADA_001 = instrumentoPressao.IND_TIPO_PRESSAO_CONSIDERADA_001,
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

                    DHA_INICIO_PERIODO_MEDICAO_001 = DateTime.TryParseExact(producao?.DHA_INICIO_PERIODO_MEDICAO_001, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var inicioPer) ? inicioPer : null,
                    DHA_FIM_PERIODO_MEDICAO_001 = DateTime.TryParseExact(producao?.DHA_FIM_PERIODO_MEDICAO_001, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fimPer) ? fimPer : null,
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

                };


                await context.Measurements.AddAsync(measurement);

                await context.SaveChangesAsync();

                var measurement001DTO = _mapper.Map<_001DTO>(measurement);
                return Ok(measurement001DTO);
            }
            #endregion

            #region 002
            if (fileName == "002")
            {
                var rootElement = documentXml.Root;

                var dadosBasicosElement = rootElement?.Element("LISTA_DADOS_BASICOS")?.Element("DADOS_BASICOS");
                var serializerDadosBasicos = new XmlSerializer(typeof(DADOS_BASICOS_002));
                var dadosBasicos = (DADOS_BASICOS_002)serializerDadosBasicos.Deserialize(dadosBasicosElement.CreateReader());

                var configuracaoCvElement = rootElement?.Element("LISTA_CONFIGURACAO_CV")?.Element("CONFIGURACAO_CV");
                var serializerConfiguracaoCv = new XmlSerializer(typeof(CONFIGURACAO_CV_002));
                var configuracaoCv = (CONFIGURACAO_CV_002)serializerConfiguracaoCv.Deserialize(configuracaoCvElement.CreateReader());

                var elementoPrimarioElement = rootElement?.Element("LISTA_ELEMENTO_PRIMARIO")?.Element("ELEMENTO_PRIMARIO");
                var serializerElementoPrimario = new XmlSerializer(typeof(ELEMENTO_PRIMARIO_002));
                var elementoPrimario = (ELEMENTO_PRIMARIO_002)serializerElementoPrimario.Deserialize(elementoPrimarioElement.CreateReader());

                var instrumentoPressaoElement = rootElement?.Element("LISTA_INSTRUMENTO_PRESSAO")?.Element("INSTRUMENTO_PRESSAO");
                var serializerInstrumentoPressao = new XmlSerializer(typeof(INSTRUMENTO_PRESSAO_002));
                var instrumentoPressao = (INSTRUMENTO_PRESSAO_002)serializerInstrumentoPressao.Deserialize(instrumentoPressaoElement.CreateReader());

                var instrumentoTemperaturaElement = rootElement?.Element("LISTA_INSTRUMENTO_TEMPERATURA")?.Element("INSTRUMENTO_TEMPERATURA");
                var serializerInstrumentoTemperatura = new XmlSerializer(typeof(INSTRUMENTO_TEMPERATURA_002));
                var instrumentoTemperatura = (INSTRUMENTO_TEMPERATURA_002)serializerInstrumentoTemperatura.Deserialize(instrumentoTemperaturaElement.CreateReader());

                try
                {

                    var measurement = new Measurement()
                    {
                        #region atributos dados basicos
                        NUM_SERIE_ELEMENTO_PRIMARIO_002 = dadosBasicos.NUM_SERIE_ELEMENTO_PRIMARIO_002,
                        COD_INSTALACAO_002 = dadosBasicos.COD_INSTALACAO_002,
                        COD_TAG_PONTO_MEDICAO_002 = dadosBasicos.COD_TAG_PONTO_MEDICAO_002,
                        #endregion 

                        #region configuracao cv
                        NUM_SERIE_COMPUTADOR_VAZAO_002 = configuracaoCv.NUM_SERIE_COMPUTADOR_VAZAO_002,
                        DHA_COLETA_002 = double.TryParse(configuracaoCv?.DHA_COLETA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double dhaColeta) ? dhaColeta : null,
                        MED_TEMPERATURA_1_002 = double.TryParse(configuracaoCv?.MED_TEMPERATURA_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medTemp) ? medTemp : null,
                        MED_PRESSAO_ATMSA_002 = double.TryParse(configuracaoCv?.MED_PRESSAO_ATMSA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medPressAt) ? medPressAt : null,
                        MED_PRESSAO_RFRNA_002 = double.TryParse(configuracaoCv?.MED_PRESSAO_RFRNA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medPressRf) ? medPressRf : null,
                        MED_DENSIDADE_RELATIVA_002 = double.TryParse(configuracaoCv?.MED_DENSIDADE_RELATIVA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double medDens) ? medDens : null,
                        DSC_NORMA_UTILIZADA_CALCULO_002 = configuracaoCv.DSC_NORMA_UTILIZADA_CALCULO_002,
                        PCT_CROMATOGRAFIA_NITROGENIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_NITROGENIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double pctCromaNit) ? pctCromaNit : null,

                        PCT_CROMATOGRAFIA_CO2_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_CO2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double pctCromaCO2) ? pctCromaCO2 : null,
                        PCT_CROMATOGRAFIA_METANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_METANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_METANO_002) ? PCT_CROMATOGRAFIA_METANO_002 : null,
                        PCT_CROMATOGRAFIA_ETANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_ETANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_ETANO_002) ? PCT_CROMATOGRAFIA_ETANO_002 : null,
                        PCT_CROMATOGRAFIA_PROPANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_PROPANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_PROPANO_002) ? PCT_CROMATOGRAFIA_PROPANO_002 : null,
                        PCT_CROMATOGRAFIA_N_BUTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_N_BUTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_N_BUTANO_002) ? PCT_CROMATOGRAFIA_N_BUTANO_002 : null,
                        PCT_CROMATOGRAFIA_I_BUTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_I_BUTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_I_BUTANO_002) ? PCT_CROMATOGRAFIA_I_BUTANO_002 : null,
                        PCT_CROMATOGRAFIA_N_PENTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_N_PENTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_N_PENTANO_002) ? PCT_CROMATOGRAFIA_N_PENTANO_002 : null,
                        PCT_CROMATOGRAFIA_I_PENTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_I_PENTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_I_PENTANO_002) ? PCT_CROMATOGRAFIA_I_PENTANO_002 : null,
                        PCT_CROMATOGRAFIA_HEXANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HEXANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HEXANO_002) ? PCT_CROMATOGRAFIA_HEXANO_002 : null,
                        PCT_CROMATOGRAFIA_HEPTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HEPTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HEPTANO_002) ? PCT_CROMATOGRAFIA_HEPTANO_002 : null,
                        PCT_CROMATOGRAFIA_OCTANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_OCTANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_OCTANO_002) ? PCT_CROMATOGRAFIA_OCTANO_002 : null,
                        PCT_CROMATOGRAFIA_NONANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_NONANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_NONANO_002) ? PCT_CROMATOGRAFIA_NONANO_002 : null,
                        PCT_CROMATOGRAFIA_DECANO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_DECANO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_DECANO_002) ? PCT_CROMATOGRAFIA_DECANO_002 : null,
                        PCT_CROMATOGRAFIA_H2S_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_H2S_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_H2S_002) ? PCT_CROMATOGRAFIA_H2S_002 : null,
                        PCT_CROMATOGRAFIA_AGUA_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_AGUA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_AGUA_002) ? PCT_CROMATOGRAFIA_AGUA_002 : null,
                        PCT_CROMATOGRAFIA_HELIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HELIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HELIO_002) ? PCT_CROMATOGRAFIA_HELIO_002 : null,
                        PCT_CROMATOGRAFIA_OXIGENIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_OXIGENIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_OXIGENIO_002) ? PCT_CROMATOGRAFIA_OXIGENIO_002 : null,
                        PCT_CROMATOGRAFIA_CO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_CO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_CO_002) ? PCT_CROMATOGRAFIA_CO_002 : null,
                        PCT_CROMATOGRAFIA_HIDROGENIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_HIDROGENIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_HIDROGENIO_002) ? PCT_CROMATOGRAFIA_HIDROGENIO_002 : null,
                        PCT_CROMATOGRAFIA_ARGONIO_002 = double.TryParse(configuracaoCv?.PCT_CROMATOGRAFIA_ARGONIO_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double PCT_CROMATOGRAFIA_ARGONIO_002) ? PCT_CROMATOGRAFIA_ARGONIO_002 : null,

                        DSC_VERSAO_SOFTWARE_002 = configuracaoCv.DSC_VERSAO_SOFTWARE_002,

                        #endregion

                        #region elemento primario
                        ICE_METER_FACTOR_1_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_1_002) ? ICE_METER_FACTOR_1_002 : null,
                        ICE_METER_FACTOR_2_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_2_002) ? ICE_METER_FACTOR_2_002 : null,
                        ICE_METER_FACTOR_3_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_3_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_3_002) ? ICE_METER_FACTOR_3_002 : null,
                        ICE_METER_FACTOR_4_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_4_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_4_002) ? ICE_METER_FACTOR_4_002 : null,
                        ICE_METER_FACTOR_5_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_5_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_5_002) ? ICE_METER_FACTOR_5_002 : null,
                        ICE_METER_FACTOR_6_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_6_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_6_002) ? ICE_METER_FACTOR_6_002 : null,
                        ICE_METER_FACTOR_7_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_7_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_7_002) ? ICE_METER_FACTOR_7_002 : null,
                        ICE_METER_FACTOR_8_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_8_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_8_002) ? ICE_METER_FACTOR_8_002 : null,
                        ICE_METER_FACTOR_9_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_9_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_9_002) ? ICE_METER_FACTOR_9_002 : null,
                        ICE_METER_FACTOR_10_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_10_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_10_002) ? ICE_METER_FACTOR_10_002 : null,
                        ICE_METER_FACTOR_11_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_11_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_11_002) ? ICE_METER_FACTOR_11_002 : null,
                        ICE_METER_FACTOR_12_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_12_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_12_002) ? ICE_METER_FACTOR_12_002 : null,
                        ICE_METER_FACTOR_13_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_13_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_13_002) ? ICE_METER_FACTOR_13_002 : null,
                        ICE_METER_FACTOR_14_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_14_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_14_002) ? ICE_METER_FACTOR_14_002 : null,
                        ICE_METER_FACTOR_15_002 = double.TryParse(elementoPrimario?.ICE_METER_FACTOR_15_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_METER_FACTOR_15_002) ? ICE_METER_FACTOR_15_002 : null,

                        QTD_PULSOS_METER_FACTOR_1_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_1_002) ? QTD_PULSOS_METER_FACTOR_1_002 : null,
                        QTD_PULSOS_METER_FACTOR_2_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_2_002) ? QTD_PULSOS_METER_FACTOR_2_002 : null,
                        QTD_PULSOS_METER_FACTOR_3_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_3_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_3_002) ? QTD_PULSOS_METER_FACTOR_3_002 : null,
                        QTD_PULSOS_METER_FACTOR_4_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_4_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_4_002) ? QTD_PULSOS_METER_FACTOR_4_002 : null,
                        QTD_PULSOS_METER_FACTOR_5_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_5_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_5_002) ? QTD_PULSOS_METER_FACTOR_5_002 : null,
                        QTD_PULSOS_METER_FACTOR_6_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_6_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_6_002) ? QTD_PULSOS_METER_FACTOR_6_002 : null,
                        QTD_PULSOS_METER_FACTOR_7_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_7_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_7_002) ? QTD_PULSOS_METER_FACTOR_7_002 : null,
                        QTD_PULSOS_METER_FACTOR_8_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_8_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_8_002) ? QTD_PULSOS_METER_FACTOR_8_002 : null,
                        QTD_PULSOS_METER_FACTOR_9_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_9_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_9_002) ? QTD_PULSOS_METER_FACTOR_9_002 : null,
                        QTD_PULSOS_METER_FACTOR_10_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_10_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_10_002) ? QTD_PULSOS_METER_FACTOR_10_002 : null,
                        QTD_PULSOS_METER_FACTOR_11_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_11_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_11_002) ? QTD_PULSOS_METER_FACTOR_11_002 : null,
                        QTD_PULSOS_METER_FACTOR_12_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_12_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_12_002) ? QTD_PULSOS_METER_FACTOR_12_002 : null,
                        QTD_PULSOS_METER_FACTOR_13_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_13_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_13_002) ? QTD_PULSOS_METER_FACTOR_13_002 : null,
                        QTD_PULSOS_METER_FACTOR_14_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_14_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_14_002) ? QTD_PULSOS_METER_FACTOR_14_002 : null,
                        QTD_PULSOS_METER_FACTOR_15_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_METER_FACTOR_15_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_METER_FACTOR_15_002) ? QTD_PULSOS_METER_FACTOR_15_002 : null,

                        ICE_K_FACTOR_1_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_1_002) ? ICE_K_FACTOR_1_002 : null,
                        ICE_K_FACTOR_2_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_2_002) ? ICE_K_FACTOR_2_002 : null,
                        ICE_K_FACTOR_3_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_3_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_3_002) ? ICE_K_FACTOR_3_002 : null,
                        ICE_K_FACTOR_4_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_4_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_4_002) ? ICE_K_FACTOR_4_002 : null,
                        ICE_K_FACTOR_5_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_5_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_5_002) ? ICE_K_FACTOR_5_002 : null,
                        ICE_K_FACTOR_6_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_6_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_6_002) ? ICE_K_FACTOR_6_002 : null,
                        ICE_K_FACTOR_7_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_7_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_7_002) ? ICE_K_FACTOR_7_002 : null,
                        ICE_K_FACTOR_8_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_8_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_8_002) ? ICE_K_FACTOR_8_002 : null,
                        ICE_K_FACTOR_9_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_9_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_9_002) ? ICE_K_FACTOR_9_002 : null,
                        ICE_K_FACTOR_10_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_10_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_10_002) ? ICE_K_FACTOR_10_002 : null,
                        ICE_K_FACTOR_11_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_11_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_11_002) ? ICE_K_FACTOR_11_002 : null,
                        ICE_K_FACTOR_12_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_12_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_12_002) ? ICE_K_FACTOR_12_002 : null,
                        ICE_K_FACTOR_13_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_13_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_13_002) ? ICE_K_FACTOR_13_002 : null,
                        ICE_K_FACTOR_14_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_14_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_14_002) ? ICE_K_FACTOR_14_002 : null,
                        ICE_K_FACTOR_15_002 = double.TryParse(elementoPrimario?.ICE_K_FACTOR_15_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_K_FACTOR_15_002) ? ICE_K_FACTOR_15_002 : null,

                        QTD_PULSOS_K_FACTOR_1_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_1_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_1_002) ? QTD_PULSOS_K_FACTOR_1_002 : null,
                        QTD_PULSOS_K_FACTOR_2_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_2_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_2_002) ? QTD_PULSOS_K_FACTOR_2_002 : null,
                        QTD_PULSOS_K_FACTOR_3_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_3_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_3_002) ? QTD_PULSOS_K_FACTOR_3_002 : null,
                        QTD_PULSOS_K_FACTOR_4_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_4_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_4_002) ? QTD_PULSOS_K_FACTOR_4_002 : null,
                        QTD_PULSOS_K_FACTOR_5_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_5_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_5_002) ? QTD_PULSOS_K_FACTOR_5_002 : null,
                        QTD_PULSOS_K_FACTOR_6_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_6_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_6_002) ? QTD_PULSOS_K_FACTOR_6_002 : null,
                        QTD_PULSOS_K_FACTOR_7_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_7_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_7_002) ? QTD_PULSOS_K_FACTOR_7_002 : null,
                        QTD_PULSOS_K_FACTOR_8_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_8_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_8_002) ? QTD_PULSOS_K_FACTOR_8_002 : null,
                        QTD_PULSOS_K_FACTOR_9_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_9_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_9_002) ? QTD_PULSOS_K_FACTOR_9_002 : null,
                        QTD_PULSOS_K_FACTOR_10_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_10_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_10_002) ? QTD_PULSOS_K_FACTOR_10_002 : null,
                        QTD_PULSOS_K_FACTOR_11_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_11_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_11_002) ? QTD_PULSOS_K_FACTOR_11_002 : null,
                        QTD_PULSOS_K_FACTOR_12_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_12_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_12_002) ? QTD_PULSOS_K_FACTOR_12_002 : null,
                        QTD_PULSOS_K_FACTOR_13_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_13_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_13_002) ? QTD_PULSOS_K_FACTOR_13_002 : null,
                        QTD_PULSOS_K_FACTOR_14_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_14_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_14_002) ? QTD_PULSOS_K_FACTOR_14_002 : null,
                        QTD_PULSOS_K_FACTOR_15_002 = double.TryParse(elementoPrimario?.QTD_PULSOS_K_FACTOR_15_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double QTD_PULSOS_K_FACTOR_15_002) ? QTD_PULSOS_K_FACTOR_15_002 : null,

                        ICE_CUTOFF_002 = double.TryParse(elementoPrimario?.ICE_CUTOFF_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_CUTOFF_002) ? ICE_CUTOFF_002 : null,
                        ICE_LIMITE_SPRR_ALARME_002 = double.TryParse(elementoPrimario?.ICE_LIMITE_SPRR_ALARME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_LIMITE_SPRR_ALARME_002) ? ICE_LIMITE_SPRR_ALARME_002 : null,
                        ICE_LIMITE_INFRR_ALARME_002 = double.TryParse(elementoPrimario?.ICE_LIMITE_INFRR_ALARME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double ICE_LIMITE_INFRR_ALARME_002) ? ICE_LIMITE_INFRR_ALARME_002 : null,
                        IND_HABILITACAO_ALARME_1_002 = elementoPrimario?.IND_HABILITACAO_ALARME_1_002,
                        #endregion

                        #region instrumento pressao

                        NUM_SERIE_1_002 = instrumentoPressao?.NUM_SERIE_1_002,
                        MED_PRSO_LIMITE_SPRR_ALRME_002 = double.TryParse(instrumentoPressao?.MED_PRSO_LIMITE_SPRR_ALRME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LIMITE_SPRR_ALRME_002) ? MED_PRSO_LIMITE_SPRR_ALRME_002 : null,
                        MED_PRSO_LMTE_INFRR_ALRME_002 = double.TryParse(instrumentoPressao?.MED_PRSO_LMTE_INFRR_ALRME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_LMTE_INFRR_ALRME_002) ? MED_PRSO_LMTE_INFRR_ALRME_002 : null,
                        IND_HABILITACAO_ALARME_2_002 = instrumentoPressao.IND_HABILITACAO_ALARME_2_002,
                        MED_PRSO_ADOTADA_FALHA_002 = double.TryParse(instrumentoPressao?.MED_PRSO_ADOTADA_FALHA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_PRSO_ADOTADA_FALHA_002) ? MED_PRSO_ADOTADA_FALHA_002 : null,
                        DSC_ESTADO_INSNO_CASO_FALHA_002 = instrumentoPressao.DSC_ESTADO_INSNO_CASO_FALHA_002,
                        IND_TIPO_PRESSAO_CONSIDERADA_002 = instrumentoPressao.IND_TIPO_PRESSAO_CONSIDERADA_002,

                        #endregion

                        #region instrumento temperatura
                        NUM_SERIE_2_002 = instrumentoTemperatura.NUM_SERIE_2_002,
                        MED_TMPTA_SPRR_ALARME_002 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_SPRR_ALARME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TMPTA_SPRR_ALARME_002) ? MED_TMPTA_SPRR_ALARME_002 : null,
                        MED_TMPTA_INFRR_ALRME_002 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_INFRR_ALRME_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TMPTA_INFRR_ALRME_002) ? MED_TMPTA_INFRR_ALRME_002 : null,
                        IND_HABILITACAO_ALARME_3_002 = instrumentoTemperatura.IND_HABILITACAO_ALARME_3_002,
                        MED_TMPTA_ADTTA_FALHA_002 = double.TryParse(instrumentoTemperatura?.MED_TMPTA_ADTTA_FALHA_002?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double MED_TMPTA_ADTTA_FALHA_002) ? MED_TMPTA_ADTTA_FALHA_002 : null,
                        DSC_ESTADO_INSTRUMENTO_FALHA_002 = instrumentoTemperatura.DSC_ESTADO_INSTRUMENTO_FALHA_002,

                        #endregion

                        #region producao
                        DHA_INICIO_PERIODO_MEDICAO_002 =


                        #endregion

                    };
                    await context.Measurements.AddAsync(measurement);

                    await context.SaveChangesAsync();

                    var measurement002DTO = _mapper.Map<_001DTO>(measurement);
                    return Ok(measurement002DTO);

                    #endregion           
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;

                }


            }
#endregion

            #region 003

            if (fileName == "003")
            {
                var rootElement = documentXml.Root;

                var dadosBasicosElement = rootElement?.Element("LISTA_DADOS_BASICOS")?.Element("DADOS_BASICOS");
                var serializerDadosBasicos = new XmlSerializer(typeof(DADOS_BASICOS_002));
                var dadosBasicos = (DADOS_BASICOS_002)serializerDadosBasicos.Deserialize(dadosBasicosElement.CreateReader());

                var configuracaoCvElement = rootElement?.Element("LISTA_CONFIGURACAO_CV")?.Element("CONFIGURACAO_CV");
                var serializerConfiguracaoCv = new XmlSerializer(typeof(CONFIGURACAO_CV_002));
                var configuracaoCv = (CONFIGURACAO_CV_002)serializerConfiguracaoCv.Deserialize(configuracaoCvElement.CreateReader());



            }

            #endregion

            return BadRequest(new ErrorResponseDTO
            {
                Message = "Error when saving measurement"
            });
        }
    }
}

