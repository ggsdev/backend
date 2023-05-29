namespace PRIO.Models
{
    public class Well : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string WellOperatorName { get; set; } = string.Empty;
        public string CodWellAnp { get; set; } = string.Empty;
        public string CategoryAnp { get; set; } = string.Empty;
        public string CategoryReclassificationAnp { get; set; } = string.Empty;
        public string CategoryOperator { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public double WaterDepth { get; set; }
        public double TopOfPerforated { get; set; }
        public double BaseOfPerforated { get; set; }
        public string ArtificialLift { get; set; } = string.Empty;
        public string Latitude4C { get; set; } = string.Empty;
        public string Longitude4C { get; set; } = string.Empty;
        public string LatitudeDD { get; set; } = string.Empty;
        public string LongitudeDD { get; set; } = string.Empty;
        public string DatumHorizontal { get; set; } = string.Empty;
        public string TypeBaseCoordinate { get; set; } = string.Empty;
        public string CoordX { get; set; } = string.Empty;
        public string CoordY { get; set; } = string.Empty;
        public User? User { get; set; }
        public List<Completion>? Completions { get; set; }
    }
}
