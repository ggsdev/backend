using PRIO.src.Modules.Measuring.MeasuringPoints.Dtos;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Dtos
{
    public class DrainVolumeWithEquipmentDTO
    {
        public Guid Id { get; set; }
        public MeasuringPointWithoutInstallationDTO? MeasuringPoint { get; set; }
    }
}
