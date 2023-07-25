using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.MeasuringPoints.ViewModels
{
    public class CreateMeasuringPointViewModel
    {
        [Required(ErrorMessage = "TagPointMeasuring is required")]
        public string TagPointMeasuring { get; set; }
        [Required(ErrorMessage = "DinamicLocalMeasuringPoint is required")]
        public string DinamicLocalMeasuringPoint { get; set; }
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "InstallationId is required")]
        public Guid InstallationId { get; set; }
    }
}
