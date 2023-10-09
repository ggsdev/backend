namespace PRIO.src.Modules.Balance.Injection.Dtos
{
    public class InjectionDto
    {
        public Guid InjectionId { get; set; }
        public bool Status { get; set; }
        public string Uep { get; set; } = string.Empty;
        public string Installation { get; set; } = string.Empty;
        public string Field { get; set; } = string.Empty;
        public double InjectedWater { get; set; }
        public double GasLift { get; set; }
        public string InjectionDate { get; set; } = string.Empty;
    }
}

