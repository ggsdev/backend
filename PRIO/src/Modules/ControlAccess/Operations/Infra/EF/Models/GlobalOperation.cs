using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Models
{
    public class GlobalOperation : BaseModel
    {
        public string? Method { get; set; }
        public List<GroupOperation>? GroupOperations { get; set; }
        public List<UserOperation>? UserOperations { get; set; }
    }
}
