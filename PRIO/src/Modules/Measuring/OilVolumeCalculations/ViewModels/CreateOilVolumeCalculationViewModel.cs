namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.ViewModels
{
    public class CreateOilVolumeCalculationViewModel
    {

        public Guid InstallationId { get; set; }
        public List<CreateSectionViewModel>? sections { get; set; }
        public List<CreateDrainVolumeViewModel>? TOGs { get; set; }
        public List<CreateDrainVolumeViewModel>? Drains { get; set; }
        public List<CreateDorViewModel>? DORs { get; set; }

    }
}
