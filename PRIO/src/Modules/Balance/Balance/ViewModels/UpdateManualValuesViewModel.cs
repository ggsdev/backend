using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Balance.Balance.ViewModels
{
    public class UpdateManualValuesViewModel
    {
        [Required(ErrorMessage = "FieldsBalances é obrigatório")]
        public List<UpdateManualBalanceViewModel> FieldsBalances { get; set; } = null!;


    }

    public class UpdateManualBalanceViewModel
    {
        [Required(ErrorMessage = "FieldBalanceId é obrigatório")]
        public Guid? FieldBalanceId { get; set; }
        public decimal? TotalWaterCaptured { get; set; }
        public decimal? DischargedSurface { get; set; }
        public decimal? TotalWaterTransferred { get; set; }
        public decimal? TotalWaterReceived { get; set; }
    }
}
