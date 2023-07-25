using PRIO.src.Modules.Hierarchy.Installations.Dtos;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Dtos
{
    public class GasVolumeCalculationDto
    {
        public Guid Id { get; set; }
        public InstallationWithoutClusterDTO Installation { get; set; }
        public List<HPFlareDto> HPFlares { get; set; }
        public List<LPFlareDto> LPFlares { get; set; }
        public List<HighPressureGasDto> HighPressureGases { get; set; }
        public List<LowPressureGasDto> LowPressureGases { get; set; }
        public List<ExportGasDto> ExportGases { get; set; }
        public List<ImportGasDto> ImportGases { get; set; }
        public List<AssistanceGasDto> AssistanceGases { get; set; }
        public List<PilotGasDto> PilotGases { get; set; }
        public List<PurgeGasDto> PurgeGases { get; set; }
    }
}
