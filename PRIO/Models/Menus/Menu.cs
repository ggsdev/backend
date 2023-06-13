using PRIO.Models.BaseModels;
using PRIO.Models.Groups.GroupsMenus;

namespace PRIO.Models.Menus
{
    public class Menu : BaseModel
    {
        public string? Name { get; set; }
        public string? Route { get; set; }
        public string? Icon { get; set; }
        public string? Parent { get; set; }
        public string? Children { get; set; }
        public List<GroupMenu>? GroupMenus { get; set; }
    }
}
