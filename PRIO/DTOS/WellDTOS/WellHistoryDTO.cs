using PRIO.DTOS.FieldDTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.Models.Fields;
using PRIO.Models.Users;
using PRIO.Models.Wells;

namespace PRIO.DTOS.WellDTOS
{
    public class WellHistoryDTO
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
        public double? WaterDepth { get; set; }
        public double? WaterDepthOld { get; set; }
        public double? TopOfPerforated { get; set; }
        public double? TopOfPerforatedOld { get; set; }
        public double? BaseOfPerforated { get; set; }
        public double? BaseOfPerforatedOld { get; set; }
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
        public string? TypeOperation { get; set; }
        public FieldDTO? Field { get; set; }
        public Guid? FieldOld { get; set; }
        public WellDTO? Well { get; set; }
        public UserDTO? User { get; set; }
    }
}
