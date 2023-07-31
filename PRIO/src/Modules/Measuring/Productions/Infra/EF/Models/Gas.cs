using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Models
{
    public class Gas : BaseModel
    {
        public bool StatusGas { get; set; } = false;
        public decimal TotalGas { get; set; }
        public decimal ExportedGas { get; set; }
        public decimal ImportedGas { get; set; }
        public decimal BurntGas { get; set; }
        public decimal FuelGas { get; set; }
        public Production Production { get; set; }

    }
}
