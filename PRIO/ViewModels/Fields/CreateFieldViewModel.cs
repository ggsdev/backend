using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Fields
{
    public class CreateFieldViewModel
    {
        [Required(ErrorMessage = "Field name is a required field.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Field code is a required field.")]
        public string CodField { get; set; } = string.Empty;
        public string Basin { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public bool? IsActive { get; set; } = true;
        public string? Description { get; set; }
        [Required]
        public Guid ClusterId { get; set; }

    }
}
