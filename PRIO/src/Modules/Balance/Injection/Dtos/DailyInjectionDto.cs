namespace PRIO.src.Modules.Balance.Injection.Dtos
{
    public class DailyInjectionDto
    {
        public string Uep { get; set; }
        public string Installation { get; set; }
        public bool Status { get; set; }
        public string DateInjection { get; set; }
        public double TotalGasLift { get; set; }
        public double TotalWaterInjected { get; set; }

        public GasLiftInjectedDto GasLift { get; set; }
        public WaterInjectedDto WaterInjected { get; set; }
    }

    public class GasLiftInjectedDto
    {
        public Guid FieldInjectionId { get; set; }
        public string Field { get; set; }
        public List<ElementGasDto> Parameters { get; set; } = new();
    }

    public class GasValuesDto
    {
        public Guid WellInjectionId { get; set; }
        public string WellName { get; set; }
        public double Volume { get; set; }
        public string Tag { get; set; }
        public string DateRead { get; set; }

    }
    public class ElementGasDto
    {
        public string Parameter { get; set; }
        public List<GasValuesDto> Values { get; set; } = new();

    }

    public class WellWaterInjectedDto
    {
        public Guid WellInjectionId { get; set; }
        public string WellName { get; set; }
        public double? VolumePI { get; set; }
        public double VolumeAssigned { get; set; }
        public string Tag { get; set; }
        public string DateRead { get; set; }
    }

    public class WaterInjectedDto
    {
        public Guid FieldInjectionId { get; set; }
        public string Field { get; set; }
        public double FIRS { get; set; }
        public List<WellWaterInjectedDto> Values { get; set; } = new();
    }

}
