using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Fields.ViewModels
{
    public class UpdateFieldViewModel
    {
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")]
        public string? Name { get; set; }
        [StringLength(60, ErrorMessage = "Code cannot exceed 60 characters.")]
        public string? CodField { get; set; }
        [StringLength(60, ErrorMessage = "Basin cannot exceed 60 characters.")]
        public string? Basin { get; set; }
        [StringLength(60, ErrorMessage = "State cannot exceed 60 characters.")]
        public string? State { get; set; }
        [StringLength(60, ErrorMessage = "Location cannot exceed 60 characters.")]
        public string? Location { get; set; }
        public string? Description { get; set; }
        public Guid? InstallationId { get; set; }
    }
}
