using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Zones.ViewModels
{
    public class UpdateZoneViewModel
    {
        [StringLength(60, ErrorMessage = "CodZone cannot exceed 60 characters.")]
        public string? CodZone { get; set; }
        public string? Description { get; set; }
        public Guid? FieldId { get; set; }
    }
}
