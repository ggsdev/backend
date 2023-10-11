namespace PRIO.src.Modules.Balance.Injection.Dtos
{
    public class WellSensorDTO
    {
        public Guid Id { get; set; }
        public double AssignedValue { get; set; }
        public DateTime MeasurementAt { get; set; }
        public WellValueDTO? WellValues { get; set; }
    }
}
