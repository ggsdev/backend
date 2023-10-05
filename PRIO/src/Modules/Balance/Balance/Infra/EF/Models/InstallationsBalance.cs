using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Balance.Infra.EF.Models
{
    public class InstallationsBalance : BaseModel
    {
        public DateTime MeasurementAt { get; set; }
        public double? TotalWaterProduced { get; set; }
        public double? TotalWaterInjected { get; set; }
        public double? TOtalWaterInjectedRS { get; set; }
        public double? TotalWaterDisposal { get; set; }
        public double? TotalWaterReceived { get; set; }
        public double? TotalWaterCaptured { get; set; }
        public double? DischargedSurface { get; set; }
        public double? TotalWaterTransferred { get; set; }
        public List<FieldsBalance> BalanceFields { get; set; }
        public UEPsBalance UEPBalance { get; set; }
    }
}
