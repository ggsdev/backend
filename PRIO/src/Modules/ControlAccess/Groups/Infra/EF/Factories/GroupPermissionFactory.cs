using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Factories
{
    public class GroupPermissionFactory
    {
        public GroupPermission CreateGroupPermission(string menuIcon, string menuName, string menuOrder, string menuRoute, Menu menuParent, string groupName, List<Menu> menuChildren, Group group)
        {
            var groupPermissionParentId = Guid.NewGuid();
            return new GroupPermission
            {
                Id = groupPermissionParentId,
                MenuIcon = menuIcon,
                MenuName = menuName,
                MenuOrder = menuOrder,
                MenuRoute = menuRoute,
                GroupName = groupName,
                CreatedAt = DateTime.UtcNow.AddHours(-3),
                Group = group,
                Menu = menuParent,
                hasChildren = menuChildren.Count == 0 ? false : true,
                hasParent = menuParent.Parent is null ? false : true,
            };
        }
    }
}
