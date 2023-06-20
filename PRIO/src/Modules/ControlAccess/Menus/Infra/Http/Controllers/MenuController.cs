using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Menus.Dtos;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.ControlAccess.Menus.Infra.Http.Controllers
{
    [ApiController]
    [Route("menus")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class MenuController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MenuController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var menus = await _context.Menus.Where(x => x.Parent == null).Include(x => x.Children).OrderBy(x => x.Order).ToListAsync();
            foreach (var menu in menus)
            {
                menu.Children = menu.Children.OrderBy(x => x.Order).ToList();
            }
            var menusDTO = _mapper.Map<List<Menu>, List<MenuParentDTO>>(menus);

            return Ok(menusDTO);
        }
    }
}
