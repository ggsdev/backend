using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Completions.ViewModels
{
    public class CreateCompletionViewModel
    {
        [StringLength(60, ErrorMessage = "CodCompletion cannot exceed 60 characters.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "ReservoirId is required")]
        public Guid? ReservoirId { get; set; }
        [Required(ErrorMessage = "WellId is required")]
        public Guid? WellId { get; set; }
        public bool? IsActive { get; set; }
    }
}
