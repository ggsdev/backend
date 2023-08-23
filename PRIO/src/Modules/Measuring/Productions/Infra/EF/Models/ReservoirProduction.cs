using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Models
{
    public class ReservoirProduction : BaseModel
    {
        public Guid ProductionId { get; set; }
        public Guid ReservoirId { get; set; }
        public decimal GasProductionInReservoir { get; set; }
        public decimal WaterProductionInReservoir { get; set; }
        public decimal OilProductionInReservoir { get; set; }
        public List<CompletionProduction>? CompletionProductions { get; set; }
        public ZoneProduction? WellProduction { get; set; }
    }
}
