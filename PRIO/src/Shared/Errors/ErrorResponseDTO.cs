using Newtonsoft.Json;

namespace PRIO.src.Shared.Errors
{
    public class ErrorResponseDTO
    {
        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
    }

    public class XlsErrorImportDTO
    {
        [JsonProperty("message")]

        public string Message { get; set; } = string.Empty;
        [JsonProperty("errors")]

        public List<string>? Errors { get; set; }
    }
}
