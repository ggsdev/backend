using PRIO.src.Modules.Hierarchy.Installations.Dtos;

namespace PRIO.src.Modules.Measuring.MeasuringPoints.Dtos
{
    public class MeasuringPointDTO
    {
        public string? Id { get; set; }
        public string? DinamicLocalMeasuringPoint { get; set; }
        public string? TagPointMeasuring { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
        public InstallationWithoutClusterDTO? Installation { get; set; }
    }
}
