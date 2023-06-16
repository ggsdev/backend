using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Fields
{
    public class CreateFieldViewModel
    {
        [Required(ErrorMessage = "Name is a required field.")]
        [StringLength(120, ErrorMessage = "Name cannot exceed 120 characters.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Code is a required field.")]
        [StringLength(10, ErrorMessage = "Code cannot exceed 10 characters.")]
        public string? CodField { get; set; }
        [StringLength(120, ErrorMessage = "Basin cannot exceed 120 characters.")]
        public string? Basin { get; set; }
        [StringLength(120, ErrorMessage = "State cannot exceed 120 characters.")]
        public string? State { get; set; }
        [StringLength(120, ErrorMessage = "Location cannot exceed 120 characters.")]
        public string? Location { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; } = true;
        [Required(ErrorMessage = "InstallationId is a required field.")]
        public Guid InstallationId { get; set; }

    }
}
