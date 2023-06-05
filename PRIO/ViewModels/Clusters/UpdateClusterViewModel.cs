using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Clusters
{
    public class UpdateClusterViewModel
    {
        public string? Name { get; set; } 
        public string? CodCluster { get; set; }
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
    }
}
