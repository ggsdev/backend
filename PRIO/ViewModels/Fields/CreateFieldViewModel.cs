using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Fields
{
    public class CreateFieldViewModel
    {
        [Required(ErrorMessage = "Field name is a required field.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Field code is a required field.")]
        public string? CodField { get; set; }
        public string? Basin { get; set; }
        public string? State { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; } = true;
        [Required]
        public Guid InstallationId { get; set; }

    }
}
