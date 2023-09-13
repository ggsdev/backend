using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.WellEvents.ViewModels
{
    public class UpdateReasonViewModel
    {
        [Required(ErrorMessage = "SystemRelated é obrigatório")]
        public string SystemRelated { get; set; }
    }
}
