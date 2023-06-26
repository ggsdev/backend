namespace PRIO.src.Modules.ControlAccess.Groups.Dtos
{
    public class GroupWithMenusDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<GroupPermissionsDTO>? GroupPermissions { get; set; }
    }
}
