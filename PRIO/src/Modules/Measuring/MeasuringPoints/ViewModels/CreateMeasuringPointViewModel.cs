namespace PRIO.src.Modules.Measuring.MeasuringPoints.ViewModels
{
    public class CreateMeasuringPointViewModel
    {
        public string? TagPointMeasuring { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public Guid InstallationId { get; set; }
    }
}
