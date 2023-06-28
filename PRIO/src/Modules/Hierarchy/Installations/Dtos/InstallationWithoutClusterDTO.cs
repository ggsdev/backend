namespace PRIO.src.Modules.Hierarchy.Installations.Dtos
{
    public class InstallationWithoutClusterDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? UepCod { get; set; }
        public string? UepName { get; set; }
        public string? CodInstallationAnp { get; set; }
        public double? GasSafetyBurnVolume { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
