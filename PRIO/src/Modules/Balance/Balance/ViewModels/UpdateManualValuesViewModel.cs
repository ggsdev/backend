using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Balance.Balance.ViewModels
{
    public class UpdateManualValuesViewModel
    {
        [Required(ErrorMessage = "FieldsBalances é obrigatório")]
        [MinLength(1, ErrorMessage = "FieldsBalances deve ser preenchido")]
        public List<UpdateManualBalanceViewModel>? FieldsBalances { get; set; }
    }

    public class UpdateManualBalanceViewModel
    {
        [Required(ErrorMessage = "FieldBalanceId é obrigatório")]
        public Guid? FieldBalanceId { get; set; }
        [Required(ErrorMessage = "TotalWaterCaptured é obrigatório")]
        public decimal? TotalWaterCaptured { get; set; }
        [Required(ErrorMessage = "DischargedSurface é obrigatório")]
        public decimal? DischargedSurface { get; set; }
        [Required(ErrorMessage = "TotalWaterTransferred é obrigatório")]
        public decimal? TotalWaterTransferred { get; set; }
        [Required(ErrorMessage = "TotalWaterReceived é obrigatório")]
        public decimal? TotalWaterReceived { get; set; }
    }
}
