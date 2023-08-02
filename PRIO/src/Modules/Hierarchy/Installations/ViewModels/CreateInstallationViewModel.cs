using PRIO.src.Shared.Utils.Validators;
using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Installations.ViewModels
{
    public class CreateInstallationViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "ClusterId is required.")]
        public Guid ClusterId { get; set; }
        [Required(ErrorMessage = "UepCod is required")]
        [StringLength(60, ErrorMessage = "UepCod cannot exceed 60 characters.")]
        public string? UepCod { get; set; }
        [Required(ErrorMessage = "UepName is required")]
        [StringLength(60, ErrorMessage = "UepName cannot exceed 60 characters.")]
        public string? UepName { get; set; }
        [Required(ErrorMessage = "CodInstallationAnp is required")]
        [StringLength(60, ErrorMessage = "CodInstallationAnp cannot exceed 60 characters.")]
        public string? CodInstallationAnp { get; set; }
        [DecimalPrecision(10, isRequired: false)]
        public decimal? GasSafetyBurnVolume { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
