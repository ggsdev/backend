using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Files._039;
using PRIO.Models;
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
        public async Task<ActionResult> XMLTeste([FromForm] IFormFile file, [FromForm] string json, [FromServices] DataContext context)
        {
            var jsonData = JsonConvert.DeserializeObject<JsonData>(json);
            var basePath = "C:\\Users\\gabri\\source\\repos\\PrioANP\\backend\\PRIO\\PRIO\\Files\\_039";
            var tempPath = Path.GetTempPath();
            var pathSchema = Path.Combine(basePath, "Schema.xsd");
            var formattedDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            if (jsonData is not null && (jsonData.FileType == "039" && jsonData.UnitName == "bravo"))
            {
                // Continue with your logic here
            }

            var pathXml = Path.Combine(tempPath, $"039_03255266_{formattedDateTime}_eeeeeeeeeeeeeee(campo_opcional).xml");

            using var stream = System.IO.File.Create(pathXml);

            if (file is not null)
                await file.CopyToAsync(stream);

            stream.Close();
            var result = Functions.CheckFormat(pathXml, pathSchema);

            //if (result is not null && result.Count > 0)
            //    return BadRequest(new { Message = result });

            var xmlDocument = XDocument.Load(pathXml);

            var rootElement = xmlDocument.Root;
            var listaDadosBasicosElement = rootElement?.Element("LISTA_DADOS_BASICOS");
            var dadosBasicosElement = listaDadosBasicosElement?.Element("DADOS_BASICOS");

            if (dadosBasicosElement != null)
            {
                var serializer = new XmlSerializer(typeof(DADOS_BASICOS));
                using XmlReader reader = dadosBasicosElement.CreateReader();
                var dadosBasicos = (DADOS_BASICOS)serializer.Deserialize(reader);
                var measurement = _mapper.Map<Measurement>(dadosBasicos);

                await context.Measurements.AddAsync(measurement);

                await context.SaveChangesAsync();

                return Ok(measurement);
            }

            return BadRequest(new ErrorResponseDTO
            {
                Message = "Error when saving measurement"
            });
        }
    }
}

