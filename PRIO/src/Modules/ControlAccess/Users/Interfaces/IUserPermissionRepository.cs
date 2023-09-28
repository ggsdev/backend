using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Interfaces
{
    public interface IUserPermissionRepository
    {
        Task AddUserPermission(UserPermission userPermission);
        Task<List<UserPermission>> GetUserPermissionsByUserId(Guid userId);
        Task<List<UserPermission>> GetUserPermissionsByGroupId();
        Task<UserPermission> GetUserPermissionByMenuNameAndUserId(string? menuName, Guid userId);
        void UpdateUserPermissions(List<UserPermission> userPermissions);
        Task RemoveUserPermissions(List<UserPermission> userPermissions);
        Task<List<UserPermission>> GetUserPermissionsByGroupId(Guid groupId);

    }
}
