namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.ViewModels
{
    public class UpdateGasVolumeCalculationViewModel
    {
        public class CreateGasVolumeCalculationViewModel
        {
            public List<AssistanceGasViewModel> AssistanceGases { get; set; }
            public List<ExportGasViewModel> ExportGases { get; set; }
            public List<HighPressureGasViewModel> HighPressureGases { get; set; }
            public List<HPFlareViewModel> HPFlares { get; set; }
            public List<ImportGasViewModel> ImportGases { get; set; }
            public List<LowPressureGasViewModel> LowPressureGases { get; set; }
            public List<LPFlareViewModel> LPFlares { get; set; }
            public List<PilotGasViewModel> PilotGases { get; set; }
            public List<PurgeGasViewModel> PurgeGases { get; set; }
        }
    }
}
