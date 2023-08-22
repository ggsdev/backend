using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.Measuring.WellAppropriations.Infra.Http.Services;
using PRIO.src.Modules.Measuring.WellAppropriations.Infra.ViewModels;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Measuring.WellAppropriations.Infra.Http.Controllers
{
    [ApiController]
    [Route("production-appropriate")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class WellAppropriationController : ControllerBase
    {
        private readonly WellAppropriationService _service;

        public WellAppropriationController(WellAppropriationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post(WellAppropriationViewModel body)
        {
            await _service.CreateAppropriation(body);

            return Ok();
        }
    }
}
