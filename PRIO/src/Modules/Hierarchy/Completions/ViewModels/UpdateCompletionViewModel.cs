using PRIO.src.Shared.Utils.Validators;
using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Completions.ViewModels
{
    public class UpdateCompletionViewModel
    {
        [StringLength(60, ErrorMessage = "CodCompletion cannot exceed 60 characters.")]
        public string? Description { get; set; }
        [DecimalPrecision(5)]
        public decimal? AllocationReservoir { get; set; }
        [DecimalPrecision(12, isRequired: false)]
        public decimal? TopOfPerforated { get; set; }
        [DecimalPrecision(12, isRequired: false)]
        public decimal? BaseOfPerforated { get; set; }
        public Guid? ReservoirId { get; set; }
        public Guid? WellId { get; set; }
        public string? Name { get; set; }
    }
}
