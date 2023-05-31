namespace PRIO.Models
{
    public class Completion : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string CodCompletion { get; set; } = string.Empty;
        public Reservoir? Reservoir { get; set; }
        public Well Well { get; set; }
        public User? User { get; set; }
    }
}
