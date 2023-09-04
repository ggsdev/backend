using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.WellEvents.ViewModels
{
    public class CreateClosingEventViewModel
    {
        [Required(ErrorMessage = "EventDateAndHour é obrigatório")]
        public string EventDateAndHour { get; set; }

        [Required(ErrorMessage = "SystemRelated é obrigatório")]
        public string SystemRelated { get; set; }

        [Required(ErrorMessage = "Reason é obrigatório")]
        public string Reason { get; set; }

        public string? StatusAnp { get; set; }
        public string? StateAnp { get; set; }

        [Required(ErrorMessage = "Lista de Wells é obrigatória")]
        public List<WellIndividualEventViewModel> Wells { get; set; }

        public string? EventRelatedCode { get; set; }
    }


    public class WellIndividualEventViewModel
    {
        [Required(ErrorMessage = "WellId é obrigatório")]
        public Guid WellId { get; set; }
    }

    public class CreateOpeningEventViewModel
    {
        [Required(ErrorMessage = "EventDateAndHour é obrigatório")]
        public string EventDateAndHour { get; set; }
        [Required(ErrorMessage = "Reason é obrigatório")]
        public string Reason { get; set; }
        public string? StatusAnp { get; set; }
        public string? StateAnp { get; set; }
        [Required(ErrorMessage = "WellId é obrigatório")]
        public Guid WellId { get; set; }

        public string Comment { get; set; }
    }
}
