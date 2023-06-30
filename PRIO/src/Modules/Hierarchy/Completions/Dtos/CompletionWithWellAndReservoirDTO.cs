using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.Hierarchy.Reservoirs.Dtos;
using PRIO.src.Modules.Hierarchy.Wells.Dtos;

namespace PRIO.src.Modules.Hierarchy.Completions.Dtos
{
    public class CompletionWithWellAndReservoirDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CodCompletion { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public UserDTO? User { get; set; }
        public ReservoirDTO? Reservoir { get; set; }
        public WellWithoutFieldDTO? Well { get; set; }
    }
}
