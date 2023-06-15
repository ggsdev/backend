using PRIO.Models.UserControlAccessModels;

namespace PRIO.Models.Operations
{
    public class GlobalOperation
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<GroupOperation>? GroupOperations { get; set; }
        public List<UserOperation>? UserOperations { get; set; }
    }
}
