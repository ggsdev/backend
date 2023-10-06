using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Injection.Infra.EF.Models
{
    public class InjectionWaterGasField
    {
        public Guid Id { get; set; }
        public double AmountWater { get; set; }
        public double AmountGasLift { get; set; }
        public DateTime MeasurementAt { get; set; }
        public List<InjectionWaterWell> WellsWaterInjections { get; set; }
        public FieldsBalance BalanceField { get; set; }
        public List<InjectionGasWell> WellsGasInjections { get; set; }
        public Field Field { get; set; }
    }
}
