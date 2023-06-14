using PRIO.Models.Groups;

namespace PRIO.Models.Operations
{
    public class GroupOperations
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public GroupPermissions? GroupPermission { get; set; }
        public GlobalOperation? GlobalOperation { get; set; }

    }
}
