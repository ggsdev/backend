namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.ViewModels
{
    public class CreateDrainVolumeViewModel
    {
        public Guid MeasuringPointId { get; set; }
        public string StaticMeasuringPointName { get; set; }
        public bool IsApplicable { get; set; }
    }
}
