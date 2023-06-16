namespace PRIO.DTOS.HistoryDTOS
{
    public class InstallationHistoryDTO
    {
        public string? name { get; set; }
        public string? codInstallationUep { get; set; }
        public string? description { get; set; }
        public DateTime createdAt { get; set; }
        public Guid clusterId { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public bool isActive { get; set; }

    }
}
