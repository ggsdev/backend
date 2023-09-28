using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Groups.Interfaces;
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

        public async Task RemoveGroupPermissions(List<GroupPermission> groupPermissions)
        {
            _context.RemoveRange(groupPermissions);
            await _context.SaveChangesAsync();
        }
        public async Task AddAsync(GroupPermission groupPermission)
        {
            await _context.AddAsync(groupPermission);
        }
        public void UpdateGroupPermissions(List<GroupPermission> groupPermissions)
        {
            _context.GroupPermissions.UpdateRange(groupPermissions);
        }
        public void UpdateGroupPermission(GroupPermission groupPermissions)
        {
            _context.GroupPermissions.UpdateRange(groupPermissions);
        }
        public async Task<List<GroupPermission>> GetBasicGroupPermissionsByGroupId(Guid groupId)
        {
            return await _context.GroupPermissions
                .Include(x => x.Group)
                .Where(x => x.Group.Id == groupId)
                .ToListAsync();
        }
        public async Task<GroupPermission> GetGroupPermissionById(Guid? id)
        {
            return await _context.GroupPermissions.Include(x => x.Group)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
        }
        public async Task<GroupPermission> GetGroupPermissionByMenuIdAndGroupId(Guid menuId, Guid groupId)
        {
            return await _context.GroupPermissions
                    .Include(x => x.Menu)
                    .Include(x => x.Group)
                    .Where(x => x.Menu.Id == menuId)
                    .Where(x => x.Group.Id == groupId)
                    .FirstOrDefaultAsync();
        }
        public async Task<GroupPermission> GetGroupPermissionByMenuIdAndGroupName(Guid menuId, string groupName)
        {
            return await _context.GroupPermissions
                    .Include(x => x.Menu)
                    .Include(x => x.Group)
                    .Where(x => x.Menu.Id == menuId)
                    .Where(x => x.Group.Name == groupName)
                    .FirstOrDefaultAsync();
        }

    }
}
