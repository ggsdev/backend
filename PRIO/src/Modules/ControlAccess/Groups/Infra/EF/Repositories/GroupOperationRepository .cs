using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Groups.Interfaces;
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

        public async Task AddAsync(GroupOperation groupOperation)
        {
            await _context.AddAsync(groupOperation);

        }
        public async Task RemoveGroupOperations(List<GroupOperation> groupOperations)
        {
            _context.RemoveRange(groupOperations);
            await _context.SaveChangesAsync();
        }
        public async Task<List<GroupOperation>> GetGroupOperationsByGroupId(Guid groupId)
        {
            return await _context.GroupOperations
                .Include(x => x.GroupPermission)
                .ThenInclude(x => x.Group)
                .Where(x => x.GroupPermission.Group.Id == groupId)
                .ToListAsync();
        }
        public async Task<GroupOperation> GetGroupOperationsByMenuIdAndGroupPermissionId(Guid operationId, Guid? groupPermissionId)
        {
            return await _context.GroupOperations
                                          .Include(x => x.GlobalOperation!)
                                          .Include(x => x.GroupPermission!)
                                          .Where(x => x.GlobalOperation.Id == operationId)
                                          .Where(x => x.GroupPermission.Id == groupPermissionId)
                                          .FirstOrDefaultAsync();
        }
        public async Task<GroupOperation> GetGroupOperationsByOperationIdAndGroupName(Guid OperationId, string groupName)
        {
            var groupOperation = await _context.GroupOperations
                                              .Include(x => x.GlobalOperation)
                                              .Include(x => x.GroupPermission)
                                              .Where(x => x.GlobalOperation.Id == OperationId)
                                              .Where(x => x.GroupPermission.GroupName == groupName)
                                              .FirstOrDefaultAsync();
            return groupOperation;
        }
        public void UpdateGroupOperations(List<GroupOperation> groupOperations)
        {
            _context.GroupOperations.UpdateRange(groupOperations);
        }
        public async void SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
