using PRIO.src.Shared.Utils.Validators;
using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Wells.ViewModels
{
    public class CreateWellViewModel
    {
        public bool? IsActive { get; set; }
        [Required(ErrorMessage = "Name ANP is required")]
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")]
        public string? Name { get; set; }

        [StringLength(60, ErrorMessage = "CodWell cannot exceed 60 characters.")]
        public string? CodWell { get; set; }

        [Required(ErrorMessage = "Well Operator Name is required")]
        [StringLength(60, ErrorMessage = "Well Operator Name cannot exceed 60 characters.")]
        public string? WellOperatorName { get; set; }
        [Required(ErrorMessage = "Code ANP is required")]
        [StringLength(60, ErrorMessage = "CodWellAnp cannot exceed 60 characters.")]
        public string? CodWellAnp { get; set; }
        [Required(ErrorMessage = "Category ANP is required")]
        [StringLength(60, ErrorMessage = "CategoryAnp cannot exceed 60 characters.")]
        public string? CategoryAnp { get; set; }
        [StringLength(60, ErrorMessage = "CategoryReclassificationAnp cannot exceed 60 characters.")]
        public string? CategoryReclassificationAnp { get; set; }
        [Required(ErrorMessage = "CategoryOperator is required")]
        [StringLength(60, ErrorMessage = "CategoryOperator cannot exceed 60 characters.")]
        public string? CategoryOperator { get; set; }
        [Required(ErrorMessage = "Type is required")]
        [StringLength(60, ErrorMessage = "Type cannot exceed 60 characters.")]
        public string? Type { get; set; }
        [DecimalPrecision(12)]
        public decimal WaterDepth { get; set; }
        public string? ArtificialLift { get; set; }
        [RegularExpression(@"^-\d{2}:\d{2}:\d{2},\d{3}$",
        ErrorMessage = "Invalid latitude. Please use the format '-dd:mm:ss,sss'.")]
        public string? Latitude4C { get; set; }
        [RegularExpression(@"^-\d{2}:\d{2}:\d{2},\d{3}$",
        ErrorMessage = "Invalid longitude. Please use the format '-dd:mm:ss,sss'.")]
        public string? Longitude4C { get; set; }
        [RegularExpression(@"^-\d{1,2},\d{10}$",
        ErrorMessage = "Invalid latitude. Please use the format '-dd,dddddddddd'.")]
        public string? LatitudeDD { get; set; }
        [RegularExpression(@"^-\d{1,2},\d{10}$",
        ErrorMessage = "Invalid latitude. Please use the format '-dd,dddddddddd'.")]
        public string? LongitudeDD { get; set; }
        [StringLength(60, ErrorMessage = "DatumHorizontal cannot exceed 60 characters.")]
        public string? DatumHorizontal { get; set; }
        [StringLength(60, ErrorMessage = "TypeBaseCoordinate cannot exceed 60 characters.")]
        public string? TypeBaseCoordinate { get; set; }
        [RegularExpression(@"^-\d{1,2},\d{10}$",
        ErrorMessage = "Invalid CoordinatesX format. Please use the decimal format '-dd,dddddddddd'.")]
        public string? CoordX { get; set; }
        [RegularExpression(@"^(-\d{1,2},\d{10})$",
        ErrorMessage = "Invalid CoordinatesY format. Please use the decimal format '-dd,dddddddddd'.")]
        public string? CoordY { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "FieldId is required")]
        public Guid FieldId { get; set; }
    }
}