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
        public string? Description { get; set; }
        public Guid? FieldId { get; set; }
    }
}