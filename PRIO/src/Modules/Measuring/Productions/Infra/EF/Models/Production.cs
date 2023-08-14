using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Models
{
    public class Production
    {
        public Guid Id { get; set; }
        public List<Measurement> Measurements { get; set; } = new();
        public DateTime MeasuredAt { get; set; }
        public DateTime CalculatedImportedAt { get; set; } = DateTime.UtcNow;

        public Oil? Oil { get; set; }
        public GasLinear? GasLinear { get; set; }
        public Gas? Gas { get; set; }
        public GasDiferencial? GasDiferencial { get; set; }

        //public Water? Water { get; set; }
        public User CalculatedImportedBy { get; set; }
        public bool StatusProduction { get; set; } = false;
        public decimal TotalProduction { get; set; }
        public Installation Installation { get; set; }
        public List<FieldFR>? FieldsFR { get; set; }
        public List<NFSMsProductions>? NFSMs { get; set; }
    }
}
