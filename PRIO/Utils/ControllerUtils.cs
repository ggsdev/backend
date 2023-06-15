using PRIO.Models.HierarchyModels;
using PRIO.ViewModels.Clusters;
using System.Dynamic;

namespace PRIO.Utils
{
    public static class ControllerUtils
    {
        public static Dictionary<string, object> CompareAndUpdateProperties(Cluster cluster, UpdateClusterViewModel body)
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
