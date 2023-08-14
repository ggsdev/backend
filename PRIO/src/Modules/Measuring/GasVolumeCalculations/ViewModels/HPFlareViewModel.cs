using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.ViewModels
{
    public class HPFlareViewModel
    {
        [Required(ErrorMessage = "MeasuringPointId is required")]
        public Guid MeasuringPointId { get; set; }
        [Required(ErrorMessage = "StaticMeasuringPointName is required")]
        public string StaticMeasuringPointName { get; set; }
        [Required(ErrorMessage = "IsApplicable is required")]
        public bool IsApplicable { get; set; }

    }
}
