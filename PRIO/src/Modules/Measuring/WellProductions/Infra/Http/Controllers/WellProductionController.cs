using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.Measuring.WellProductions.Infra.Http.Services;
using PRIO.src.Modules.Measuring.WellProductions.Infra.ViewModels;
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

        [HttpPost]
        public async Task<IActionResult> Post(WellProductionViewModel body)
        {
            await _service.CreateAppropriation(body);

            return Ok();
        }
    }
}
