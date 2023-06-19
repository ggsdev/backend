using Microsoft.AspNetCore.Mvc;
using PRIO.DTOS.GlobalDTOS;
using PRIO.Filters;
using PRIO.Models.UserControlAccessModels;
using PRIO.Services.HierarchyServices;
using PRIO.ViewModels.Wells;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("wells")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class WellController : ControllerBase
    {
        private readonly WellService _wellService;
        public WellController(WellService service)

        {
            _wellService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWellViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var wellDTO = await _wellService.CreateWell(body, user);
            return Created($"wells/{wellDTO.Id}", wellDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var wellsDTO = await _wellService.GetWells();
            return Ok(wellsDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var wellDTO = await _wellService.GetWellById(id);
            return Ok(wellDTO);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWellViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var wellDTO = await _wellService.UpdateWell(body, id, user);
            return Ok(wellDTO);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            await _wellService.DeleteWell(id, user);
            return NoContent();
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var wellDTO = await _wellService.RestoreWell(id, user);
            return Ok(wellDTO);
        }

        [HttpGet("{id:Guid}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var wellHistories = await _wellService.GetWellHistory(id);

            return Ok(wellHistories);
        }
    }
}
