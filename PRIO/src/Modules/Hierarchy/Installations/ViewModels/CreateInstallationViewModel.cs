using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Installations.ViewModels
{
    public class CreateInstallationViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(120, ErrorMessage = "Name cannot exceed 120 characters.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "ClusterId is required.")]
        public Guid ClusterId { get; set; }
        [Required(ErrorMessage = "UepCod is required")]
        [StringLength(120, ErrorMessage = "UepCod cannot exceed 120 characters.")]
        public string? UepCod { get; set; }
        [Required(ErrorMessage = "CodInstallation is required")]
        [StringLength(120, ErrorMessage = "CodInstallation cannot exceed 120 characters.")]
        public string? CodInstallation { get; set; }
        public double? GasSafetyBurnVolume { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
