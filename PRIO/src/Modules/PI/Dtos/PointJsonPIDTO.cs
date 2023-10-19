namespace PRIO.src.Modules.PI.Dtos
{
    public class PointJsonPIDTO
    {
        public string WebId { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Descriptor { get; set; } = string.Empty;
        public string PointClass { get; set; } = string.Empty;
        public string PointType { get; set; } = string.Empty;
        public string DigitalSetName { get; set; } = string.Empty;
        public string EngineeringUnits { get; set; } = string.Empty;
        public double Span { get; set; }
        public double Zero { get; set; }
        public bool Step { get; set; }
        public bool Future { get; set; }
        public int DisplayDigits { get; set; }
        public LinksJson Links { get; set; }
    }

    public class LinksJson
    {
        public string Self { get; set; } = string.Empty;
        public string DataServer { get; set; } = string.Empty;
        public string Attributes { get; set; } = string.Empty;
        public string InterpolatedData { get; set; } = string.Empty;
        public string RecordedData { get; set; } = string.Empty;
        public string PlotData { get; set; } = string.Empty;
        public string SummaryData { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string EndValue { get; set; } = string.Empty;

    }
}
