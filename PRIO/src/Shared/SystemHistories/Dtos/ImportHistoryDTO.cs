namespace PRIO.src.Shared.SystemHistories.Dtos
{
    public class ImportHistoryDTO
    {
        public string? FileName { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
