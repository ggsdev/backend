using PRIO.src.Modules.Hierarchy.Reservoirs.Dtos;
using PRIO.src.Modules.Hierarchy.Wells.Dtos;

namespace PRIO.src.Modules.Hierarchy.Completions.Dtos
{
    public class CreateUpdateCompletionDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? AllocationReservoir { get; set; }
        public decimal? TopOfPerforated { get; set; }
        public decimal? BaseOfPerforated { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public WellWithoutCompletionDTO? Well { get; set; }
        public CreateUpdateReservoirDTO? Reservoir { get; set; }
    }
}
