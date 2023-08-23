using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Models
{
    public class ZoneProduction : BaseModel
    {
        public Guid ProductionId { get; set; }
        public Guid ZoneId { get; set; }
        public decimal GasProductionInZone { get; set; }
        public decimal WaterProductionInZone { get; set; }
        public decimal OilProductionInZone { get; set; }
        public List<ReservoirProduction>? ReservoirProductions { get; set; }
    }
}
