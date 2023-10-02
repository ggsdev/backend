using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.PI.Infra.Http.Services;

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
    }
}
