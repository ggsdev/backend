using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models
{
    public class GroupPermission
    {
        public Guid? Id { get; set; }
        public Group? Group { get; set; }
        public string? GroupName { get; set; }
        public Menu? Menu { get; set; }
        public string? MenuName { get; set; }
        public string? MenuRoute { get; set; }
        public string? MenuIcon { get; set; }
        public string? MenuOrder { get; set; }
        public bool? hasChildren { get; set; }
        public bool? hasParent { get; set; }
        public List<UserPermission>? Permissions { get; set; }
        public List<GroupOperation?>? Operations { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
