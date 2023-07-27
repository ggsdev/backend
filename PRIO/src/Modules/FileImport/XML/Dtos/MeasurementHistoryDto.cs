namespace PRIO.src.Modules.FileImport.XML.Dtos
{
    public class MeasurementHistoryDto
    {
        public DateTime ImportedAt { get; set; }
        public string FileContent { get; set; }
        public string FileType { get; set; }
        public bool IsLoaded { get; set; }
    }
}
