using PRIO.src.Modules.Hierarchy.Wells.Dtos;

namespace PRIO.src.Modules.Balance.Balance.Dtos
{
    public class ManualConfigWithListsDTO
    {
        public Guid Id { get; set; }
        public List<InjectivityIndexDTO>? InjectivityIndex { get; set; }
        public List<ProductivityIndexDTO>? ProductivityIndex { get; set; }
        public List<BuildUpDTO>? BuildUp { get; set; }
    }
}
