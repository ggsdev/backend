namespace PRIO.Models
{
    public class Unit : BaseModel
    {
        public string Name { get; set; }
        public User? User { get; set; }
        public List<Cluster> Clusters { get; set; }
    }
}
