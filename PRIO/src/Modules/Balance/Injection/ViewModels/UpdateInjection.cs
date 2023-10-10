using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Balance.Injection.ViewModels
{
    public class UpdateInjectionViewModel
    {
        [Required(ErrorMessage = "FieldId é obrigatório")]
        public Guid? FieldId { get; set; } = null!;
        public double? FIRS { get; set; }
        public List<AssignedValuesViewModel> AssignedValues { get; set; } = new();
    }
}
