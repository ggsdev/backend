using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models
{
    public class Group : BaseModel
    {
        public string? Name { get; set; }
        public List<GroupPermission>? GroupPermissions { get; set; }
        public List<User>? User { get; set; }
    }
}
