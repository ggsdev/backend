using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models
{
    public class DrainVolume : BaseModel
    {
        public Guid Id { get; set; }
        public string StaticLocalMeasuringPoint { get; set; }
        public bool IsApplicable { get; set; }
        public OilVolumeCalculation? OilVolumeCalculation { get; set; }
        public MeasuringPoint? MeasuringPoint { get; set; }
    }
}
