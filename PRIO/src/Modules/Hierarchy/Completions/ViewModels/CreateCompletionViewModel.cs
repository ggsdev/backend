using PRIO.src.Shared.Utils.Validators;
using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Completions.ViewModels
{
    public class CreateCompletionViewModel
    {
        public string? Description { get; set; }
        [DecimalPrecision(5)]
        public decimal? AllocationReservoir { get; set; } = 1;
        [Required(ErrorMessage = "ReservoirId is required")]
        public Guid? ReservoirId { get; set; }
        [Required(ErrorMessage = "WellId is required")]
        public Guid? WellId { get; set; }
        public bool? IsActive { get; set; }
    }
}
