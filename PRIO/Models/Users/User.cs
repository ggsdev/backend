﻿using PRIO.Models.BaseModels;
using PRIO.Models.Clusters;
using PRIO.Models.Completions;
using PRIO.Models.Fields;
using PRIO.Models.Installations;
using PRIO.Models.Measurements;
using PRIO.Models.MeasuringEquipments;
using PRIO.Models.Permissions;
using PRIO.Models.Reservoirs;
using PRIO.Models.Wells;
using PRIO.Models.Zones;
using System.Text.Json.Serialization;

namespace PRIO.Models.Users
{
    public class User : BaseModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? Type { get; set; }

        #region Relationships
        //public Role Role { get; set; }

        public List<UserPermissions>? UserPermissions { get; set; }
        public Session? Session { get; set; }
        public List<Cluster>? Clusters { get; set; }
        public List<ClusterHistory>? ClusterHistories { get; set; }
        public List<Field>? Fields { get; set; }
        public List<FieldHistory>? FieldHistories { get; set; }
        public List<Installation>? Installations { get; set; }
        public List<InstallationHistory>? InstallationHistories { get; set; }
        public List<Reservoir>? Reservoirs { get; set; }
        public List<ReservoirHistory>? ReservoirHistories { get; set; }
        public List<Completion>? Completions { get; set; }
        public List<CompletionHistory>? CompletionHistories { get; set; }
        public List<Well>? Wells { get; set; }
        public List<WellHistory>? WellHistories { get; set; }
        public List<Zone>? Zones { get; set; }
        public List<ZoneHistory>? ZoneHistories { get; set; }
        [JsonIgnore]
        public List<Measurement>? Measurements { get; set; }
        public List<MeasuringEquipment>? MeasuringEquipments { get; set; }
        public List<UserHistory>? UserHistories { get; set; }
        #endregion
    }
}
