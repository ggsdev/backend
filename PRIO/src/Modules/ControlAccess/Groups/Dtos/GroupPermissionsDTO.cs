using PRIO.src.Modules.ControlAccess.Menus.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Dtos;

namespace PRIO.src.Modules.ControlAccess.Groups.Dtos
{
    public class GroupPermissionsDTO
    {
        public Guid? Id { get; set; }
        public GroupDTO? Group { get; set; }
        public string? GroupName { get; set; }
        public MenuDTO? Menu { get; set; }
        public string? MenuName { get; set; }
        public string? MenuRoute { get; set; }
        public string? MenuIcon { get; set; }
        public string? MenuOrder { get; set; }
        public bool? hasChildren { get; set; }
        public bool? hasParent { get; set; }
        public List<UserGroupOperationDTO>? Operations { get; set; }
    }
}
