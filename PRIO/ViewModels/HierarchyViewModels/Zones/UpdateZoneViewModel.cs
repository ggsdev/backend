using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.HierarchyViewModels.Zones
{
    public class UpdateZoneViewModel
    {
        [StringLength(120, ErrorMessage = "CodZone cannot exceed 120 characters.")]
        public string? CodZone { get; set; }
        public string? Description { get; set; }
        public Guid? FieldId { get; set; }
    }
}
