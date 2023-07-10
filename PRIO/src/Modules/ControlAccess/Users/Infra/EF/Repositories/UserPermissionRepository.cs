using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Repositories
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        private readonly DataContext _context;

        public UserPermissionRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddUserPermission(UserPermission userPermission)
        {
            await _context.AddAsync(userPermission);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserPermission>> GetUserPermissionsByUserId(Guid userId)
        {
            return await _context.UserPermissions
                .Include(x => x.User)
                .Where(x => x.User.Id == userId)
                .ToListAsync();
        }
        public async Task<UserPermission> GetUserPermissionByMenuNameAndUserId(string? menuName, Guid userId)
        {
            return await _context
                        .UserPermissions.Include(x => x.User)
                        .Where(x => x.MenuName == menuName)
                        .Where(x => x.User.Id == userId)
                        .FirstOrDefaultAsync();
        }

        public async Task<List<UserPermission>> GetUserPermissionsByGroupId()
        {
            return await _context.UserPermissions.ToListAsync();
        }
        public void UpdateUserPermissions(List<UserPermission> userPermissions)
        {
            _context.UserPermissions.UpdateRange(userPermissions);
        }

        public async Task RemoveUserPermissions(List<UserPermission> userPermissions)
        {
            _context.RemoveRange(userPermissions);
            await _context.SaveChangesAsync();
        }
        public async Task<List<UserPermission>> GetUserPermissionsByGroupId(Guid groupId)
        {
            return await _context.UserPermissions
                .Where(x => x.GroupId == groupId)
                .ToListAsync();
        }
    }
}
