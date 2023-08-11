namespace PRIO.src.Shared.SystemHistories.Dtos.UserDtos
{
    public class GroupHistoryDTO
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public bool? isActive { get; set; }
    }
}
