using PRIO.Models.Menus;
using PRIO.Models.Permissions;

namespace PRIO.Models.Groups.GroupsMenus
{
    public class GroupMenu
    {
        public Guid Id { get; set; }
        public Group? Group { get; set; }
        public Menu? Menu { get; set; }
        public List<Permission>? Permissions { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
