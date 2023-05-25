using PRIO.Models.Measurements;
using System.Text.Json.Serialization;

namespace PRIO.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        #region Relationships
        //public Role Role { get; set; }
        public Session? Session { get; set; }
        public List<Cluster>? Clusters { get; set; }
        public List<Field>? Fields { get; set; }
        public List<Installation>? Installations { get; set; }
        public List<Reservoir>? Reservoirs { get; set; }
        public List<Completion>? Completions { get; set; }
        public List<Well>? Wells { get; set; }
        [JsonIgnore]
        public List<Measurement>? Measurements { get; set; }

        #endregion
    }
}
