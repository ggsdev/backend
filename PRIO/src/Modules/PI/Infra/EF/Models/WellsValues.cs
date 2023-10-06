using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;

namespace PRIO.src.Modules.PI.Infra.EF.Models
{
    public class WellsValues
    {
        public Guid Id { get; set; }
        public Well Well { get; set; }
        public Value Value { get; set; }
        public InjectionWaterWell InjectionWaterWell { get; set; }
        public InjectionGasWell InjectionGasWell { get; set; }
    }
}
