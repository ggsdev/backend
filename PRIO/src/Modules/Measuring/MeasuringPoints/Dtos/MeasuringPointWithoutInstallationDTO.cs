namespace PRIO.src.Modules.Measuring.MeasuringPoints.Dtos
{
    public class MeasuringPointWithoutInstallationDTO
    {
        public string? Id { get; set; }
        public string? DinamicLocalMeasuringPoint { get; set; }
        public string? TagPointMeasuring { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Description { get; set; }
    }
}
