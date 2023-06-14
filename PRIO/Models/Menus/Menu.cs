using PRIO.Models.BaseModels;
using PRIO.Models.Groups;

namespace PRIO.Models.Menus
{
    public class Menu : BaseModel
    {
        public string? Name { get; set; }
        public string? Route { get; set; }
        public string? Icon { get; set; }
        public string? Order { get; set; }
        public Menu? Parent { get; set; }
        public List<Menu>? Children { get; set; }
        public List<GroupPermissions>? GroupPermissions { get; set; }
    }
}
