namespace PRIO.src.Modules.Hierarchy.Zones.Dtos
{
    public class ZoneWithoutFieldDTO
    {
        public Guid? Id { get; set; }
        public string? CodZone { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}
