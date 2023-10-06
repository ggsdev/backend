using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.PI.Infra.Http.Services;
using PRIO.src.Modules.PI.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.PI.Infra.Http.Controllers
{
    [ApiController]
    [Route("PI")]
    [ServiceFilter(typeof(AuthorizationFilter))]
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

        [HttpPost("tags")]
        public async Task<IActionResult> CreateTag(CreateTagViewModel tagViewModel)
        {
            var data = await _service.CreateTag(tagViewModel);

            return Created($"{data.Id}", data);
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
