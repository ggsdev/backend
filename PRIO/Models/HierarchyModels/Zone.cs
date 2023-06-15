using PRIO.Models.BaseModels;
using PRIO.Models.UserControlAccessModels;

namespace PRIO.Models.HierarchyModels
{
    public class Zone : BaseModel
    {
        public string? CodZone { get; set; }
        public User? User { get; set; }
        public Field? Field { get; set; }
        public List<Reservoir>? Reservoirs { get; set; }
    }
}
