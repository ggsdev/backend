using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models
{
    public class GasVolumeCalculation : BaseModel
    {
        public Installation Installation { get; set; }
        public List<HPFlare> HPFlares { get; set; } = new();
        public List<LPFlare> LPFlares { get; set; } = new();
        public List<HighPressureGas> HighPressureGases { get; set; } = new();
        public List<LowPressureGas> LowPressureGases { get; set; } = new();
        public List<ExportGas> ExportGases { get; set; } = new();
        public List<ImportGas> ImportGases { get; set; } = new();
        public List<AssistanceGas> AssistanceGases { get; set; } = new();
        public List<PilotGas> PilotGases { get; set; } = new();
        public List<PurgeGas> PurgeGases { get; set; } = new();
    }
}
