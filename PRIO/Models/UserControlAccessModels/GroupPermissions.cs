namespace PRIO.Models.UserControlAccessModels
{
    public class GroupPermissions
    {
        public Guid? Id { get; set; }
        public Group? Group { get; set; }
        public Menu? Menu { get; set; }
        public List<UserPermission>? Permissions { get; set; }
        public List<Operation>? Operations { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
