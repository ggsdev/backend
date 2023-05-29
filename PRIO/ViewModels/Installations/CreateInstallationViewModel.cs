using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Installations
{
    public class CreateInstallationViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        public string? CodInstallation { get; set; }

        [Required(ErrorMessage = "FieldId is required.")]
        public Guid FieldId { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }
    }
}
