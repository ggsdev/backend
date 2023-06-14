using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Completions
{
    public class CreateCompletionViewModel
    {
        public string? CodCompletion { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "ReservoirId is required")]
        public Guid? ReservoirId { get; set; }
        [Required(ErrorMessage = "WellId is required")]
        public Guid? WellId { get; set; }
        public bool? IsActive { get; set; }
    }
}
