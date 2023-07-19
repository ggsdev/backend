using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models
{
    public class LPFlare : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public GasVolumeCalculation GasVolumeCalculation { get; set; }
        public MeasuringPoint MeasuringPoint { get; set; }

    }
}
