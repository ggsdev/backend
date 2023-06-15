namespace PRIO.DTOS.GlobalDTOS
{
    public class BaseDTO
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
