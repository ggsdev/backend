using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Zones.ViewModels
{
    public class CreateZoneViewModel
    {
        [Required(ErrorMessage = "Zone code is required")]
        [StringLength(60, ErrorMessage = "CodZone cannot exceed 60 characters.")]
        public string? CodZone { get; set; }

        [Required(ErrorMessage = "Field id is required")]
        public Guid? FieldId { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
