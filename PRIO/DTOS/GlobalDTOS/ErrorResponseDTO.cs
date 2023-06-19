using Newtonsoft.Json;

namespace PRIO.DTOS.GlobalDTOS
{
    public class ErrorResponseDTO
    {
        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
    }
}
