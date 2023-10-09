namespace PRIO.src.Modules.Hierarchy.Wells.Dtos
{
    public class ManualConfigDTO
    {
        public Guid Id { get; set; }
        public InjectivityIndexDTO? InjectivityIndex { get; set; }
        public ProductivityIndexDTO? ProductivityIndex { get; set; }
        public BuildUpDTO? BuildUp { get; set; }
    }
}
