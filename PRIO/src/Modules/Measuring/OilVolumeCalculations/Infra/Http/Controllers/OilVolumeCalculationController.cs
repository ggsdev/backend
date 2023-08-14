using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Dtos;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.Http.Services;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.ViewModels;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.Http.Controllers
{
    [ApiController]
    [Route("calculation")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class OilVolumeCalculationController : ControllerBase
    {
        private readonly OilVolumeCalculationService _oilVolumeCalculationService;
        public OilVolumeCalculationController(OilVolumeCalculationService oilVolumeCalculationService)
        {
            _oilVolumeCalculationService = oilVolumeCalculationService;
        }

        [HttpGet("oil")]
        public async Task<List<OilVolumeCalculationDTO>> Get()
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var OilVolumeCalculationDTO = await _oilVolumeCalculationService.GetAll();

            return OilVolumeCalculationDTO;
        }
        [HttpPost("oil")]
        public async Task<OilVolumeCalculationDTO?> Create([FromBody] CreateOilVolumeCalculationViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var OilVolumeCalculationDTO = await _oilVolumeCalculationService.CreateOilVolumeCalculation(body, user);

            return OilVolumeCalculationDTO;
        }

        [HttpGet("oil/{installationId}")]
        public async Task<OilVolumeCalculationDTO?> Get([FromRoute] Guid installationId)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var oilVolumeCalculationDTO = await _oilVolumeCalculationService.GetById(installationId);

            return oilVolumeCalculationDTO;
        }

        [HttpGet("oil/{installationId}/equation")]
        public async Task<ActionResult> GetEquation([FromRoute] Guid installationId)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var eq = await _oilVolumeCalculationService.GetEquation(installationId);
            return Ok(eq);
        }

        [HttpPatch("oil/{installationId}")]
        public async Task<object> UpdateOilCalculation([FromRoute] Guid installationId, [FromBody] CreateOilVolumeCalculationViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var oilVolumeCalculationDTO = await _oilVolumeCalculationService.Update(installationId, body);
            return oilVolumeCalculationDTO;
        }

        [HttpPatch("oil/{installationId}/refresh")]
        public async Task<object> Refresh([FromRoute] Guid installationId)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var oilVolumeCalculationDTO = await _oilVolumeCalculationService.Refresh(installationId);
            return oilVolumeCalculationDTO;
        }

        [HttpDelete("oil/{installationId}")]
        public async Task<object> DeleteOilCalculation([FromRoute] Guid installationId, [FromBody] CreateOilVolumeCalculationViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");
            await _oilVolumeCalculationService.Delete(installationId, body);

            return NoContent();
        }
    }
}
