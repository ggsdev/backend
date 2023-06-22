using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Interfaces
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetMenusAsync();
    }
}
