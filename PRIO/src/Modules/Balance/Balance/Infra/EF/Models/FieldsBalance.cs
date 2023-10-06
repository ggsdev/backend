using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Balance.Infra.EF.Models
{
    public class FieldsBalance : BaseModel
    {
        public DateTime MeasurementAt { get; set; }
        public double? FIRS { get; set; }
        public decimal TotalWaterProduced { get; set; }
        public decimal? TotalWaterInjected { get; set; }
        public decimal? TotalWaterInjectedRS { get; set; }
        public decimal? TotalWaterDisposal { get; set; }
        public decimal? TotalWaterReceived { get; set; }
        public decimal? TotalWaterCaptured { get; set; }
        public decimal? DischargedSurface { get; set; }
        public decimal? TotalWaterTransferred { get; set; }
        public bool IsParameterized { get; set; }
        public InjectionWaterGasField? InjectionWaterField { get; set; }
        public InstallationsBalance? InstallationBalance { get; set; }
        public FieldProduction FieldProduction { get; set; }
    }
}
