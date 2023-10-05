using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Injection.Infra.EF.Models
{
    public class InjectionWaterField
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public DateTime MeasurementAt { get; set; }
        public List<InjectionWaterWell> WellsInjections { get; set; }
        public FieldsBalance BalanceField { get; set; }
    }
}
