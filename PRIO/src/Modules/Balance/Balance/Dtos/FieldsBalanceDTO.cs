namespace PRIO.src.Modules.Balance.Balance.Dtos
{
    public class FieldsBalanceDTO
    {
        public Guid? Id { get; set; }
        public Guid? FieldId { get; set; }
        public DateTime MeasurementAt { get; set; }
        public string? FieldName { get; set; }
        public double? FIRS { get; set; }
        public decimal TotalWaterProduced { get; set; }
        public decimal? TotalWaterInjected { get; set; }
        public decimal? TotalWaterInjectedRS { get; set; }
        public decimal? TotalWaterDisposal { get; set; }
        public decimal? TotalWaterReceived { get; set; }
        public decimal? TotalWaterCaptured { get; set; }
        public decimal? DischargedSurface { get; set; }
        public decimal? TotalWaterTransferred { get; set; }
        public bool IsParameterized { get; set; }
    }
}
