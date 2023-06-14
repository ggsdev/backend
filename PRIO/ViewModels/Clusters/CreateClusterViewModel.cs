using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Clusters
{
    public class CreateClusterViewModel
    {
        [Required(ErrorMessage = "Cluster name is a required field.")]
        public string? Name { get; set; }
        public string? CodCluster { get; set; }
        public string? UepCode { get; set; }
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
    }
}
