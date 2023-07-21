namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.ViewModels
{
    public class CreateOilVolumeCalculationViewModel
    {

        public string? UEPCode { get; set; }
        public List<CreateSectionViewModel>? Sections { get; set; }
        public List<CreateTOGRecoverOilViewModel>? TOGs { get; set; }
        public List<CreateDrainVolumeViewModel>? Drains { get; set; }
        public List<CreateDorViewModel>? DORs { get; set; }

    }
}
