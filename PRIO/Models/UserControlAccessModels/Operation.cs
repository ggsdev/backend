namespace PRIO.Models.UserControlAccessModels
{
    public class Operation
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<GroupPermissions>? GroupPermissions { get; set; }
    }
}
