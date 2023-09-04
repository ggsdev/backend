using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.WellEvents.ViewModels
{
    public class CreateReasonViewModel
    {
        [Required(ErrorMessage = "SystemRelated é obrigatório")]
        public string SystemRelated { get; set; }
    }
}
