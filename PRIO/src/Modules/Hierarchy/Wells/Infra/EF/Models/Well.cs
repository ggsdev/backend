using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models
{
    public class Well : BaseModel
    {
        public string? Name { get; set; }
        public string? WellOperatorName { get; set; }
        public string? CodWellAnp { get; set; }
        public string? CodWell { get; set; }
        public string? CategoryAnp { get; set; }
        public string? CategoryReclassificationAnp { get; set; }
        public string? CategoryOperator { get; set; }
        public bool? StatusOperator { get; set; }
        public string? Type { get; set; }
        public decimal? WaterDepth { get; set; }
        public string? ArtificialLift { get; set; }
        public string? Latitude4C { get; set; }
        public string? Longitude4C { get; set; }
        public string? LatitudeDD { get; set; }
        public string? LongitudeDD { get; set; }
        public string? DatumHorizontal { get; set; }
        public string? TypeBaseCoordinate { get; set; }
        public string? CoordX { get; set; }
        public string? CoordY { get; set; }
        public User? User { get; set; }
        public Field? Field { get; set; }
        public DateTime? InactivatedAt { get; set; }
        public List<Completion>? Completions { get; set; }
        public List<WellTests>? WellTests { get; set; }
        public List<WellEvent> WellEvents { get; set; } = new();
    }
}
