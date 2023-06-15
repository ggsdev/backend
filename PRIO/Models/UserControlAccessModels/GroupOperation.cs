using PRIO.Models.Operations;

namespace PRIO.Models.UserControlAccessModels
{
    public class GroupOperation
    {
        public Guid Id { get; set; }
        public string? OperationName { get; set; }
        public GroupPermission? GroupPermission { get; set; }
        public GlobalOperation? GlobalOperation { get; set; }
    }
}