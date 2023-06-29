using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models
{
    public class GasVolumeCalculation : BaseModel
    {
        public string? HPFlare { get; set; }
        public string? LPFlare { get; set; }
        public string? HighPressureFuelGas { get; set; }
        public string? LowPressureFuelGas { get; set; }
        public string? ExportGas1 { get; set; }
        public string? ExportGas2 { get; set; }
        public string? ExportGas3 { get; set; }
        public string? ImportGas1 { get; set; }
        public string? ImportGas2 { get; set; }
        public string? ImportGas3 { get; set; }
        public string? AssistanceGas { get; set; }
        public string? PilotGas { get; set; }
        public string? PurgeGas { get; set; }
    }
}
