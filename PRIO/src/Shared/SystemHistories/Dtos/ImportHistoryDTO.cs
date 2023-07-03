namespace PRIO.src.Shared.SystemHistories.Dtos
{
    public class ImportHistoryDTO
    {
        public Guid? Id { get; set; }
        public object? FileName { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
