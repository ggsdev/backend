namespace PRIO.Models.UserControlAccessModels
{
    public class GroupPermission
    {
        public Guid? Id { get; set; }
        public Group? Group { get; set; }
        public string? GroupName { get; set; }
        public Menu? Menu { get; set; }
        public string? MenuName { get; set; }
        public string? MenuRoute { get; set; }
        public string? MenuIcon { get; set; }
        public string? MenuOrder { get; set; }
        public bool? hasChildren { get; set; }
        public bool? hasParent { get; set; }
        public List<UserPermission>? Permissions { get; set; }
        public List<GroupOperation>? Operations { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
