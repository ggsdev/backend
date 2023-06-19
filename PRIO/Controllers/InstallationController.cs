using Microsoft.AspNetCore.Mvc;
using PRIO.DTOS.GlobalDTOS;
using PRIO.Filters;
using PRIO.Models.UserControlAccessModels;
using PRIO.Services.HierarchyServices;
using PRIO.ViewModels.HierarchyViewModels.Installations;

namespace PRIO.Controllers
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var installationsDTO = await _installationService.GetInstallations();
            return Ok(installationsDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var installationDTO = await _installationService.GetInstallationById(id);
            return Ok(installationDTO);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateInstallationViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var installationDTO = await _installationService.UpdateInstallation(body, id, user);
            return Ok(installationDTO);
        }

        [HttpDelete("{id:Guid}")]
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

        [HttpPatch("{id:Guid}/restore")]
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

        [HttpGet("{id:Guid}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var installationHistories = await _installationService.GetInstallationHistory(id, user);

            return Ok(installationHistories);
        }
    }
}