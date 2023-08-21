using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Models
{
    public class Water : BaseModel
    {
        public bool StatusWater { get; set; }
        public decimal TotalWater { get; set; }
        public Production? Production { get; set; }

    }
}
