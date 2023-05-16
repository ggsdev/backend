namespace PRIO.Models
{
    public class Completion : BaseModel
    {
        public string Name { get; set; }
        public Reservoir Reservoir { get; set; }
        public User? User { get; set; }
        public List<Well> Wells { get; set; }
    }
}
