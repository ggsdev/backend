using Microsoft.AspNetCore.Mvc;
using PRIO.src.Shared.Auxiliaries.Infra.Http.Services;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Shared.Auxiliaries.Infra.Http.Controllers
{

    [ApiController]
    [Route("auxiliaries")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class AuxiliaryController : ControllerBase
    {
        private readonly AuxiliaryService _service;

        public AuxiliaryController(AuxiliaryService auxiliaryService)
        {
            _service = auxiliaryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string table, [FromQuery] string route)
        {
            var selectOptions = await _service.Get(table, route);

            return Ok(selectOptions);
        }
    }
}
