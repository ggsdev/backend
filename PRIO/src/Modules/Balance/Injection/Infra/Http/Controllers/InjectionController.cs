using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.Balance.Injection.Infra.Http.Services;
using PRIO.src.Modules.Balance.Injection.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Infra.Http.Filters;

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

        [HttpPatch("water/{id}")]
        public async Task<IActionResult> UpdateInjection(UpdateWaterInjectionViewModel body, [FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var data = await _service.UpdateWaterInjection(body, user!);

            return Ok(data);
        }
    }
}
