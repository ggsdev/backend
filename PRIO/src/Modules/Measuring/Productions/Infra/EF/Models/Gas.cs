using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Models
{
    public class GasLinear : BaseModel
    {
        public bool StatusGas { get; set; } = false;
        public decimal TotalGas { get; set; }
        public decimal ExportedGas { get; set; }
        public decimal ImportedGas { get; set; }
        public decimal BurntGas { get; set; }
        public decimal FuelGas { get; set; }
        public Production Production { get; set; }
        public Gas Gas { get; set; }


    }

    public class GasDiferencial : BaseModel
    {
        public bool StatusGas { get; set; } = false;
        public decimal TotalGas { get; set; }
        public decimal ExportedGas { get; set; }
        public decimal ImportedGas { get; set; }
        public decimal BurntGas { get; set; }
        public decimal FuelGas { get; set; }
        public Production Production { get; set; }
        public Gas Gas { get; set; }

    }

    public class Gas : BaseModel
    {
        public decimal LimitOperacionalBurn { get; set; }
        public decimal ScheduledStopBurn { get; set; }
        public decimal ForCommissioningBurn { get; set; }
        public decimal VentedGas { get; set; }
        public decimal WellTestBurn { get; set; }
        public decimal EmergencialBurn { get; set; }
        public decimal OthersBurn { get; set; }
        public Production Production { get; set; }

        public GasLinear? GasLinear { get; set; }
        public GasDiferencial? GasDiferencial { get; set; }

    }
}
