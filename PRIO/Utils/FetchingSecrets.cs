using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PRIO.Utils
{
    public class FetchingSecrets
    {
        [JsonPropertyName("DATABASE_NAME")]
        public string? DatabaseName { get; set; }

        [JsonPropertyName("DATABASE_PASSWORD")]
        public string? DatabasePassword { get; set; }

        [JsonPropertyName("DATABASE_SERVER")]
        public string? DatabaseServer { get; set; }

        [JsonPropertyName("DATABASE_USER")]
        public string? DatabaseUser { get; set; }
        [JsonPropertyName("JWT_TOKEN")]
        public string? JwtToken { get; set; }

        private static readonly HttpClient _httpClient;
        private static readonly JsonSerializerOptions _jsonSerializerOptions;

        static FetchingSecrets()
        {
            _httpClient = new HttpClient();
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public static async Task<FetchingSecrets> FetchSecretsAsync()
        {
            var dopplerToken = Environment.GetEnvironmentVariable("DOPPLER_TOKEN");
            var basicAuthHeaderValue = Convert.ToBase64String(Encoding.Default.GetBytes(dopplerToken + ":"));

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", basicAuthHeaderValue);
            var streamTask = _httpClient.GetStreamAsync("https://api.doppler.com/v3/configs/config/secrets/download?format=json");
            var secrets = await JsonSerializer.DeserializeAsync<FetchingSecrets>(await streamTask, _jsonSerializerOptions);
            return secrets;
        }
    }
}
