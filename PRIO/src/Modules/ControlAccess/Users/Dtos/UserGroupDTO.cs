using PRIO.src.Modules.ControlAccess.Groups.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Dtos
{
    public class UserGroupDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public List<UserPermissionParentDTO>? UserPermissions { get; set; }
        public Session? Session { get; set; }
        public GroupDTO? Group { get; set; }
    }
}