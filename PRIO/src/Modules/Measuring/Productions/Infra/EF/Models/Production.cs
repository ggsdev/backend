using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Comments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Models
{
    public class Production : BaseModel
    {
        //public Guid Id { get; set; }
        public List<Measurement> Measurements { get; set; } = new();
        public DateTime MeasuredAt { get; set; }
        public DateTime CalculatedImportedAt { get; set; } = DateTime.UtcNow.AddHours(-3);
        public bool CanDetailGasBurned { get; set; } = true;
        public Oil? Oil { get; set; }
        public GasLinear? GasLinear { get; set; }
        public Gas? Gas { get; set; }
        public GasDiferencial? GasDiferencial { get; set; }
        public bool IsCalculated { get; set; }
        public Water? Water { get; set; }
        public User CalculatedImportedBy { get; set; }
        public string StatusProduction { get; set; } = "aberto";
        public decimal TotalProduction { get; set; }
        public Installation Installation { get; set; }
        public CommentInProduction? Comment { get; set; }
        public List<FieldFR>? FieldsFR { get; set; }
        public List<NFSMsProductions>? NFSMs { get; set; }
        public List<WellProductions.Infra.EF.Models.WellProduction>? WellProductions { get; set; }
    }
}
