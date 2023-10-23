using Newtonsoft.Json;

namespace PRIO.src.Modules.FileImport.XML.Measuring.Infra.Http.Dtos
{
    public class ErrorXmlResponseDto
    {
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();


    }

    public class ErrorDifferentDates
    {
        [JsonProperty("message")]
        public string Message { get; set; } = "Datas entre medições diferentes";
        [JsonProperty("referenceDate")]
        public DateTime? ReferenceDate { get; set; }
        [JsonProperty("filesWithDifferentDates")]
        public List<FileErrorDto> FilesWithDifferentDates { get; set; } = new();
    }

    public class FileErrorDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("index")]
        public int Index { get; set; }
        [JsonProperty("fileName")]
        public string FileName { get; set; } = null!;
        [JsonProperty("fileType")]
        public string FileType { get; set; } = null!;
        [JsonProperty("measurementsWithDifferentDates")]
        public List<MeasurementsWithDifferentDates> MeasurementsWithDifferentDates { get; set; } = new();
    }

    public class MeasurementsWithDifferentDates
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }

    }
}
