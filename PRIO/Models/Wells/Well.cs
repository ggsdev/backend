using PRIO.Models.BaseModels;
using PRIO.Models.Completions;
using PRIO.Models.Fields;
using PRIO.Models.Users;

namespace PRIO.Models.Wells
{
    public class Well : BaseModel
    {
        public string? Name { get; set; }
        public string? WellOperatorName { get; set; }
        public string CodWellAnp { get; set; } = string.Empty;
        public string? CodWell { get; set; }
        public string? CategoryAnp { get; set; }
        public string? CategoryReclassificationAnp { get; set; }
        public string? CategoryOperator { get; set; }
        public bool? StatusOperator { get; set; }
        public string? Type { get; set; }
        public double? WaterDepth { get; set; }
        public double? TopOfPerforated { get; set; }
        public double? BaseOfPerforated { get; set; }
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
        public Field Field { get; set; }
        public List<Completion>? Completions { get; set; }
    }
}
