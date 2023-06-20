using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Clusters.ViewModels
{
    public class CreateClusterViewModel
    {
        [Required(ErrorMessage = "Cluster name is a required field.")]
        [StringLength(120, ErrorMessage = "Name cannot exceed 120 characters.")]
        public string? Name { get; set; }
        [StringLength(10, ErrorMessage = "CodCluster cannot exceed 10 characters.")]
        public string? CodCluster { get; set; }
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
    }
}
