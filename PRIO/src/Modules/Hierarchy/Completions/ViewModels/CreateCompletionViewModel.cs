using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Completions.ViewModels
{
    public class CreateCompletionViewModel
    {
        [StringLength(8, ErrorMessage = "CodCluster cannot exceed 8 characters.")]
        public string? CodCompletion { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "ReservoirId is required")]
        public Guid? ReservoirId { get; set; }
        [Required(ErrorMessage = "WellId is required")]
        public Guid? WellId { get; set; }
        public bool? IsActive { get; set; }
    }
}
