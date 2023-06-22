using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Repositories
{
    public class GroupPermissionRepository : IGroupPermissionRepository
    {
        private readonly DataContext _context;

        public GroupPermissionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GroupPermission>> GetGroupPermissionsByGroupId(Guid groupId)
        {
            return await _context.GroupPermissions
                .Include(x => x.Operations)
                .Include(x => x.Menu)
                .Include(x => x.Group)
                .Where(x => x.Group.Id == groupId)
                .ToListAsync();
        }
        public void UpdateGroupPermissions(List<GroupPermission> groupPermissions)
        {
            _context.GroupPermissions.UpdateRange(groupPermissions);
        }
    }
}
