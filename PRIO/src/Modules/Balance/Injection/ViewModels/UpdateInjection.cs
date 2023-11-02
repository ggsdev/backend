using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Balance.Injection.ViewModels
{
    public class UpdateWaterInjectionViewModel
    {
        [Required(ErrorMessage = "FieldId é obrigatório")]
        public Guid? FieldId { get; set; } = null!;
        public double? FIRS { get; set; }
        public List<AssignedWaterValuesViewModel> AssignedValues { get; set; } = new();
    }

    public class UpdateGasInjectionViewModel
    {
        [Required(ErrorMessage = "FieldId é obrigatório")]
        public Guid? FieldId { get; set; } = null!;
        public List<AssignedGasValuesViewModel> AssignedValues { get; set; } = new();
    }
}
