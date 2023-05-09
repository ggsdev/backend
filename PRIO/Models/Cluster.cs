namespace PRIO.Models
{
    public class Cluster
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public User? User { get; set; }
        public Unit Unit { get; set; }
        public List<Field> Fields { get; set; }
    }
}
