namespace PRIO.Models
{
    public class Reservoir
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public User? User { get; set; }
        public Installation Installation { get; set; }
        public List<Completion> Completions { get; set; }
    }
}
