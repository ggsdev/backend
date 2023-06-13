using PRIO.Models.BaseModels;
using PRIO.Models.Groups.GroupsMenus;
using PRIO.Models.Users;

namespace PRIO.Models.Groups
{
    public class Group : BaseModel
    {
        public string? Name { get; set; }
        public List<GroupMenu>? GroupMenus { get; set; }
        public List<User>? Users { get; set; }
    }
}
