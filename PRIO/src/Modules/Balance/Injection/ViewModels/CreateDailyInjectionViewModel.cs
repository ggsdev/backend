﻿using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Balance.Injection.ViewModels
{
    public class CreateDailyInjectionViewModel
    {
        [Required(ErrorMessage = "FieldId é obrigatório")]
        public Guid? FieldId { get; set; } = null!;
        [Required(ErrorMessage = "FIRS(Fator de Injeção para Recuperação Secundária) é obrigatório")]
        public double? FIRS { get; set; } = null!;
        public List<AssignedValuesViewModel> AssignedValues { get; set; } = new();
        public List<AssignedGasValuesViewModel> AssignedGasValues { get; set; } = new();
    }

    public class AssignedValuesViewModel
    {
        public double? AssignedValue { get; set; }
        public Guid? WellInjectionId { get; set; }
    }

    public class AssignedGasValuesViewModel
    {
        public double? AssignedValue { get; set; }
        public Guid? WellInjectionId { get; set; }
    }
}
