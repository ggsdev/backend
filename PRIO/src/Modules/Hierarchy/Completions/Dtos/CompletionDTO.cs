using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.Hierarchy.Reservoirs.Dtos;
using PRIO.src.Modules.Hierarchy.Wells.Dtos;

namespace PRIO.src.Modules.Hierarchy.Completions.Dtos
{
    public class CompletionDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public decimal? AllocationReservoir { get; set; }
        public decimal? TopOfPerforated { get; set; }
        public decimal? BaseOfPerforated { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
        public ReservoirDTO? Reservoir { get; set; }
        public WellDTO? Well { get; set; }
    }
}
