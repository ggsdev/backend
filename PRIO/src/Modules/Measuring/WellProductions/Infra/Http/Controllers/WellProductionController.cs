using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.Measuring.WellProductions.Infra.Http.Services;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Measuring.WellProductions.Infra.Http.Controllers
{
    [ApiController]
    [Route("production-appropriate")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class WellProductionController : ControllerBase
    {
        private readonly WellProductionService _service;

        public WellProductionController(WellProductionService service)
        {
            _service = service;
        }

        [HttpPost("{productionId}")]
        public async Task<IActionResult> Post([FromRoute] Guid productionId)
        {
            await _service.CreateAppropriation(productionId);

            return Ok();
        }
    }
}
