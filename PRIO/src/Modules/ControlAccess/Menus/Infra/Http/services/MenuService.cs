using AutoMapper;
using PRIO.src.Modules.ControlAccess.Menus.Dtos;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Menus.Infra.Http.Services
{
    public class MenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuParentDTO>> Get()
        {
            var menus = await _menuRepository.GetMenusAsync();
            foreach (var menu in menus)
            {
                menu.Children = menu.Children.OrderBy(x => x.Order).ToList();
            }
            var menusDTO = _mapper.Map<List<Menu>, List<MenuParentDTO>>(menus);

            return menusDTO;
        }
    }
}
