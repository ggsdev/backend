using PRIO.Models.BaseModels;
using PRIO.Models.HierarchyModels;
using PRIO.Models.MeasurementModels;
using System.Text.Json.Serialization;

namespace PRIO.Models.UserControlAccessModels
{
    public class User : BaseModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }

        #region Relationships
        public List<UserPermission>? UserPermissions { get; set; }
        [JsonIgnore]
        public List<SystemHistory>? SystemHistories { get; set; }
        public Session? Session { get; set; }
        public Group? Group { get; set; }
        public List<Cluster>? Clusters { get; set; }
        public List<Field>? Fields { get; set; }
        public List<Installation>? Installations { get; set; }
        public List<Reservoir>? Reservoirs { get; set; }
        public List<Completion>? Completions { get; set; }
        public List<Well>? Wells { get; set; }
        public List<Zone>? Zones { get; set; }
        [JsonIgnore]
        public List<Measurement>? Measurements { get; set; }
        public List<MeasuringEquipment>? MeasuringEquipments { get; set; }
        #endregion
    }
}
