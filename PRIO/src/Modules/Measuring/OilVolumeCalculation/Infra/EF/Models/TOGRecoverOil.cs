using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculation.Infra.EF.Models
{
    public class TOGRecoveredOil
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public MeasuringEquipment? Equipment { get; set; }
    }
}
