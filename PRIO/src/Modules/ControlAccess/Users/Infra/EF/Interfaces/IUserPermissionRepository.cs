using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces
{
    public interface IUserPermissionRepository
    {
        Task<List<UserPermission>> GetUserPermissionsByUserId(Guid userId);
        Task<List<UserPermission>> GetUserPermissionsByGroupId();
        void UpdateUserPermissions(List<UserPermission> userPermissions);
        Task RemoveUserPermissions(List<UserPermission> userPermissions);
    }
}
