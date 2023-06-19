namespace PRIO.DTOS.ControlAccessDTOS
{
    public class GroupWithMenusDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<GroupPermissionsDTO>? GroupPermissions { get; set; }
    }
}
