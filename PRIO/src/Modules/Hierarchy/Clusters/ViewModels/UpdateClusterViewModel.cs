using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Clusters.ViewModels
{
    public class UpdateClusterViewModel
    {
        [StringLength(120, ErrorMessage = "Name cannot exceed 120 characters.")]
        public string? Name { get; set; }
        [StringLength(8, ErrorMessage = "CodCluster cannot exceed 8 characters.")]
        public string? CodCluster { get; set; }
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
    }
}
