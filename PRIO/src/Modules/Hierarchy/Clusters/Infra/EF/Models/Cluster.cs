using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models
{
    public class Cluster : BaseModel
    {
        public string? Name { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public List<Installation>? Installations { get; set; }
    }
}
