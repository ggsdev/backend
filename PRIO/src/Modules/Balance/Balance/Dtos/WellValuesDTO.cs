namespace PRIO.src.Modules.Balance.Balance.Dtos
{
    public class WellValuesDTO
    {
        public Guid? Id { get; set; }
        public ValueWithInjecctionDTO? Value { get; set; }

        public InjectionWaterWellDTO? InjectionWaterWell { get; set; }
        public InjectionGasWellDTO? InjectionGasWell { get; set; }
        public SensorsDTO? WellSensor { get; set; }
    }
}
