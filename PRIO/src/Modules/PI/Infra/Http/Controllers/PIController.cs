using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.PI.Infra.Http.Services;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.PI.Infra.Http.Controllers
{
    [ApiController]
    [Route("PI")]
    public class PIController : ControllerBase
    {
        private readonly PIService _service;
        public PIController(PIService service)
        {
            _service = service;
        }

        [HttpGet("ueps")]
        public async Task<IActionResult> GetDataByUep()
        {
            var data = await _service.GetDataByUep();
            return Ok(data);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromQuery] string date)
        {
            var PIValuesDTO = await _service.GetHistoryByDate(date);

            return Ok(PIValuesDTO);
        }


        [HttpGet("attributes/{wellId}")]
        public async Task<IActionResult> GetAttributesByWell([FromRoute] Guid wellId)
        {
            var PIAttributesWellDTO = await _service.GetAttributesByWell(wellId);

            return Ok(PIAttributesWellDTO);
        }
        
        [HttpGet("wells")]
        public async Task<IActionResult> GetByWellNameOperator(string wellName, string wellNameOperator)
        {
            if (wellName is null || wellNameOperator is null)
                throw new BadRequestException("Parâmetros de consulta devem ser utilizados: 'wellName' e 'wellNameOperator'.");

            var PIValuesDTO = await _service.GetTagsByWellName(wellName, wellNameOperator);

            return Ok(PIValuesDTO);

        }
    }
}
