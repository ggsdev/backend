using PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models
{
    public class UserOperation
    {

        public Guid Id { get; set; }
        public string? OperationName { get; set; }
        public string? GroupName { get; set; }
        public UserPermission? UserPermission { get; set; }
        public GlobalOperation? GlobalOperation { get; set; }
    }
}
