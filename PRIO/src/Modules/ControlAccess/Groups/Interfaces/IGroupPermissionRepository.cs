using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Groups.Interfaces
{
    public interface IGroupPermissionRepository
    {
        Task<List<GroupPermission>> GetGroupPermissionsByGroupId(Guid groupId);
        void UpdateGroupPermissions(List<GroupPermission> groupPermissions);
        void UpdateGroupPermission(GroupPermission groupPermissions);
        Task<List<GroupPermission>> GetBasicGroupPermissionsByGroupId(Guid groupId);
        Task RemoveGroupPermissions(List<GroupPermission> groupPermissions);
        Task<GroupPermission> GetGroupPermissionById(Guid? id);
        Task<GroupPermission> GetGroupPermissionByMenuIdAndGroupId(Guid menuId, Guid groupId);
        Task<GroupPermission> GetGroupPermissionByMenuIdAndGroupName(Guid menuId, string groupName);

        Task AddAsync(GroupPermission groupPermission);

    }
}
