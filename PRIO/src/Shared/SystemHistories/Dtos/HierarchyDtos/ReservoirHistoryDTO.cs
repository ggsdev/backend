namespace PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos
{
    public class ReservoirHistoryDTO
    {
        public string? name { get; set; }
        public string? codReservoir { get; set; }
        public string? description { get; set; }
        public Guid? zoneId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public bool? isActive { get; set; }
    }
}
