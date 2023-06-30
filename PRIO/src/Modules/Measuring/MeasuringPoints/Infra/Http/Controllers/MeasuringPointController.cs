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
    }
}
