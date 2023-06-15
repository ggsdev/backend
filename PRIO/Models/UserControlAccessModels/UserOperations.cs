using PRIO.Models.Operations;

namespace PRIO.Models.UserControlAccessModels
{
    public class UserOperation
    {

        public Guid Id { get; set; }
        public string? OperationName { get; set; }
        public UserPermission? UserPermission { get; set; }
        public GlobalOperation? GlobalOperation { get; set; }
    }
}
