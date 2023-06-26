using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models
{
    public class UserPermission
    {
        public Guid? Id { get; set; }
        public Guid? GroupId { get; set; }
        public string? GroupName { get; set; }
        public Guid? MenuId { get; set; }
        public string? MenuName { get; set; }
        public string? MenuRoute { get; set; }
        public string? MenuIcon { get; set; }
        public string? MenuOrder { get; set; }
        public bool? hasChildren { get; set; }
        public bool? hasParent { get; set; }
        public GroupPermission? GroupMenu { get; set; }
        public List<UserOperation>? UserOperation { get; set; }
        public User? User { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
