using PRIO.Models.HierarchyModels;
using PRIO.ViewModels.Clusters;
using PRIO.ViewModels.Installations;
using System.Dynamic;

namespace PRIO.Utils
{
    public static class ControllerUtils
    {
        public static Dictionary<string, object> CompareAndUpdateCluster(Cluster cluster, UpdateClusterViewModel body)
        {
            var updatedProperties = new Dictionary<string, object>();

            if (body.Name is not null && body.Name != cluster.Name)
            {
                cluster.Name = body.Name;
                updatedProperties[nameof(Cluster.Name)] = body.Name;
            }

            if (body.Description is not null && body.Description != cluster.Description)
            {
                cluster.Description = body.Description;
                updatedProperties[nameof(Cluster.Description)] = body.Description;
            }

            if (body.CodCluster is not null && body.CodCluster != cluster.CodCluster)
            {
                cluster.CodCluster = body.CodCluster;
                updatedProperties[nameof(Cluster.CodCluster)] = body.CodCluster;
            }
            return updatedProperties;

        }

        public static Dictionary<string, object> CompareAndUpdateInstallation(Installation installation, UpdateInstallationViewModel body)
        {
            var updatedProperties = new Dictionary<string, object>();

            if (body.Name is not null && body.Name != installation.Name)
            {
                installation.Name = body.Name;
                updatedProperties[nameof(Installation.Name)] = body.Name;
            }

            if (body.Description is not null && body.Description != installation.Description)
            {
                installation.Description = body.Description;
                updatedProperties[nameof(Installation.Description)] = body.Description;
            }

            if (body.CodInstallationUep is not null && body.CodInstallationUep != installation.CodInstallationUep)
            {
                installation.CodInstallationUep = body.CodInstallationUep;
                updatedProperties[nameof(Installation.CodInstallationUep)] = body.CodInstallationUep;
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
