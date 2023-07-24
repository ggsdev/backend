using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.ViewModels
{
    public class CreateGasVolumeCalculationViewModel
    {
        [Required(ErrorMessage = "UepCode is required")]
        public string UepCode { get; set; }
        [Required(ErrorMessage = "AssistanceGases are required")]
        public List<AssistanceGasViewModel> AssistanceGases { get; set; }
        [Required(ErrorMessage = "ExportGases are required")]
        public List<ExportGasViewModel> ExportGases { get; set; }
        [Required(ErrorMessage = "HighPresureGases are required")]
        public List<HighPressureGasViewModel> HighPressureGases { get; set; }
        [Required(ErrorMessage = "HPFlares are required")]
        public List<HPFlareViewModel> HPFlares { get; set; }
        [Required(ErrorMessage = "ImportGases are required")]
        public List<ImportGasViewModel> ImportGases { get; set; }
        [Required(ErrorMessage = "LowPressureGases are required")]
        public List<LowPressureGasViewModel> LowPressureGases { get; set; }
        [Required(ErrorMessage = "LPFlares are required")]
        public List<LPFlareViewModel> LPFlares { get; set; }
        [Required(ErrorMessage = "PilotGases are required")]
        public List<PilotGasViewModel> PilotGases { get; set; }
        [Required(ErrorMessage = "PurgeGases are required")]
        public List<PurgeGasViewModel> PurgeGases { get; set; }
    }
}
