using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Fields.ViewModels
{
    public class UpdateFieldViewModel
    {
        [StringLength(120, ErrorMessage = "Name cannot exceed 120 characters.")]
        public string? Name { get; set; }
        [StringLength(10, ErrorMessage = "Code cannot exceed 10 characters.")]
        public string? CodField { get; set; }
        [StringLength(120, ErrorMessage = "Basin cannot exceed 120 characters.")]
        public string? Basin { get; set; }
        [StringLength(120, ErrorMessage = "State cannot exceed 120 characters.")]
        public string? State { get; set; }
        [StringLength(120, ErrorMessage = "Location cannot exceed 120 characters.")]
        public string? Location { get; set; }
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
        public Guid? InstallationId { get; set; }
    }
}
