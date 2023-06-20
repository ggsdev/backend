using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Installations.ViewModels
{
    public class UpdateInstallationViewModel
    {
        [StringLength(120, ErrorMessage = "Name cannot exceed 120 characters.")]
        public string? Name { get; set; }
        public Guid? ClusterId { get; set; }
        [StringLength(120, ErrorMessage = "CodInstallationUep cannot exceed 120 characters.")]
        public string? CodInstallationUep { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
