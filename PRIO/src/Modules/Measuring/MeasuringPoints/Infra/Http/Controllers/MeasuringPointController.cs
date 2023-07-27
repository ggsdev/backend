using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.Http.Services;
using PRIO.src.Modules.Measuring.MeasuringPoints.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Measuring.MeasuringPoints.Infra.Http.Controllers
{
    [ApiController]
    [Route("mpoint")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class MeasuringPointController : ControllerBase
    {
        private readonly MeasuringPointService _measuringPointService;
        public MeasuringPointController(MeasuringPointService service)
        {
            _measuringPointService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMeasuringPointViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var measuringPointDTO = await _measuringPointService.CreateMeasuringPoint(body, user);
            return Created($"mpoint/{measuringPointDTO.Id}", measuringPointDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var measuringPointDTO = await _measuringPointService.ListAll();
            return Ok(measuringPointDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var measuringPointDTO = await _measuringPointService.GetById(id);
            return Ok(measuringPointDTO);
        }

        [HttpGet]
        [Route("installation/{id}")]
        public async Task<IActionResult> GetByInstallationId([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var measuringPointDTO = await _measuringPointService.GetByInstallationId(id);
            return Ok(measuringPointDTO);
        }

        [HttpGet]
        [Route("uep")]
        public async Task<IActionResult> GetByUEPCod([FromQuery] string uepCode)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var measuringPointDTO = await _measuringPointService.GetByUEPCode(uepCode);
            return Ok(measuringPointDTO);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMeasuringPointViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var measuringPointDTO = await _measuringPointService.Update(id, body, user);
            return Ok(measuringPointDTO);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            await _measuringPointService.Delete(id, user);
            return NoContent();
        }

        [HttpPatch]
        [Route("{id}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var measuringPointDTO = await _measuringPointService.Restore(id, user);

            return Ok(measuringPointDTO);
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var measuringPointHistories = await _measuringPointService.GetHistory(id);

            return Ok(measuringPointHistories);
        }
    }
}
