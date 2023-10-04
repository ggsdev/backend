using PRIO.src.Modules.PI.Infra.EF.Models;

namespace PRIO.src.Modules.PI.Dtos
{
    public class ValueDTO
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public AttributeDTO Attribute { get; set; }
        public List<WellsValues>? WellsValues { get; set; }
    }
}
