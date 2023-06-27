using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculation.Infra.EF.Models
{
    public class Section
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public OilVolumeCalculation? OilVolumeCalculation { get; set; }
        public MeasuringEquipment? Equipment { get; set; }
    }
}
