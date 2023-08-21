using PRIO.src.Modules.FileImport.XLSX.BTPS.Dtos;
using PRIO.src.Modules.Measuring.Productions.Dtos;

namespace PRIO.src.Modules.Measuring.WellAppropriations.Infra.Dtos
{
    public class CreateWellAppropriationDto
    {
        public Guid Id { get; set; }
        public decimal ProductionInWell { get; set; }
        public decimal ProductionAsPercentageOfField { get; set; }
        public decimal ProductionAsPercentageOfInstallation { get; set; }
        public BTPDataDTO BtpData { get; set; }
        public ProductionDto Production { get; set; }
    }
}
