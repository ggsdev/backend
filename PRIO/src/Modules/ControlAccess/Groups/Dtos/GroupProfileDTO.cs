namespace PRIO.src.Modules.ControlAccess.Groups.Dtos
{
    public class GroupProfileDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public List<GroupPermissionParentDTO>? GroupPermissions { get; set; }

    }
}
