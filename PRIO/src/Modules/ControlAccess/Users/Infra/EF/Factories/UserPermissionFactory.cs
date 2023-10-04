using PRIO.src.Modules.ControlAccess.Groups.Dtos;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Factories
{
    public class UserPermissionFactory
    {
        public UserPermission CreateUserPermission(GroupPermissionsDTO permission, User userHasGroup, GroupPermission groupPermission)
        {
            var userPermissionId = Guid.NewGuid();
            return new UserPermission
            {
                Id = userPermissionId,
                hasChildren = permission.hasChildren,
                hasParent = permission.hasParent,
                MenuIcon = permission.MenuIcon,
                MenuName = permission.MenuName,
                MenuOrder = permission.MenuOrder,
                MenuRoute = permission.MenuRoute,
                GroupId = permission.Group.Id,
                GroupName = permission.Group.Name,
                MenuId = Guid.Parse(permission.Menu.Id),
                User = userHasGroup,
                GroupMenu = groupPermission,
                CreatedAt = DateTime.UtcNow.AddHours(-3),
            };
        }
    }
}
