using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Repositories
{
    public class GroupOperationRepository : IGroupOperationRepository
    {
        private readonly DataContext _context;

        public GroupOperationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GroupOperation>> GetGroupOperationsByGroupId(Guid groupId)
        {
            return await _context.GroupOperations
                .Include(x => x.GroupPermission)
                .ThenInclude(x => x.Group)
                .Where(x => x.GroupPermission.Group.Id == groupId)
                .ToListAsync();
        }
        public void UpdateGroupOperations(List<GroupOperation> groupOperations)
        {
            _context.GroupOperations.UpdateRange(groupOperations);
        }
    }
}
