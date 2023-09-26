using PRIO.src.Modules.ControlAccess.Groups.Dtos;

namespace PRIO.src.Modules.ControlAccess.Users.Dtos
{
    public class UserWithPermissionsDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public List<UserPermissionParentDTO>? UserPermissions { get; set; }
        public List<InstallationAccessDTO>? InstallationsAccess { get; set; }
        public GroupDTO? Group { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
