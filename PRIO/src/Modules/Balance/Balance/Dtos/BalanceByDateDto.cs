namespace PRIO.src.Modules.Balance.Balance.Dtos
{
    public class BalanceByDateDto
    {
        public Guid UepBalanceId { get; set; }
        public bool StatusBalance { get; set; }
        public string DateBalance { get; set; }
        public decimal? TotalWaterProduced { get; set; }
        public decimal? TotalWaterInjected { get; set; }
        public decimal? TotalWaterInjectedRS { get; set; }
        public decimal? TotalWaterDisposal { get; set; }
        public decimal? TotalWaterReceived { get; set; }
        public decimal? TotalWaterCaptured { get; set; }
        public decimal? DischargedSurface { get; set; }
        public decimal? TotalWaterTransferred { get; set; }
        public List<FieldBalanceDto> FieldBalances { get; set; } = new();
    }


    public class FieldBalanceDto
    {
        public Guid FieldBalanceId { get; set; }
        public string DateBalance { get; set; }
        public decimal? TotalWaterProduced { get; set; }
        public decimal? TotalWaterInjected { get; set; }
        public decimal? TotalWaterInjectedRS { get; set; }
        public decimal? TotalWaterDisposal { get; set; }
        public decimal? TotalWaterReceived { get; set; }
        public decimal? TotalWaterCaptured { get; set; }
        public decimal? DischargedSurface { get; set; }
        public decimal? TotalWaterTransferred { get; set; }
    }
}
