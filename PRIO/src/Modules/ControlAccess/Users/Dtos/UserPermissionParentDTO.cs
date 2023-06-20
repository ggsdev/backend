namespace PRIO.src.Modules.ControlAccess.Users.Dtos
{
    public class UserPermissionParentDTO
    {
        public string? MenuName { get; set; }
        public string? MenuRoute { get; set; }
        public string? MenuIcon { get; set; }
        public string? MenuOrder { get; set; }
        public bool? hasParent { get; set; }
        public bool? hasChildren { get; set; }
        public List<UserPermissionChildrenDTO>? Children { get; set; }
        public List<UserOperationsDTO>? UserOperation { get; set; }
    }
}
