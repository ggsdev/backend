using PRIO.Models.BaseModels;
using PRIO.Models.UserControlAccessModels;

namespace PRIO.Models.HierarchyModels
{
    public class Installation : BaseModel
    {
        public string? Name { get; set; }
        public string? CodInstallationUep { get; set; }
        public string? Cod { get; set; }
        public User? User { get; set; }
        public Cluster? Cluster { get; set; }
        public List<MeasuringEquipment>? MeasuringEquipments { get; set; }
        public List<Field>? Fields { get; set; }
    }
}
