﻿using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;
using System.Text.Json.Serialization;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models
{
    public class User : BaseModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? Type { get; set; } = "User";
        public bool? IsPermissionDefault { get; set; }
        public Guid? LastGroupId { get; set; }
        #region Relationships
        public List<UserPermission>? UserPermissions { get; set; }
        [JsonIgnore]
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
        public List<MeasurementHistory>? MeasurementsHistories { get; set; }
        public List<MeasuringEquipment>? MeasuringEquipments { get; set; }
        #endregion
    }
}
