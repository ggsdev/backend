using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Models
{
    public class Oil : BaseModel
    {
        public bool StatusOil { get; set; } = false;
        public decimal TotalOil { get; set; }
        public Production Production { get; set; }
        public decimal? BswAverage { get; set; }
    }
}
