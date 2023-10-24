using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.Balance.Injection.Infra.Http.Services;
using PRIO.src.Modules.Balance.Injection.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;
using System.Globalization;

namespace PRIO.src.Modules.Balance.Injection.Infra.Http.Controllers
{
    [ApiController]
    [Route("injections")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class InjectionController : ControllerBase
    {
        private readonly InjectionService _service;

        public InjectionController(InjectionService injectionService)
        {
            _service = injectionService;
        }

        [HttpPost("water")]
        public async Task<IActionResult> CreateWaterInjection(CreateDailyWaterInjectionViewModel body, string dateInjection)
        {
            if (!DateTime.TryParseExact(dateInjection, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                throw new BadRequestException("O formato da data deve ser dd-MM-yyyy");

            var user = HttpContext.Items["User"] as User;

            var data = await _service.CreateWaterDailyInjection(body, date, user!);

            return Created($"injections/water/{data.FieldInjectionId}", data);
        }

        [HttpPost("gas")]
        public async Task<IActionResult> CreateGasInjection(CreateDailyGasInjectionViewModel body, string dateInjection)
        {
            if (!DateTime.TryParseExact(dateInjection, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                throw new BadRequestException("O formato da data deve ser dd-MM-yyyy");

            var user = HttpContext.Items["User"] as User;

            var data = await _service.CreateGasDailyInjection(body, date, user!);

            return Created($"injections/water/{data.FieldInjectionId}", data);
        }

        [HttpGet("installation/{id}")]
        public async Task<IActionResult> GetInjectionByInstallationId(Guid id)
        {
            var data = await _service.GetInjectionByInstallationId(id);

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetInjectionByDate(string dateInjection, Guid installationId)
        {
            if (!DateTime.TryParseExact(dateInjection, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                throw new BadRequestException("O formato da data deve ser dd-MM-yyyy");

            var data = await _service.GetDailyInjectionTags(date, installationId);

            return Ok(data);
        }

        [HttpGet("{fieldInjectionId}")]
        public async Task<IActionResult> GetInjectionByFieldInjectionId(Guid fieldInjectionId)
        {
            var data = await _service.GetInjectionByFieldInjectionId(fieldInjectionId);

            return Ok(data);
        }


        [HttpPatch("water/{fieldInjectionId}")]
        public async Task<IActionResult> UpdateWaterInjection(UpdateWaterInjectionViewModel body, Guid fieldInjectionId)
        {
            var user = HttpContext.Items["User"] as User;

            var data = await _service.UpdateWaterInjection(body, fieldInjectionId, user!);

            return Ok(data);
        }

        [HttpPatch("gas/{fieldInjectionId}")]
        public async Task<IActionResult> UpdateGasInjection(UpdateGasInjectionViewModel body, Guid fieldInjectionId)
        {
            var user = HttpContext.Items["User"] as User;

            var data = await _service.UpdateGasInjection(body, fieldInjectionId, user!);

            return Ok(data);
        }

        [HttpPatch("{fieldInjectionId}/status")]
        public async Task<IActionResult> UpdateStatus(Guid fieldInjectionId)
        {
            await _service.UpdateInjectionStatus(fieldInjectionId);

            return NoContent();
        }

    }
}
