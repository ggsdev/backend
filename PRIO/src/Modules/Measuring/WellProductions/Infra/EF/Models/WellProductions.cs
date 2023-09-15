using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models
{
    public class WellProduction : BaseModel
    {
        public decimal ProductionGasInWellM3 { get; set; }
        public decimal ProductionGasInWellSCF { get; set; }
        public decimal ProductionWaterInWellM3 { get; set; }
        public decimal ProductionWaterInWellBBL { get; set; }
        public decimal ProductionOilInWellM3 { get; set; }
        public decimal ProductionOilInWellBBL { get; set; }
        public decimal ProductionGasAsPercentageOfField { get; set; }
        public decimal ProductionOilAsPercentageOfField { get; set; }
        public decimal ProductionWaterAsPercentageOfField { get; set; }
        public decimal ProductionLostOil { get; set; }
        public decimal ProductionLostWater { get; set; }
        public decimal ProductionLostGas { get; set; }
        public decimal ProductionGasAsPercentageOfInstallation { get; set; }
        public decimal ProductionOilAsPercentageOfInstallation { get; set; }
        public decimal? ProductionWaterAsPercentageOfInstallation { get; set; }
        public decimal EfficienceLoss { get; set; }
        public decimal ProportionalDay { get; set; }

        public string Downtime { get; set; }
        public Guid WellId { get; set; }
        public WellTests WellTest { get; set; }
        public Production Production { get; set; }

        public FieldProduction FieldProduction { get; set; }
        public List<CompletionProduction>? CompletionProductions { get; set; }
        public List<WellLosses>? WellLosses { get; set; }
    }
}
