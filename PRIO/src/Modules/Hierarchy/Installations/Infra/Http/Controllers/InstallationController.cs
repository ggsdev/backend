using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Installations.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Hierarchy.Installations.Infra.Http.Controllers
{
    [ApiController]
    [Route("installations")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class InstallationController : ControllerBase
    {
        private readonly InstallationService _installationService;
        public InstallationController(InstallationService service)
        {
            _installationService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInstallationViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var installationDTO = await _installationService.CreateInstallation(body, user);

            return Created($"installations/{installationDTO.Id}", installationDTO);
        }
        //[HttpPost("fr")]
        //public async Task<IActionResult> ApplyFR([FromBody] CreateFRsFieldsViewModel body)
        //{
        //    if (HttpContext.Items["User"] is not User user)
        //        return Unauthorized(new ErrorResponseDTO
        //        {
        //            Message = "User not identified, please login first"
        //        });

        //    var frsDTO = await _installationService.ApplyFR(body, user);

        //    return Created($"installations/{frsDTO}", frsDTO);
        //}

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var installationsDTO = await _installationService.GetInstallations();
            return Ok(installationsDTO);
        }
        [HttpGet("fr/{installationId}")]
        public async Task<IActionResult> GetFR([FromRoute] Guid installationId)
        {
            var installationsDTO = await _installationService.GetFRsField(installationId);
            return Ok(installationsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var installationDTO = await _installationService.GetInstallationById(id);
            return Ok(installationDTO);
        }

        [HttpGet("ueps")]
        public async Task<IActionResult> GetUEPs()
        {
            var installationsDTO = await _installationService.GetUEPs();
            return Ok(installationsDTO);
        }
        [HttpGet("ueps/create")]
        public async Task<IActionResult> GetUEPsCreate([FromQuery] string table)
        {
            var installationsDTO = await _installationService.GetUEPsCreate(table);
            return Ok(installationsDTO);
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateInstallationViewModel body)
        {
            if (!Guid.TryParse(id.ToString(), out var guidValue))
            {
                return BadRequest("Invalid GUID format in route param");
            }

            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var installationDTO = await _installationService.UpdateInstallation(body, id, user);
            return Ok(installationDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            await _installationService.DeleteInstallation(id, user);

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

            var installationDTO = await _installationService.RestoreInstallation(id, user);
            return Ok(installationDTO);
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {

            var installationHistories = await _installationService
                .GetInstallationHistory(id);

            return Ok(installationHistories);
        }
    }
}