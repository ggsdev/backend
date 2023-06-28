using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Completions.ViewModels
{
    public class UpdateCompletionViewModel
    {
        [StringLength(8, ErrorMessage = "CodCompletion cannot exceed 8 characters.")]
        public string? CodCompletion { get; set; }
        public string? Description { get; set; }
        public Guid? ReservoirId { get; set; }
        public Guid? WellId { get; set; }
    }
}
