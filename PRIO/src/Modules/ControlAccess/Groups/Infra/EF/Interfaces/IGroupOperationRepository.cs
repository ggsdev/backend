using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Interfaces
{
    public interface IGroupOperationRepository
    {
        Task<List<GroupOperation>> GetGroupOperationsByGroupId(Guid groupId);
        void UpdateGroupOperations(List<GroupOperation> groupOperations);
    }
}
