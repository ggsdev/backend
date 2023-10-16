namespace PRIO.src.Modules.Balance.Injection.Dtos
{
    namespace PRIO.src.Modules.Balance.Injection.Dtos
    {
        public class FieldInjectionDto
        {
            public string Uep { get; set; }
            public string Installation { get; set; }

            public bool Status { get; set; }
            public string DateInjection { get; set; }
            public double TotalGasLift { get; set; }
            public double TotalWaterInjected { get; set; }

            public GasLiftInjectedDto GasLiftFields { get; set; }
            public WaterInjectedDto WaterInjectedFields { get; set; }

        }
    }

}
