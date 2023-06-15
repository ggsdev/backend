using PRIO.Models.BaseModels;
using PRIO.Models.UserControlAccessModels;

namespace PRIO.Models.HierarchyModels
{
    public class Cluster : BaseModel
    {
        public string? Name { get; set; }
        public string? CodCluster { get; set; }
        public User? User { get; set; }
        public List<Installation>? Installations { get; set; }
    }
}
