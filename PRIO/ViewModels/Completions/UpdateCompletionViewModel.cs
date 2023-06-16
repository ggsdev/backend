using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Completions
{
    public class UpdateCompletionViewModel
    {
        [StringLength(10, ErrorMessage = "CodCluster cannot exceed 10 characters.")]
        public string? CodCompletion { get; set; }
        public string? Description { get; set; }
        public Guid? ReservoirId { get; set; }
        public Guid? WellId { get; set; }
    }
}
