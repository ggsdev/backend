using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces
{
    public interface IUserOperationRepository
    {
        Task<List<UserOperation>> GetUserOperationsByUserId(Guid userId);
        Task<List<UserOperation>> GetUserOperationsByGroupId(Guid groupId);
        Task RemoveUserOperations(List<UserOperation> userOperations);
        void UpdateUserOperations(List<UserOperation> userOperations);
    }
}
