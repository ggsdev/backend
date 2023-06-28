using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Installations.ViewModels
{
    public class UpdateInstallationViewModel
    {
        [StringLength(120, ErrorMessage = "Name cannot exceed 120 characters.")]
        public string? Name { get; set; }
        public Guid? ClusterId { get; set; }
        [StringLength(120, ErrorMessage = "UepCod cannot exceed 120 characters.")]
        public string? UepCod { get; set; }
        [StringLength(120, ErrorMessage = "UepName cannot exceed 120 characters.")]
        public string? UepName { get; set; }
        [StringLength(120, ErrorMessage = "CodInstallationAnp cannot exceed 120 characters.")]
        public string? CodInstallationAnp { get; set; }
        public double? GasSafetyBurnVolume { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
