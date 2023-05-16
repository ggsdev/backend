namespace PRIO.Models
{
    public class Reservoir : BaseModel
    {
        public string Name { get; set; }
        public User? User { get; set; }
        public Installation Installation { get; set; }
        public List<Completion> Completions { get; set; }
    }
}
