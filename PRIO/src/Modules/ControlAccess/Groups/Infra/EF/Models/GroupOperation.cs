using PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models
{
    public class GroupOperation
    {
        public Guid Id { get; set; }
        public string? OperationName { get; set; }
        public GroupPermission? GroupPermission { get; set; }
        public GlobalOperation? GlobalOperation { get; set; }
    }
}