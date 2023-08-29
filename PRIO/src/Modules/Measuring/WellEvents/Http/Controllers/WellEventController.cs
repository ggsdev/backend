using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.Measuring.WellEvents.Http.Services;
using PRIO.src.Modules.Measuring.WellEvents.ViewModels;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Measuring.WellEvents.Http.Controllers
{
    [ApiController]
    [Route("well-events")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class WellEventController : ControllerBase
    {
        private readonly WellEventService _service;

        public WellEventController(WellEventService wellEventService)
        {
            _service = wellEventService;
        }

        [HttpPost("close")]
        public async Task<IActionResult> Post(CreateClosingEventViewModel body)
        {
            await _service.CloseWellFieldEvent(body);

            return NoContent();
        }
    }
}
