using PRIO.src.Shared.Infra.EF.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRIO.src.Modules.PI.Infra.EF.Models
{
    public class Value : BaseModel
    {
        public double? Amount { get; set; }//examinar pra ver se precisar ser string??
        public DateTime Date { get; set; }
        public Attribute Attribute { get; set; }
        [ForeignKey("AttributeId")]
        public Guid AttributeId { get; set; }
        public List<WellsValues>? WellsValues { get; set; }
        public bool IsCaptured { get; set; }
    }


    public class ValueJson
    {
        public double? Value { get; set; }
        public DateTime Timestamp { get; set; }
        public string UnitsAbbreviation { get; set; } = string.Empty;
        public bool Good { get; set; }
        public bool Questionable { get; set; }
        public bool Substituted { get; set; }
        public bool Annotated { get; set; }
        public bool IsCaptured { get; set; }
    }
}
