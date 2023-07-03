namespace PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos
{
    public class MeasuringPointHistoryDTO
    {
        public string? name { get; set; }
        public string? tagPointMeasuring { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public bool? isActive { get; set; }
        public string? description { get; set; }
    }
}
