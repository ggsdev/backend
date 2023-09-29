namespace PRIO.src.Modules.PI.Infra.EF.Models
{
    public class Attributes
    {
        public Guid Id { get; set; }
        public string WebId { get; set; }
        public string PIId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SelfRoute { get; set; }
        public string ValueRoute { get; set; }
        public string WellName { get; set; }
        public Elements Element { get; set; }
    }
}
