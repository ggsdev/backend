namespace PRIO.DTOS.UserDTOS
{
    public class UserHistoryDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? NameOld { get; set; }
        public string? Email { get; set; }
        public string? EmailOld { get; set; }
        public string? Username { get; set; }
        public string? UsernameOld { get; set; }
        public string? Type { get; set; }
        public string? TypeOld { get; set; }
        public string? Description { get; set; }
        public string? DescriptionOld { get; set; }
        public string? IsActive { get; set; }
        public string? IsActiveOld { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? TypeOperation { get; set; }
        public Guid? UserOperationId { get; set; }
    }
}
