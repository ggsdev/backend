using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Interfaces
{
    public interface IGroupPermissionRepository
    {
        Task<List<GroupPermission>> GetGroupPermissionsByGroupId(Guid groupId);
        void UpdateGroupPermissions(List<GroupPermission> groupPermissions);
    }
}
