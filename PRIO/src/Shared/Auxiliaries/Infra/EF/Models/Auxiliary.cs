namespace PRIO.src.Shared.Auxiliaries.Infra.EF.Models
{
    public class Auxiliary
    {
        public Guid? Id { get; set; }
        public string? Option { get; set; }
        public string? Route { get; set; }
        public string? Table { get; set; }
        public string? Select { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
