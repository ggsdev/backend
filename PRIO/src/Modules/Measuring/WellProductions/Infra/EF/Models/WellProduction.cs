using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models
{
    public class WellProduction : BaseModel
    {
        public decimal ProductionGasInWell { get; set; }
        public decimal ProductionWaterInWell { get; set; }
        public decimal ProductionOilInWell { get; set; }
        public decimal ProductionGasAsPercentageOfField { get; set; }
        public decimal ProductionOilAsPercentageOfField { get; set; }
        public decimal ProductionWaterAsPercentageOfField { get; set; }
        public decimal ProductionGasAsPercentageOfInstallation { get; set; }
        public decimal ProductionOilAsPercentageOfInstallation { get; set; }
        public decimal ProductionWaterAsPercentageOfInstallation { get; set; }
        public Guid WellId { get; set; }
        public BTPData BtpData { get; set; }
        public Production Production { get; set; }

        public FieldProduction FieldProduction { get; set; }
        public List<CompletionProduction>? CompletionProductions { get; set; }
    }
}
