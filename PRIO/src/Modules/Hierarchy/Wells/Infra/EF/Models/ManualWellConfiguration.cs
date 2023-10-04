namespace PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models
{
    public class ManualWellConfiguration
    {
        public Guid Id { get; set; }
        public List<ProductivityIndex>? ProductivityIndex { get; set; }
        public List<InjectivityIndex>? InjectivityIndex { get; set; }
        public List<BuildUp>? BuildUp { get; set; }
        public Well Well { get; set; }
    }
}
