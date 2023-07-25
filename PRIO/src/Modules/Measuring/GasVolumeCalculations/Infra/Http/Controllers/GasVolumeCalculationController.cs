using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.Http.Services;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.ViewModels;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.Http.Controllers
{
    [ApiController]
    [Route("calculation/gas")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class GasVolumeCalculationController : ControllerBase
    {
        private readonly GasVolumeCalculationService _service;

        public GasVolumeCalculationController(GasVolumeCalculationService service)
        {

            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGasVolumeCalculationViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var gasCalculation = await _service.CreateGasCalculaton(body, user);
            return Created($"calculation/gas/{gasCalculation.Id}", gasCalculation);
        }

        [HttpGet("{installationId}")]
        public async Task<IActionResult> Get([FromRoute] Guid installationId)
        {

            var gasCalculation = await _service.GetGasCalculationByInstallationId(installationId);
            return Ok(gasCalculation);
        }


        [HttpGet("{installationId}/equation")]
        public async Task<IActionResult> GetEquation([FromRoute] Guid installationId)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var gasEquation = await _service.GetGasEquationByInstallationId(installationId);
            return Ok(gasEquation);
        }
    }
}
