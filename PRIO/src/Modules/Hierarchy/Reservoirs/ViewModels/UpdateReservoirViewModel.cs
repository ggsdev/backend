using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Reservoirs.ViewModels
{
    public class UpdateReservoirViewModel
    {
        [StringLength(120, ErrorMessage = "Name cannot exceed 120 characters.")]
        public string? Name { get; set; }
        [StringLength(8, ErrorMessage = "CodReservoir cannot exceed 8 characters.")]
        public string? CodReservoir { get; set; }
        public string? Description { get; set; }
        public Guid? ZoneId { get; set; }
    }
}
