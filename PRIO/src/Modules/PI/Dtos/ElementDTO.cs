namespace PRIO.src.Modules.PI.Dtos
{
    public class ElementDTO
    {
        public Guid Id { get; set; }
        public string WebId { get; set; }
        public string PIId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SelfRoute { get; set; }
        public string AttributesRoute { get; set; }
        public string CategoryParameter { get; set; }
        public string Parameter { get; set; }
    }
}
