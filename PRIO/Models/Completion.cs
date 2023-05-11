namespace PRIO.Models
{
    public class Completion
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public Reservoir Reservoir { get; set; }
        public User? User { get; set; }
        public List<Well> Wells { get; set; }
    }
}
