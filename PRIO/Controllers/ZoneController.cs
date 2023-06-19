using Microsoft.AspNetCore.Mvc;
using PRIO.DTOS.GlobalDTOS;
using PRIO.Filters;
using PRIO.Models.UserControlAccessModels;
using PRIO.Services.HierarchyServices;
using PRIO.ViewModels.HierarchyViewModels.Zones;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("zones")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class ZoneController : ControllerBase
    {
        private readonly ZoneService _zoneService;
        public ZoneController(ZoneService service)
        {
            _zoneService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateZoneViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var zoneDTO = await _zoneService.CreateZone(body, user);
            return Created($"zones/{zoneDTO.Id}", zoneDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var zonesDTO = await _zoneService.GetZones();
            return Ok(zonesDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var zoneDTO = await _zoneService.GetZoneById(id);
            return Ok(zoneDTO);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateZoneViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var zoneDTO = await _zoneService.UpdateZone(body, id, user);
            return Ok(zoneDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            await _zoneService.DeleteZone(id, user);

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

            var zoneDTO = await _zoneService.RestoreZone(id, user);
            return Ok(zoneDTO);
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistoryById([FromRoute] Guid id)
        {
            var zoneHistories = await _zoneService.GetZoneHistory(id);
            return Ok(zoneHistories);
        }
    }
}
