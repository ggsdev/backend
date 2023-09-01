using PRIO.src.Modules.Measuring.WellEvents.EF.Models;

namespace PRIO.src.Modules.Measuring.WellProductions.Infra.Dtos
{
    public class CreateWellLossDTO
    {
        public Guid Id { get; set; }
        public DateTime MeasuredAt { get; set; }
        public decimal Downtime { get; set; }
        public WellEvent Event { get; set; }
    }
}
