using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Installations.ViewModels
{
    public class CreateInstallationViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(120, ErrorMessage = "Name cannot exceed 120 characters.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "ClusterId is required.")]
        public Guid? ClusterId { get; set; }
        [StringLength(120, ErrorMessage = "CodInstallationUep cannot exceed 120 characters.")]
        public string? CodInstallationUep { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
