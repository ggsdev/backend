namespace PRIO.src.Modules.ControlAccess.Users.Dtos
{
    public class UserPermissionChildrenDTO
    {
        public string? MenuName { get; set; }
        public string? MenuRoute { get; set; }
        public string? MenuIcon { get; set; }
        public string? MenuOrder { get; set; }
        public bool? hasParent { get; set; }
        public bool? hasChildren { get; set; }
        public List<UserOperationsDTO>? UserOperation { get; set; }
    }
}
