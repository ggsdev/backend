using PRIO.src.Modules.Measuring.Equipments.Dtos;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Dtos
{
    public class DORWithEquipmentDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public MeasuringEquipmentWithoutInstallationDTO? Equipment { get; set; }
    }
}
