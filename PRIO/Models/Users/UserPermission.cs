using PRIO.Models.Groups;
using PRIO.Models.Users;

namespace PRIO.Models.Permissions
{
    public class UserPermission
    {
        public Guid Id { get; set; }
        public Guid? GroupId { get; set; }
        public string? GroupName { get; set; }
        public Guid? MenuId { get; set; }
        public string? MenuName { get; set; }
        public string? MenuRoute { get; set; }
        public string? MenuOrder { get; set; }
        public string? MenuIcon { get; set; }
        public GroupPermissions? GroupMenu { get; set; }
        public User? User { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
