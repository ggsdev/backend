using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models
{
    public class FieldFR : BaseModel
    {
        public decimal? FRGas { get; set; }
        public decimal? FROil { get; set; }
        public decimal? FRWater { get; set; }
        public Field Field { get; set; }
        public Production DailyProduction { get; set; }
        public decimal ProductionInField { get; set; }
    }
}
