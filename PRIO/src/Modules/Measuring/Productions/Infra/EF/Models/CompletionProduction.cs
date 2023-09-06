using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Models
{
    public class CompletionProduction : BaseModel
    {
        public Guid ProductionId { get; set; }
        public Guid CompletionId { get; set; }
        public decimal GasProductionInCompletion { get; set; }
        public decimal WaterProductionInCompletion { get; set; }
        public decimal OilProductionInCompletion { get; set; }
        public WellProductions.Infra.EF.Models.WellProduction? WellAllocation { get; set; }
        public ReservoirProduction? ReservoirProduction { get; set; }
    }
}
