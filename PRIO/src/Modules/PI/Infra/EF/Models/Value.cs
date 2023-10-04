using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.PI.Infra.EF.Models
{
    public class Value : BaseModel
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Description { get; set; }
        public Attribute Attribute { get; set; }
        public List<WellsValues>? WellsValues { get; set; }
    }


    public class ValueJson
    {
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
        public string UnitsAbbreviation { get; set; } = string.Empty;
        public bool Good { get; set; }
        public bool Questionable { get; set; }
        public bool Substituted { get; set; }
        public bool Annotated { get; set; }
    }
}
