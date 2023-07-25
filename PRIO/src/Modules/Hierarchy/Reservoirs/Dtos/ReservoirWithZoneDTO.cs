using PRIO.src.Modules.Hierarchy.Zones.Dtos;

namespace PRIO.src.Modules.Hierarchy.Reservoirs.Dtos
{
    public class ReservoirWithZoneDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ZoneWithoutFieldDTO? Zone { get; set; }
        public bool? IsActive { get; set; }
    }
}
