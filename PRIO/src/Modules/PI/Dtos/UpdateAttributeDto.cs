namespace PRIO.src.Modules.PI.Dtos
{
    public class UpdateAttributeDto
    {
        public Guid Id { get; set; }
        public string Tag { get; set; }
        public string GroupParameter { get; set; }
        public string Parameter { get; set; }
        public bool Operational { get; set; }
        public bool Status { get; set; }
    }
}
