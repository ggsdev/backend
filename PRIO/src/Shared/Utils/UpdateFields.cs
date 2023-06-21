using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Groups.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.ViewModels;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.ViewModels;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.ViewModels;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.ViewModels;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.ViewModels;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.ViewModels;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.ViewModels;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.ViewModels;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.ViewModels;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Dtos.UserDtos;
using System.Dynamic;

namespace PRIO.src.Shared.Utils
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

            if (body.UepCod is not null && body.UepCod != installation.UepCod)
            {
                installation.UepCod = body.UepCod;
                updatedProperties[nameof(InstallationHistoryDTO.uepCod)] = body.UepCod;
            }

            if (body.CodInstallation is not null && body.CodInstallation != installation.CodInstallation)
            {
                installation.UepCod = body.CodInstallation;
                updatedProperties[nameof(InstallationHistoryDTO.codInstallation)] = body.CodInstallation;
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

        public static Dictionary<string, object> CompareAndUpdateCompletion(Completion user, UpdateCompletionViewModel body)
        {
            var updatedProperties = new Dictionary<string, object>();

            if (body.Description is not null && body.Description != user.Description)
            {
                user.Description = body.Description;
                updatedProperties[nameof(CompletionHistoryDTO.description)] = body.Description;
            }

            if (body.CodCompletion is not null && body.CodCompletion != user.CodCompletion)
            {
                user.CodCompletion = body.CodCompletion;
                updatedProperties[nameof(CompletionHistoryDTO.codCompletion)] = body.CodCompletion;
            }

            return updatedProperties;
        }

        public static Dictionary<string, object> CompareAndUpdateEquipment(MeasuringEquipment equipment, UpdateEquipmentViewModel body)
        {
            var updatedProperties = new Dictionary<string, object>();

            var equipmentType = typeof(MeasuringEquipment);
            var bodyType = typeof(UpdateEquipmentViewModel);

            var properties = bodyType.GetProperties();

            foreach (var property in properties)
            {
                var equipmentProperty = equipmentType.GetProperty(property.Name);

                if (equipmentProperty != null)
                {
                    var equipmentValue = equipmentProperty.GetValue(equipment);
                    var bodyValue = property.GetValue(body);

                    if (bodyValue != null && !bodyValue.Equals(equipmentValue) && equipmentValue is not null)
                    {
                        equipmentProperty.SetValue(equipment, bodyValue);
                        updatedProperties[property.Name.ToLower()] = bodyValue;
                    }
                }
            }

            return updatedProperties;
        }

        public static Dictionary<string, object> CompareAndUpdateGroup(Group group, UpdateGroupViewModel body)
        {
            var updatedProperties = new Dictionary<string, object>();

            var groupType = typeof(Group);
            var bodyType = typeof(UpdateGroupViewModel);

            var properties = bodyType.GetProperties();

            foreach (var property in properties)
            {
                var groupProperty = groupType.GetProperty(property.Name);

                if (groupProperty != null)
                {
                    var groupValue = groupProperty.GetValue(group);
                    var bodyValue = property.GetValue(body);

                    if (bodyValue != null && !bodyValue.Equals(groupValue) && groupValue is not null)
                    {
                        groupProperty.SetValue(group, bodyValue);
                        updatedProperties[property.Name.ToLower()] = bodyValue;
                    }
                }
            }

            return updatedProperties;
        }

        public static Dictionary<string, object> CompareUpdateReturnOnlyUpdated<TModel, TViewModel>(TModel model, TViewModel viewModel)
        {
            var updatedProperties = new Dictionary<string, object>();

            var modelType = typeof(TModel);
            var viewModelType = typeof(TViewModel);

            var properties = viewModelType.GetProperties();

            foreach (var property in properties)
            {
                var modelProperty = modelType.GetProperty(property.Name);

                if (modelProperty != null)
                {
                    var modelValue = modelProperty.GetValue(model);
                    var viewModelValue = property.GetValue(viewModel);
                    if (property.PropertyType == typeof(Guid))
                        continue;

                    if (viewModelValue is not null && !viewModelValue.Equals(modelValue) && modelValue is not null)
                    {
                        modelProperty.SetValue(model, viewModelValue);
                        updatedProperties[property.Name.ToLower()] = viewModelValue;
                    }
                }
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
