using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Models
{
    public class Production : BaseModel
    {
        public List<Measurement> Measurements { get; set; } = new();
        public DateTime? MeasuredAt { get; set; }
        public DateTime? CalculatedImportedAt { get; set; }
        public bool StatusGas { get; set; } = false;
        public bool StatusOil { get; set; } = false;
        public bool StatusWater { get; set; } = false;
        public User CalculatedImportedBy { get; set; }
        public decimal? TotalOil { get; set; }
        public decimal? TotalGas { get; set; }
        public decimal? TotalWater { get; set; }
    }
}
