using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Injection.Infra.EF.Models
{
    public class InjectionWaterGasField
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public DateTime MeasurementAt { get; set; }
        public List<InjectionWaterWell> WellsWaterInjections { get; set; }
        public FieldsBalance BalanceField { get; set; }
        public List<InjectionGasWell> WellsGasInjections { get; set; }

    }
}
