using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Reservoirs.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Hierarchy.Reservoirs.Infra.Http.Controllers
{
    [ApiController]
    [Route("reservoirs")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class ReservoirController : ControllerBase
    {
        private readonly ReservoirService _reservoirService;
        public ReservoirController(ReservoirService service)
        {
            _reservoirService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservoirViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var reservoirDTO = await _reservoirService.CreateReservoir(body, user);
            return Created($"reservoirs/{reservoirDTO.Id}", reservoirDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reservoirsDTO = await _reservoirService.GetReservoirs();
            return Ok(reservoirsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var reservoirDTO = await _reservoirService.GetReservoirById(id);
            return Ok(reservoirDTO);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateReservoirViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var reservoirDTO = await _reservoirService.UpdateReservoir(body, id, user);
            return Ok(reservoirDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, [FromHeader] string StatusDate)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            await _reservoirService.DeleteReservoir(id, user, StatusDate);
            return NoContent();
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });
            var reservoirDTO = await _reservoirService.RestoreReservoir(id, user);
            return Ok(reservoirDTO);
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var reservoirHistories = await _reservoirService.GetReservoirHistory(id);
            return Ok(reservoirHistories);
        }
    }
}
