namespace PRIO.Models
{
    public class Cluster : BaseModel
    {
        public string Name { get; set; }
        public User? User { get; set; }
        public Unit Unit { get; set; }
        public List<Field> Fields { get; set; }
    }
}
