using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.ViewModels
{
    public class CreateGasVolumeCalculationViewModel
    {
        [Required(ErrorMessage = "UepCode is required")]
        public string UepCode { get; set; }
        public List<AssistanceGasViewModel> AssistanceGases { get; set; } = new();
        public List<ExportGasViewModel> ExportGases { get; set; } = new();
        public List<HighPressureGasViewModel> HighPressureGases { get; set; } = new();
        public List<HPFlareViewModel> HPFlares { get; set; } = new();
        public List<ImportGasViewModel> ImportGases { get; set; } = new();
        public List<LowPressureGasViewModel> LowPressureGases { get; set; } = new();
        public List<LPFlareViewModel> LPFlares { get; set; } = new();
        public List<PilotGasViewModel> PilotGases { get; set; } = new();
        public List<PurgeGasViewModel> PurgeGases { get; set; } = new();
    }
}
