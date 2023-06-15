using PRIO.Models.BaseModels;
using PRIO.Models.UserControlAccessModels;

namespace PRIO.Models.HierarchyModels
{
    public class Reservoir : BaseModel
    {
        public string? Name { get; set; }
        public string? CodReservoir { get; set; }
        public User? User { get; set; }
        public Zone? Zone { get; set; }
        public List<Completion>? Completions { get; set; }
    }
}
