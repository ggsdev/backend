using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Reservoirs
{
    public class CreateReservoirViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        public string? CodReservoir { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Zone id is required")]
        public Guid? ZoneId { get; set; }
    }
}
