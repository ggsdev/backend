using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Models
{
    public class Production
    {
        public Guid Id { get; set; }
        public List<Measurement> Measurements { get; set; } = new();
        public DateTime MeasuredAt { get; set; }
        public DateTime CalculatedImportedAt { get; set; } = DateTime.UtcNow.AddHours(-3);

        public Oil? Oil { get; set; }
        public GasLinear? GasLinear { get; set; }
        public GasDiferencial? GasDiferencial { get; set; }

        //public Water? Water { get; set; }
        public User CalculatedImportedBy { get; set; }
        public bool StatusProduction { get; set; } = false;
        public decimal TotalProduction { get; set; }
    }
}
