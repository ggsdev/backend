namespace PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos
{
    public class CompletionHistoryDTO
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public decimal? TopOfPerforated { get; set; }
        public decimal? BaseOfPerforated { get; set; }
        public Guid? wellId { get; set; }
        public Guid? reservoirId { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public bool? isActive { get; set; }
    }
}
