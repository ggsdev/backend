using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Zones
{
    public class CreateReservoirViewModel
    {
        [Required(ErrorMessage = "Reservoir code is required")]
        public string CodReservoir { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zone id is required")]
        public Guid? ZoneId { get; set; }
    }
}
