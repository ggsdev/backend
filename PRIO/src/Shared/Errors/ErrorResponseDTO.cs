using Newtonsoft.Json;

namespace PRIO.src.Shared.Errors
{
    public class ErrorResponseDTO
    {
        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
    }
}
