namespace PRIO.DTOS.HistoryDTOS
{
    public class FieldHistoryDTO
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public string? codField { get; set; }
        public string? basin { get; set; }
        public string? state { get; set; }
        public string? location { get; set; }
        public Guid? installationId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public bool? isActive { get; set; }
    }
}
