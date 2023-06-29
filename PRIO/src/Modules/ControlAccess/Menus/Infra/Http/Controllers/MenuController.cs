using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Menus.Infra.Http.Services;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.ControlAccess.Menus.Infra.Http.Controllers
{
    [ApiController]
    [Route("menus")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService service)
        {
            _menuService = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var menus = await _menuService.Get();
            return Ok(menus);
        }
    }
}
