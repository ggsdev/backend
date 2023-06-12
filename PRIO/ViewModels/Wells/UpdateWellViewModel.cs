using PRIO.Validators;
using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Wells
{
    public class UpdateWellViewModel
    {
        public string? CodWell { get; set; }
        public string? Name { get; set; }
        public string? WellOperatorName { get; set; }
        public string? CodWellAnp { get; set; }
        public string? CategoryAnp { get; set; }
        public string? CategoryReclassificationAnp { get; set; }
        public string? CategoryOperator { get; set; }
        public bool? StatusOperator { get; set; }
        public string? Type { get; set; }
        [DecimalPrecision(12)]
        public decimal? WaterDepth { get; set; }
        [DecimalPrecision(12)]
        public decimal? TopOfPerforated { get; set; }
        [DecimalPrecision(12)]
        public decimal? BaseOfPerforated { get; set; }
        public string? ArtificialLift { get; set; }
        [RegularExpression(@"^-?\d{2}:\d{2}:\d{2},\d{3}$",
        ErrorMessage = "Invalid latitude. Please use the format 'dd:mm:ss,sss'.")]
        public string? Latitude4C { get; set; }
        [RegularExpression(@"^-?\d{2}:\d{2}:\d{2},\d{3}$",
        ErrorMessage = "Invalid longitude. Please use the format 'dd:mm:ss,sss'.")]
        public string? Longitude4C { get; set; }
        [RegularExpression(@"^-?\d{1,2},\d{10}$",
        ErrorMessage = "Invalid latitude. Please use the format 'dd,dddddddddd'.")]
        public string? LatitudeDD { get; set; }
        [RegularExpression(@"^-?\d{1,2},\d{10}$",
        ErrorMessage = "Invalid latitude. Please use the format 'dd,dddddddddd'.")]
        public string? LongitudeDD { get; set; }
        public string? DatumHorizontal { get; set; }
        public string? TypeBaseCoordinate { get; set; }
        [RegularExpression(@"^-?\d{1,2},\d{10}$",
        ErrorMessage = "Invalid CoordinatesX format. Please use the decimal format 'dd,dddddddddd'.")]
        public string? CoordX { get; set; }
        [RegularExpression(@"^-?\d{1,2},\d{10}$",
        ErrorMessage = "Invalid CoordinatesY format. Please use the decimal format 'dd,dddddddddd'.")]
        public string? CoordY { get; set; }
        public string? Description { get; set; }
        public Guid? FieldId { get; set; }
    }
}