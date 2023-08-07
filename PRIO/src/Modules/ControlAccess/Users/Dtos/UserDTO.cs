using PRIO.src.Modules.ControlAccess.Groups.Dtos;

namespace PRIO.src.Modules.ControlAccess.Users.Dtos
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public GroupDTO? Group { get; set; }
    }

    public class LoginDTO
    {
        public string? Token { get; set; }
    }
    public class ProfileDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public List<UserPermissionParentDTO>? UserPermissions { get; set; }
        public GroupDTO? Group { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}