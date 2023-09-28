using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Menus.Interfaces
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetMenusAsync();
        Task<Menu> GetByMenuId(Guid menuId);
        Task<List<Menu>> GetChildrensByOrder(string orderString);
    }
}
