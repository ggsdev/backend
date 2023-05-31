using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Zones
{
    public class CreateZoneViewModel
    {
        [Required(ErrorMessage = "Zone code is required")]
        public string CodZone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Field id is required")]
        public Guid? FieldId { get; set; }
        public string? Description { get; set; }
    }
}
