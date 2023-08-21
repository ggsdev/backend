using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellAppropriations.Infra.EF.Models
{
    public class WellAppropriation : BaseModel
    {
        public decimal ProductionInWell { get; set; }
        public decimal ProductionAsPercentageOfField { get; set; }
        public decimal ProductionAsPercentageOfInstallation { get; set; }
        public BTPData BtpData { get; set; }
        public Production Production { get; set; }
    }
}
