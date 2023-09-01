namespace PRIO.src.Modules.Measuring.Productions.Dtos
{
    public class DetailedGasBurnedDto
    {
        public Guid GasId { get; set; }
        public decimal LimitOperacionalBurn { get; set; }
        public decimal ScheduledStopBurn { get; set; }
        public decimal ForCommissioningBurn { get; set; }
        public decimal VentedGas { get; set; }
        public decimal WellTestBurn { get; set; }
        public decimal EmergencialBurn { get; set; }
        public decimal OthersBurn { get; set; }
    }
}
