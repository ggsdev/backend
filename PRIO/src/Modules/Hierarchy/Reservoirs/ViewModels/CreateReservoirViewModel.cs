using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Reservoirs.ViewModels
{
    public class CreateReservoirViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")]
        public string? Name { get; set; }
        [StringLength(60, ErrorMessage = "CodReservoir cannot exceed 60 characters.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Zone id is required")]
        public Guid? ZoneId { get; set; }
        public bool? IsActive { get; set; }
    }
}
