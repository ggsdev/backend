using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Reservoirs.ViewModels
{
    public class UpdateReservoirViewModel
    {
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")]
        public string? Name { get; set; }
        [StringLength(60, ErrorMessage = "CodReservoir cannot exceed 60 characters.")]
        public string? Description { get; set; }
        public Guid? ZoneId { get; set; }
    }
}
