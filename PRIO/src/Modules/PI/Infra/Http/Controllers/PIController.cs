using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.PI.Infra.Http.Services;

namespace PRIO.src.Modules.PI.Infra.Http.Controllers
{
    [ApiController]
    [Route("Pitest")]
    public class PIController : ControllerBase
    {
        private readonly PIService _service;
        public PIController(PIService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task TestPI()
        {
            await _service.TestPI();
        }

        [HttpGet("history")]
        public async Task<IActionResult> TestPI([FromQuery] string date)
        {
            var PIValuesDTO = await _service.GetHistoryByDate(date);

            return Ok(PIValuesDTO);
        }
    }
}
