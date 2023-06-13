using PRIO.Models.Groups.GroupsMenus;
using PRIO.Models.Users;

namespace PRIO.Models.Permissions
{
    public class Permission
    {
        public Guid Id { get; set; }
        public GroupMenu? GroupMenu { get; set; }
        public User? User { get; set; }
        public string? MenuName { get; set; }
        public Guid? GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? MenuRoute { get; set; }
        public Guid? MenuId { get; set; }
        public string? MenuIcon { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
