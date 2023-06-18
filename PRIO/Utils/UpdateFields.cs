using PRIO.DTOS.HistoryDTOS;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.ViewModels.Clusters;
using PRIO.ViewModels.Fields;
using PRIO.ViewModels.Installations;
using PRIO.ViewModels.Reservoirs;
using PRIO.ViewModels.Users;
using PRIO.ViewModels.Wells;
using PRIO.ViewModels.Zones;
using System.Dynamic;

namespace PRIO.Utils
{
    public static class UpdateFields
    {
        public static Dictionary<string, object> CompareAndUpdateCluster(Cluster cluster, UpdateClusterViewModel body)
        {
            var updatedProperties = new Dictionary<string, object>();

            if (body.Name is not null && body.Name != cluster.Name)
            {
                cluster.Name = body.Name;
                updatedProperties[nameof(ClusterHistoryDTO.name)] = body.Name;
            }

            if (body.Description is not null && body.Description != cluster.Description)
            {
                cluster.Description = body.Description;
                updatedProperties[nameof(ClusterHistoryDTO.description)] = body.Description;
            }

            if (body.CodCluster is not null && body.CodCluster != cluster.CodCluster)
            {
                cluster.CodCluster = body.CodCluster;
                updatedProperties[nameof(ClusterHistoryDTO.codCluster)] = body.CodCluster;
            }
            return updatedProperties;

        }

        public static Dictionary<string, object> CompareAndUpdateInstallation(Installation installation, UpdateInstallationViewModel body)
        {
            var updatedProperties = new Dictionary<string, object>();

            if (body.Name is not null && body.Name != installation.Name)
            {
                installation.Name = body.Name;
                updatedProperties[nameof(InstallationHistoryDTO.name)] = body.Name;
            }

            if (body.Description is not null && body.Description != installation.Description)
            {
                installation.Description = body.Description;
                updatedProperties[nameof(InstallationHistoryDTO.description)] = body.Description;
            }

            if (body.CodInstallationUep is not null && body.CodInstallationUep != installation.CodInstallationUep)
            {
                installation.CodInstallationUep = body.CodInstallationUep;
                updatedProperties[nameof(InstallationHistoryDTO.codInstallationUep)] = body.CodInstallationUep;
            }

            return updatedProperties;

        }

        public static Dictionary<string, object> CompareAndUpdateField(Field field, UpdateFieldViewModel body)
        {
            var updatedProperties = new Dictionary<string, object>();

            if (body.Name is not null && body.Name != field.Name)
            {
                field.Name = body.Name;
                updatedProperties[nameof(FieldHistoryDTO.name)] = body.Name;
            }

            if (body.Description is not null && body.Description != field.Description)
            {
                field.Description = body.Description;
                updatedProperties[nameof(FieldHistoryDTO.description)] = body.Description;
            }

            if (body.CodField is not null && body.CodField != field.CodField)
            {
                field.CodField = body.CodField;
                updatedProperties[nameof(FieldHistoryDTO.codField)] = body.CodField;
            }

            if (body.Basin is not null && body.Basin != field.Basin)
            {
                field.Basin = body.Basin;
                updatedProperties[nameof(FieldHistoryDTO.basin)] = body.Basin;
            }

            if (body.State is not null && body.State != field.State)
            {
                field.State = body.State;
                updatedProperties[nameof(FieldHistoryDTO.state)] = body.State;
            }

            if (body.Location is not null && body.Location != field.Location)
            {
                field.Location = body.Location;
                updatedProperties[nameof(FieldHistoryDTO.location)] = body.Location;
            }

            return updatedProperties;

        }

        public static Dictionary<string, object> CompareAndUpdateZone(Zone zone, UpdateZoneViewModel body)
        {
            var updatedProperties = new Dictionary<string, object>();

            if (body.CodZone is not null && body.CodZone != zone.CodZone)
            {
                zone.CodZone = body.CodZone;
                updatedProperties[nameof(ZoneHistoryDTO.codZone)] = body.CodZone;
            }

            if (body.Description is not null && body.Description != zone.Description)
            {
                zone.Description = body.Description;
                updatedProperties[nameof(ZoneHistoryDTO.description)] = body.Description;
            }

            return updatedProperties;

        }

        public static Dictionary<string, object> CompareAndUpdateReservoir(Reservoir reservoir, UpdateReservoirViewModel body)
        {
            var updatedProperties = new Dictionary<string, object>();

            if (body.Name is not null && body.Name != reservoir.Name)
            {
                reservoir.Name = body.Name;
                updatedProperties[nameof(ReservoirHistoryDTO.name)] = body.Name;
            }

            if (body.Description is not null && body.Description != reservoir.Description)
            {
                reservoir.Description = body.Description;
                updatedProperties[nameof(ReservoirHistoryDTO.description)] = body.Description;
            }

            if (body.CodReservoir is not null && body.CodReservoir != reservoir.CodReservoir)
            {
                reservoir.CodReservoir = body.CodReservoir;
                updatedProperties[nameof(ReservoirHistoryDTO.codReservoir)] = body.CodReservoir;
            }

            return updatedProperties;
        }

        public static Dictionary<string, object> CompareAndUpdateWell(Well well, UpdateWellViewModel body)
        {
            var updatedProperties = new Dictionary<string, object>();

            var wellType = typeof(Well);
            var bodyType = typeof(UpdateWellViewModel);

            var properties = bodyType.GetProperties();

            foreach (var property in properties)
            {
                var wellProperty = wellType.GetProperty(property.Name);

                if (wellProperty != null)
                {
                    var wellValue = wellProperty.GetValue(well);
                    var bodyValue = property.GetValue(body);

                    if (bodyValue != null && !bodyValue.Equals(wellValue) && wellValue is not null)
                    {
                        wellProperty.SetValue(well, bodyValue);
                        updatedProperties[property.Name.ToLower()] = bodyValue;
                    }
                }
            }

            return updatedProperties;
        }

        public static Dictionary<string, object> CompareAndUpdateUser(User user, UpdateUserViewModel body)
        {
            var updatedProperties = new Dictionary<string, object>();

            if (body.Name is not null && body.Name != user.Name)
            {
                user.Name = body.Name;
                updatedProperties[nameof(UserHistoryDTO.name)] = body.Name;
            }

            if (body.Description is not null && body.Description != user.Description)
            {
                user.Description = body.Description;
                updatedProperties[nameof(UserHistoryDTO.description)] = body.Description;
            }

            if (body.Email is not null && body.Email != user.Email)
            {
                user.Email = body.Email;
                updatedProperties[nameof(UserHistoryDTO.email)] = body.Email;
            }

            if (body.Username is not null && body.Username != user.Username)
            {
                user.Username = body.Username;
                updatedProperties[nameof(UserHistoryDTO.username)] = body.Username;
            }

            if (body.Password is not null && body.Password != user.Password)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(body.Password);
                updatedProperties[nameof(UserHistoryDTO.password)] = BCrypt.Net.BCrypt.HashPassword(body.Password);
            }

            return updatedProperties;
        }

        public static dynamic DictionaryToObject(Dictionary<string, object> dict)
        {
            var eo = new ExpandoObject();
            var eoColl = (ICollection<KeyValuePair<string, object>>)eo;

            foreach (var kvp in dict)
            {
                eoColl.Add(kvp);
            }

            dynamic eoDynamic = eo;

            return eoDynamic;
        }
    }
}
