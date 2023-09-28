using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Groups.Interfaces
{
    public interface IGroupOperationRepository
    {
        Task AddAsync(GroupOperation groupOperation);
        Task<List<GroupOperation>> GetGroupOperationsByGroupId(Guid groupId);
        Task<GroupOperation> GetGroupOperationsByMenuIdAndGroupPermissionId(Guid operationId, Guid? groupPermissionId);
        Task<GroupOperation> GetGroupOperationsByOperationIdAndGroupName(Guid OperationId, string groupName);
        Task RemoveGroupOperations(List<GroupOperation> groupOperations);
        void UpdateGroupOperations(List<GroupOperation> groupOperations);
        void SaveChangesAsync();
    }
}
