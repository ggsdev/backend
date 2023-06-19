using PRIO.DTOS.UserDTOS;
using PRIO.Models.UserControlAccessModels;

namespace PRIO.DTOS.ControlAccessDTOS
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