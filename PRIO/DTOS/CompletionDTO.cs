namespace PRIO.DTOS
{
    public class CompletionDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CodCompletion { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
        public ReservoirDTO? Reservoir { get; set; }
        public WellDTO? Well { get; set; }
    }
}
