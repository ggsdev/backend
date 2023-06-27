using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.Hierarchy.Completions.Dtos;
using PRIO.src.Modules.Hierarchy.Zones.Dtos;

namespace PRIO.src.Modules.Hierarchy.Reservoirs.Dtos
{
    public class ReservoirWithCompletionsDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? CodReservoir { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public UserDTO? User { get; set; }
        public List<CompletionWithoutReservoirDTO>? Completions { get; set; }
        public ZoneWithoutFieldDTO? Zone { get; set; }
    }
}
