namespace PRIO.src.Modules.Hierarchy.Clusters.Dtos
{
    public class ClusterWithoutInstallationsDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
