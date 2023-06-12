using PRIO.Models.BaseModels;
using PRIO.Models.Completions;
using PRIO.Models.Fields;
using PRIO.Models.Users;

namespace PRIO.Models.Wells
{
    public class WellHistory : BaseHistoryModel
    {
        public string? Name { get; set; }
        public string? NameOld { get; set; }
        public string? WellOperatorName { get; set; }
        public string? WellOperatorNameOld { get; set; }
        public string? CodWellAnp { get; set; }
        public string? CodWellAnpOld { get; set; }
        public string? CodWell { get; set; }
        public string? CodWellOld { get; set; }
        public string? CategoryAnp { get; set; }
        public string? CategoryAnpOld { get; set; }
        public string? CategoryReclassificationAnp { get; set; }
        public string? CategoryReclassificationAnpOld { get; set; }
        public string? CategoryOperator { get; set; }
        public string? CategoryOperatorOld { get; set; }
        public bool? StatusOperator { get; set; }
        public bool? StatusOperatorOld { get; set; }
        public string? Type { get; set; }
        public string? TypeOld { get; set; }
        public decimal? WaterDepth { get; set; }
        public decimal? WaterDepthOld { get; set; }
        public decimal? TopOfPerforated { get; set; }
        public decimal? TopOfPerforatedOld { get; set; }
        public decimal? BaseOfPerforated { get; set; }
        public decimal? BaseOfPerforatedOld { get; set; }
        public string? ArtificialLift { get; set; }
        public string? ArtificialLiftOld { get; set; }
        public string? Latitude4C { get; set; }
        public string? Latitude4COld { get; set; }
        public string? Longitude4C { get; set; }
        public string? Longitude4COld { get; set; }
        public string? LatitudeDD { get; set; }
        public string? LatitudeDDOld { get; set; }
        public string? LongitudeDD { get; set; }
        public string? LongitudeDDOld { get; set; }
        public string? DatumHorizontal { get; set; }
        public string? DatumHorizontalOld { get; set; }
        public string? TypeBaseCoordinate { get; set; }
        public string? TypeBaseCoordinateOld { get; set; }
        public string? CoordX { get; set; }
        public string? CoordXOld { get; set; }
        public string? CoordY { get; set; }
        public string? CoordYOld { get; set; }
        public User? User { get; set; }
        public Field? Field { get; set; }
        public Guid? FieldOld { get; set; }
        public Well? Well { get; set; }
        public List<Completion>? Completions { get; set; }
    }
}
