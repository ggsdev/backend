using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Balance.Injection.ViewModels
{
    public class CreateDailyWaterInjectionViewModel
    {
        [Required(ErrorMessage = "FieldId é obrigatório")]
        public Guid? FieldId { get; set; } = null!;
        [Required(ErrorMessage = "FIRS(Fator de Injeção para Recuperação Secundária) é obrigatório")]
        public double? FIRS { get; set; } = null!;
        public List<AssignedValuesViewModel> AssignedWaterValues { get; set; } = new();
    }
    public class CreateDailyGasInjectionViewModel
    {
        [Required(ErrorMessage = "FieldId é obrigatório")]
        public Guid? FieldId { get; set; } = null!;
        public List<AssignedGasValuesViewModel> AssignedGasValues { get; set; } = new();

    }
    public class AssignedValuesViewModel
    {
        public Guid? WellInjectionId { get; set; }
        public double? AssignedValue { get; set; }
        public Guid? WFLInjectionId { get; set; }
        public double? AssignedWFLValue { get; set; }
    }

    public class AssignedGasValuesViewModel
    {
        public Guid? WellInjectionId { get; set; }
        public double? AssignedValue { get; set; }
        public Guid? GFLInjectionId { get; set; }
        public double? AssignedGFLValue { get; set; }
    }
}
