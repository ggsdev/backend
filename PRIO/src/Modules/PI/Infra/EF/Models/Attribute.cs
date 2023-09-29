namespace PRIO.src.Modules.PI.Infra.EF.Models
{
    public class Attribute
    {
        public Guid Id { get; set; }
        public string WebId { get; set; }
        public string PIId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SelfRoute { get; set; }
        public string ValueRoute { get; set; }
        public Element Element { get; set; }
        public List<Value>? Values { get; set; }
    }
}
