using PRIO.src.Modules.PI.Dtos;

namespace PRIO.src.Modules.Hierarchy.Installations.Dtos
{
    public class InstallationWithAttributesDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? UepCod { get; set; }
        public string? UepName { get; set; }
        public string? CodInstallationAnp { get; set; }
        public double? GasSafetyBurnVolume { get; set; }
        public string? Description { get; set; }
        public string? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public List<AttributeDTO> Attributes { get; set; }
    }
}
