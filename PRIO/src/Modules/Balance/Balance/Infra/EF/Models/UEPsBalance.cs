using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Balance.Infra.EF.Models
{
    public class UEPsBalance : BaseModel
    {
        public DateTime MeasurementAt { get; set; }
        public decimal? TotalWaterProduced { get; set; }
        public decimal? TotalWaterInjected { get; set; }
        public decimal? TotalWaterInjectedRS { get; set; }
        public decimal? TotalWaterDisposal { get; set; }
        public decimal? TotalWaterReceived { get; set; }
        public decimal? TotalWaterCaptured { get; set; }
        public decimal? DischargedSurface { get; set; }
        public decimal? TotalWaterTransferred { get; set; }
        public List<InstallationsBalance> InstallationsBalance { get; set; }
        public Installation? Uep { get; set; }
    }
}
