namespace PRIO.src.Modules.PI.Dtos
{
    public class AttributeDTO
    {
        public Guid Id { get; set; }
        public string WebId { get; set; }
        public string WellName { get; set; }
        public string PIId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SelfRoute { get; set; }
        public string ValueRoute { get; set; }
        public bool IsActive { get; set; }
        public bool IsOperating { get; set; }
        public DateTime CreatedAt { get; set; }
        public ElementDTO Element { get; set; }
    }
}
