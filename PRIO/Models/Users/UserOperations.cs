using PRIO.Models.Operations;
using PRIO.Models.Permissions;

namespace PRIO.Models.Users
{
    public class UserOperations
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public UserPermissions? UserPermission { get; set; }
        public GlobalOperation? GlobalOperation { get; set; }
    }
}
