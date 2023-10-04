using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models
{
    public class ProductivityIndex : BaseModel
    {
        public double Value { get; set; }
        public bool IsOperating { get; set; }
        public ManualWellConfiguration ManualWellConfiguration { get; set; }
    }
}
