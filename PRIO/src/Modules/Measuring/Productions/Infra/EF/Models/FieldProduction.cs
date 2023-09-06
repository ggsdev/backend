using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Models
{
    public class FieldProduction : BaseModel
    {
        public decimal GasProductionInField { get; set; }
        public decimal WaterProductionInField { get; set; }
        public decimal OilProductionInField { get; set; }
        public List<WellProductions.Infra.EF.Models.WellProduction> WellProductions { get; set; }
        public Guid ProductionId { get; set; }
        public Guid FieldId { get; set; }
    }
}
