using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Wells.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Hierarchy.Wells.Infra.Http.Controllers
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

        [HttpPost("config/{wellId}")]
        public async Task<IActionResult> CreateConfig([FromBody] CreateConfigViewModels body, [FromRoute] Guid wellId)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });
            var wellDTO = await _wellService.CreateConfig(body, wellId, user);
            return Created($"wells/config/{wellDTO.Id}", wellDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });
            var wellsDTO = await _wellService.GetWells(user);
            return Ok(wellsDTO);
        }

        [HttpGet("config/{wellId}")]
        public async Task<IActionResult> GetConfig([FromRoute] Guid wellId)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });
            var wellsConfigDTO = await _wellService.GetWellsConfig(user, wellId);
            return Ok(wellsConfigDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var wellDTO = await _wellService.GetWellById(id);
            return Ok(wellDTO);
        }

        [HttpPatch("{id}")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, [FromHeader] string StatusDate)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            await _wellService.DeleteWell(id, user, StatusDate);
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

            var wellDTO = await _wellService.RestoreWell(id, user);
            return Ok(wellDTO);
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var wellHistories = await _wellService.GetWellHistory(id);

            return Ok(wellHistories);
        }

        //[HttpGet("paginated")]
        //public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 3)
        //{
        //    var requestUrl = Request.GetEncodedUrl();
        //    var paginatedWellsDTO = await _wellService.GetWellsPaginated(pageNumber, pageSize, requestUrl);

        //    return Ok(paginatedWellsDTO);
        //}
    }
}
