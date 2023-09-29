namespace PRIO.src.Modules.PI.Infra.EF.Models
{
    public class Element
    {
        public Guid Id { get; set; }
        public string WebId { get; set; }
        public string PIId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SelfRoute { get; set; }
        public string AttributesRoute { get; set; }
        public Instance Instance { get; set; }
        public List<Attribute> AttributesInstance { get; set; }
    }
}
