using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Clusters.ViewModels
{
    public class CreateClusterViewModel
    {
        [Required(ErrorMessage = "Cluster name is a required field.")]
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")]
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
    }
}
