using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.PI.Infra.EF.Models
{
    public class Value : BaseModel
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public Attribute Attribute { get; set; }
        public List<WellsValues>? WellsValues { get; set; }
    }
}
