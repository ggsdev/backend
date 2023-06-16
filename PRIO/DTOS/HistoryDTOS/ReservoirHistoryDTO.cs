namespace PRIO.DTOS.HistoryDTOS
{
    public class ReservoirHistoryDTO
    {
        public string? name { get; set; }
        public string? codReservoir { get; set; }
        public string? description { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public bool? isActive { get; set; }
    }
}
