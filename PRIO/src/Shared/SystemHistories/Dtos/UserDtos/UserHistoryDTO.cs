namespace PRIO.src.Shared.SystemHistories.Dtos.UserDtos
{
    public class UserHistoryDTO
    {
        public string? name { get; set; }
        public string? email { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public string? description { get; set; }
        public bool? isActive { get; set; }
    }
}
