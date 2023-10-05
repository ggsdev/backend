using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Balance.Infra.EF.Models
{
    public class FieldsBalance : BaseModel
    {
        public DateTime MeasurementAt { get; set; }
        public double? FIRS { get; set; }
        public double? TotalWaterProduced { get; set; }
        public double? TotalWaterInjected { get; set; }
        public double? TOtalWaterInjectedRS { get; set; }
        public double? TotalWaterDisposal { get; set; }
        public double? TotalWaterReceived { get; set; }
        public double? TotalWaterCaptured { get; set; }
        public double? DischargedSurface { get; set; }
        public double? TotalWaterTransferred { get; set; }
        public bool IsParameterized { get; set; }
        public InjectionWaterField? InjectionWaterField { get; set; }
        public InstallationsBalance? installationBalance { get; set; }
        public FieldProduction FieldProduction { get; set; }
    }
}
