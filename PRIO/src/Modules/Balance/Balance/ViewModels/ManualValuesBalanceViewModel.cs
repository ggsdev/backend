using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Balance.Balance.ViewModels
{
    public class ManualValuesBalanceViewModel
    {
        [Required(ErrorMessage = "UepBalanceId é obrigatório")]
        public Guid? UepBalanceId { get; set; }
        [Required(ErrorMessage = "FieldsBalances é obrigatório")]
        public List<FieldManualBalanceViewModel> FieldsBalances { get; set; } = null!;
    }

    public class FieldManualBalanceViewModel
    {
        [Required(ErrorMessage = "FieldBalanceId é obrigatório")]
        public Guid? FieldBalanceId { get; set; }
        [Required(ErrorMessage = "TotalWaterCaptured é obrigatório")]
        public decimal TotalWaterCaptured { get; set; }
        [Required(ErrorMessage = "DischargedSurface é obrigatório")]
        public decimal DischargedSurface { get; set; }
        [Required(ErrorMessage = "TotalWaterTransferred é obrigatório")]
        public decimal TotalWaterTransferred { get; set; }
        [Required(ErrorMessage = "TotalWaterReceived é obrigatório")]
        public decimal TotalWaterReceived { get; set; }
    }
}
