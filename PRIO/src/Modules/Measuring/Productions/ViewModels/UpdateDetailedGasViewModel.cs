namespace PRIO.src.Modules.Measuring.Productions.ViewModels
{
    public class UpdateDetailedGasViewModel
    {
        public decimal LimitOperacionalBurn { get; set; }
        public decimal ScheduledStopBurn { get; set; }
        public decimal ForCommissioningBurn { get; set; }
        public decimal VentedGas { get; set; }
        public decimal WellTestBurn { get; set; }
        public decimal EmergencialBurn { get; set; }
        public decimal OthersBurn { get; set; }
    }
}
