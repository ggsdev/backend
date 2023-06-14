using PRIO.Models.Menus;
using PRIO.Models.Operations;
using PRIO.Models.Permissions;

namespace PRIO.Models.Groups
{
    public class GroupPermissions
    {
        public Guid Id { get; set; }
        public Group? Group { get; set; }
        public Menu? Menu { get; set; }
        public List<UserPermissions>? Permissions { get; set; }
        public List<GroupOperations>? Operations { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
