using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Repositories
{
    public class UserOperationRepository : IUserOperationRepository
    {
        private readonly DataContext _context;

        public UserOperationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<UserOperation>> GetUserOperationsByUserId(Guid userId)
        {
            return await _context.UserOperations
                .Include(x => x.UserPermission)
                .ThenInclude(x => x.User)
                .Where(x => x.UserPermission.User.Id == userId)
                .ToListAsync();
        }

        public async Task RemoveUserOperations(List<UserOperation> userOperations)
        {
            _context.RemoveRange(userOperations);
            await _context.SaveChangesAsync();
        }
        public async Task<List<UserOperation>> GetUserOperationsByGroupId(Guid groupId)
        {
            return await _context.UserOperations
                .Include(x => x.UserPermission)
                .Where(x => x.UserPermission.GroupId == groupId)
                .ToListAsync();
        }
        public void UpdateUserOperations(List<UserOperation> userOperations)
        {
            _context.UserOperations.UpdateRange(userOperations);
        }
    }
}
