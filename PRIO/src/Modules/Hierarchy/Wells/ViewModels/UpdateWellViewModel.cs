using PRIO.src.Shared.Utils.Validators;
using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Wells.ViewModels
{
    public class UpdateWellViewModel
    {
        public bool? IsActive { get; set; }
        [StringLength(10, ErrorMessage = "Name cannot exceed 10 characters.")]
        public string? CodWell { get; set; }
        [StringLength(120, ErrorMessage = "Name cannot exceed 120 characters.")]
        public string? Name { get; set; }
        [StringLength(120, ErrorMessage = "Well Operator Name cannot exceed 120 characters.")]
        public string? WellOperatorName { get; set; }
        [StringLength(120, ErrorMessage = "CodWellAnp cannot exceed 120 characters.")]
        public string? CodWellAnp { get; set; }
        [StringLength(120, ErrorMessage = "CategoryAnp cannot exceed 120 characters.")]
        public string? CategoryAnp { get; set; }
        [StringLength(120, ErrorMessage = "CategoryReclassificationAnp cannot exceed 120 characters.")]
        public string? CategoryReclassificationAnp { get; set; }
        [StringLength(120, ErrorMessage = "CategoryOperator cannot exceed 120 characters.")]
        public string? CategoryOperator { get; set; }
        public bool StatusOperator { get; set; }
        [StringLength(120, ErrorMessage = "Type cannot exceed 120 characters.")]
        public string? Type { get; set; }
        [DecimalPrecision(12, isRequired: false)]
        public decimal WaterDepth { get; set; }
        [DecimalPrecision(12, isRequired: false)]
        public decimal TopOfPerforated { get; set; }
        [DecimalPrecision(12, isRequired: false)]
        public decimal BaseOfPerforated { get; set; }
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
        [StringLength(120, ErrorMessage = "DatumHorizontal cannot exceed 120 characters.")]
        public string? DatumHorizontal { get; set; }
        [StringLength(120, ErrorMessage = "TypeBaseCoordinate cannot exceed 120 characters.")]
        public string? TypeBaseCoordinate { get; set; }
        [RegularExpression(@"^-?\d{1,2},\d{10}$",
            ErrorMessage = "Invalid CoordinatesX format. Please use the decimal format 'dd,dddddddddd'.")]
        public string? CoordX { get; set; }
        [RegularExpression(@"^(-?\d{1,2},\d{10})$",
            ErrorMessage = "Invalid CoordinatesY format. Please use the decimal format 'dd,dddddddddd'.")]
        public string? CoordY { get; set; }
        public string? Description { get; set; }
        public Guid? FieldId { get; set; }
    }
}