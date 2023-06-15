using PRIO.Models.BaseModels;
using PRIO.Models.UserControlAccessModels;

namespace PRIO.Models.HierarchyModels
{
    public class Completion : BaseModel
    {
        public string? Name { get; set; }
        public string? CodCompletion { get; set; }
        public Reservoir? Reservoir { get; set; }
        public Well? Well { get; set; }
        public User? User { get; set; }
    }
}
