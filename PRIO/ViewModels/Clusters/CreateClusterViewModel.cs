using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Clusters
{
    public class CreateClusterViewModel
    {
        [Required(ErrorMessage = "Cluster name is a required field.")]
        public string Name { get; set; } = string.Empty;
        public string? CodCluster { get; set; }
        public bool? IsActive { get; set; } = true;
        public string? Description { get; set; }
    }
}
