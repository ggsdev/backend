using PRIO.src.Modules.Measuring.WellAppropriations.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Models
{
    public class FieldProduction : BaseModel
    {
        public decimal GasProductionInField { get; set; }
        public decimal WaterProductionInField { get; set; }
        public decimal OilProductionInField { get; set; }
        public List<WellAppropriation> WellAppropriations { get; set; }
    }
}
