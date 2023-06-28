using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;

public class DOR
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public OilVolumeCalculation? OilVolumeCalculation { get; set; }
    public MeasuringEquipment? Equipment { get; set; }
}
