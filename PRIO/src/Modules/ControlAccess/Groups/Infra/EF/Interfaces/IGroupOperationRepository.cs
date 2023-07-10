using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Interfaces
{
    public interface IGroupOperationRepository
    {
        Task<List<GroupOperation>> GetGroupOperationsByGroupId(Guid groupId);
        Task<GroupOperation> GetGroupOperationsByMenuIdAndGroupPermissionId(Guid operationId, Guid? groupPermissionId);
        Task<GroupOperation> GetGroupOperationsByOperationIdAndGroupName(Guid OperationId, string groupName);
        void UpdateGroupOperations(List<GroupOperation> groupOperations);
        void SaveChangesAsync();
    }
}
