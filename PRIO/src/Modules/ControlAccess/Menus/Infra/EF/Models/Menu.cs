using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Models
{
    public class Menu : BaseModel
    {
        public string? Name { get; set; }
        public string? Route { get; set; }
        public string? Icon { get; set; }
        public string? Order { get; set; }
        public Menu? Parent { get; set; }
        public List<Menu>? Children { get; set; }
        public List<GroupPermission>? GroupPermissions { get; set; }
    }
}
