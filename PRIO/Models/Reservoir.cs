namespace PRIO.Models
{
    public class Reservoir : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string CodReservoir { get; set; } = string.Empty;
        public User? User { get; set; }
        public Field Field { get; set; }
        public List<Completion>? Completions { get; set; }
    }
}
