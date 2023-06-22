using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DataContext _context;

        public MenuRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Menu>> GetMenusAsync()
        {
            return await _context.Menus
                .Where(x => x.Parent == null)
                .Include(x => x.Children)
                .OrderBy(x => x.Order)
                .ToListAsync();
        }
    }
}
