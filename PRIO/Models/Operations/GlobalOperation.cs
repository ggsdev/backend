using PRIO.Models.Users;

namespace PRIO.Models.Operations
{
    public class GlobalOperation
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<GroupOperations>? GroupOperations { get; set; }
        public List<UserOperations>? UserOperations { get; set; }
    }
}
