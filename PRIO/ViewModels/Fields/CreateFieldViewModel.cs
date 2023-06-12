using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Fields
{
    public class CreateFieldViewModel
    {
        [Required(ErrorMessage = "Name is a required field.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Code is a required field.")]
        public string? CodField { get; set; }
        public string? Basin { get; set; }
        public string? State { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; } = true;
        [Required(ErrorMessage = "InstallationId is a required field.")]
        public Guid InstallationId { get; set; }

    }
}
