using PRIO.Models.BaseModels;

namespace PRIO.Models.UserControlAccessModels
{
    public class Group : BaseModel
    {
        public string? Name { get; set; }
        public List<GroupPermissions>? GroupPermissions { get; set; }
    }
}
