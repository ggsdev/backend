using PRIO.src.Modules.Hierarchy.Wells.Dtos;

namespace PRIO.src.Modules.Hierarchy.Completions.Dtos
{
    public class CompletionWithoutReservoirDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CodCompletion { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public WellWithoutCompletionDTO? Well { get; set; }
    }
}
