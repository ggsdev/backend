using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.WellEvents.ViewModels
{
    public class CreateClosingEventViewModel
    {
        [Required(ErrorMessage = "StartDate é obrigatório")]
        public string StartDate { get; set; }
        [Required(ErrorMessage = "EventRelatedId é obrigatório")]
        public Guid EventRelatedId { get; set; }
        [Required(ErrorMessage = "SystemRelated é obrigatório")]
        public string SystemRelated { get; set; }
        [Required(ErrorMessage = "Reason é obrigatório")]
        public string Reason { get; set; }
        public string? StatusAnp { get; set; }
        public string? StateAnp { get; set; }
        public List<WellIndividualEventViewModel> Wells { get; set; }
    }


    public class WellIndividualEventViewModel
    {
        public Guid WellId { get; set; }
    }
}
