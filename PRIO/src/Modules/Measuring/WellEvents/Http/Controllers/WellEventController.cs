using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
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
            var user = HttpContext.Items["User"] as User;

            await _service.CloseWellEvent(body, user);

            return NoContent();
        }

        [HttpPost("open")]
        public async Task<IActionResult> Post(CreateOpeningEventViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            await _service.OpenWellEvent(body, user);

            return NoContent();
        }

        [HttpPatch("{eventId}")]
        public async Task<IActionResult> Post(Guid eventId, CreateReasonViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            await _service.UpdateClosedEvent(eventId, body, user);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid fieldId, string eventType)
        {
            var data = await _service.GetWellsWithEvents(fieldId, eventType);

            return Ok(data);
        }

        [HttpPatch("reason/{id}")]
        public async Task<IActionResult> UpdateReason(Guid id, UpdateReasonViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var data = await _service.UpdateReason(id, body, user);

            return Ok(data);
        }

        [HttpGet("well/{wellId}")]
        public async Task<IActionResult> GetWellEvents([FromRoute] Guid wellId, [FromQuery] string? date)
        {
            var data = await _service.GetWellEvents(wellId, date);

            return Ok(data);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get([FromRoute] Guid eventId)
        {
            var data = await _service.GetEventById(eventId);

            return Ok(data);
        }

        [HttpGet("ueps")]
        public async Task<IActionResult> Get()
        {
            var data = await _service.GetUepsForWellEvent();

            return Ok(data);
        }
    }
}
