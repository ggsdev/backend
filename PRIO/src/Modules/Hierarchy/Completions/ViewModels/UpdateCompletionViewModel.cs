using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Completions.ViewModels
{
    public class UpdateCompletionViewModel
    {
        [StringLength(60, ErrorMessage = "CodCompletion cannot exceed 60 characters.")]
        public string? CodCompletion { get; set; }
        public string? Description { get; set; }
        public Guid? ReservoirId { get; set; }
        public Guid? WellId { get; set; }
    }
}
