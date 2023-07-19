using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.ViewModels
{
    public class CreateGasVolumeCalculationViewModel
    {
        //public Installation Installation { get; set; }
        [Required(ErrorMessage = "LocalPoint is required")]
        public string LocalPoint { get; set; }
        [Required(ErrorMessage = "TagMeasuringPoint is required")]
        public string TagMeasuringPoint { get; set; }
        [Required(ErrorMessage = "Applicable is required")]
        public bool Applicable { get; set; }
    }
}
